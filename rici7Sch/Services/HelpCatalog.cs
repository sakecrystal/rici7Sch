using System.Text.Json;
using System.Text.RegularExpressions;

namespace rici7Sch.Services
{
    /// <summary>目次 (.hhc 由来) のノード。Topic が null のものは「本」(フォルダ)。</summary>
    public sealed class TocNode
    {
        public string Title { get; set; } = "";
        public string? Topic { get; set; }
        public string? Anchor { get; set; }
        public List<TocNode> Children { get; set; } = [];

        public string? Url => Topic is null ? null : HelpCatalog.TopicUrl(Topic, Anchor);
    }

    /// <summary>索引 (.hhk 由来) のキーワード。</summary>
    public sealed class KeywordEntry
    {
        public string Keyword { get; set; } = "";
        public List<KeywordTopic> Topics { get; set; } = [];
    }

    public sealed class KeywordTopic
    {
        public string Title { get; set; } = "";
        public string Topic { get; set; } = "";
        public string? Anchor { get; set; }
        public string Url => HelpCatalog.TopicUrl(Topic, Anchor);
    }

    public sealed class TopicInfo
    {
        public string Id { get; set; } = "";
        public string Title { get; set; } = "";
        public string Text { get; set; } = "";
        /// <summary>元トピックが参照していたスタイルシート (mshtmlExpNonbrand / mshtmlExp2nd)。</summary>
        public string Stylesheet { get; set; } = "mshtmlExpNonbrand";
        public string Url => HelpCatalog.TopicUrl(Id, null);
    }

    public sealed class WebSearchOptions
    {
        public bool Enabled { get; set; }
        public bool Embedded { get; set; }
        public bool MainWindowOnly { get; set; }
        public string ButtonLabel { get; set; } = "WebSearch";
    }

    public sealed class HelpOptions
    {
        public WebSearchOptions WebSearch { get; set; } = new();
    }

    /// <summary>トピックの前後・パンくず (ブラウズシーケンス相当)。</summary>
    public sealed record TopicNavigation(
        TopicInfo Topic,
        IReadOnlyList<TocNode> Breadcrumb,
        TopicInfo? Previous,
        TopicInfo? Next);

    /// <summary>検索結果。Snippet は強調部分を分けたセグメント列 (HTML は含まない)。</summary>
    public sealed record SearchHit(TopicInfo Topic, double Score, IReadOnlyList<(string Text, bool Match)> Snippet);

    /// <summary>変換済みヘルプデータ (Data/*.json) を読み込み、目次・索引・検索を提供する。</summary>
    public sealed class HelpCatalog
    {
        private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

        private readonly Dictionary<string, TopicInfo> _topics;
        private readonly List<string> _sequence;                         // 目次順のトピック (重複除去)
        private readonly Dictionary<string, List<TocNode>> _paths;       // トピック → 目次上の親の列

        public IReadOnlyList<TocNode> Toc { get; }
        public IReadOnlyList<KeywordEntry> Keywords { get; }
        public HelpOptions Options { get; }
        public IEnumerable<TopicInfo> Topics => _topics.Values;

        /// <summary>目次順のトピック ID (ブラウズシーケンス)。</summary>
        public IReadOnlyList<string> Sequence => _sequence;

        /// <summary>目次に載っていないトピック (ポップアップ用など)。</summary>
        public IEnumerable<TopicInfo> TopicsNotInToc =>
            _topics.Values.Where(t => !_paths.ContainsKey(t.Id)).OrderBy(t => t.Title);

        /// <summary>目次ノードを深さ付きで平坦化する。</summary>
        public static IEnumerable<(TocNode Node, int Depth)> Flatten(IEnumerable<TocNode> nodes, int depth = 0)
        {
            foreach (var n in nodes)
            {
                yield return (n, depth);
                foreach (var c in Flatten(n.Children, depth + 1)) yield return c;
            }
        }

        public HelpCatalog(IWebHostEnvironment env)
        {
            var dir = Path.Combine(env.ContentRootPath, "Data");
            T Load<T>(string name) where T : new()
            {
                var path = Path.Combine(dir, name);
                return File.Exists(path)
                    ? JsonSerializer.Deserialize<T>(File.ReadAllText(path), JsonOptions) ?? new T()
                    : new T();
            }

            Toc = Load<List<TocNode>>("toc.json");
            Keywords = Load<List<KeywordEntry>>("keywords.json");
            Options = Load<HelpOptions>("helpoptions.json");
            _topics = Load<List<TopicInfo>>("topics.json")
                .ToDictionary(t => t.Id, StringComparer.OrdinalIgnoreCase);

            _sequence = [];
            _paths = new(StringComparer.OrdinalIgnoreCase);
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            void Walk(IEnumerable<TocNode> nodes, List<TocNode> parents)
            {
                foreach (var n in nodes)
                {
                    if (n.Topic is not null && seen.Add(n.Topic))
                    {
                        _sequence.Add(n.Topic);
                        _paths[n.Topic] = [.. parents];
                    }
                    if (n.Children.Count > 0)
                        Walk(n.Children, [.. parents, n]);
                }
            }
            Walk(Toc, []);
        }

        public static string TopicUrl(string id, string? anchor) =>
            "/Topics/" + id + (string.IsNullOrEmpty(anchor) ? "" : "#" + anchor);

        public TopicInfo? Find(string id) => _topics.GetValueOrDefault(id);

        public TopicNavigation? GetNavigation(string id)
        {
            if (!_topics.TryGetValue(id, out var topic)) return null;
            var index = _sequence.FindIndex(s => s.Equals(id, StringComparison.OrdinalIgnoreCase));
            TopicInfo? prev = index > 0 ? Find(_sequence[index - 1]) : null;
            TopicInfo? next = index >= 0 && index < _sequence.Count - 1 ? Find(_sequence[index + 1]) : null;
            return new TopicNavigation(topic, _paths.GetValueOrDefault(id) ?? [], prev, next);
        }

        /// <summary>目次ノードが現在のトピックを (子孫として) 含むか。</summary>
        public static bool Contains(TocNode node, string? topicId) =>
            topicId is not null &&
            (string.Equals(node.Topic, topicId, StringComparison.OrdinalIgnoreCase) ||
             node.Children.Any(c => Contains(c, topicId)));

        /// <summary>全文検索 (すべての語を含むトピック。タイトル一致を優先)。</summary>
        public IReadOnlyList<SearchHit> Search(string? query, int max = 100)
        {
            var terms = Tokenize(query);
            if (terms.Count == 0) return [];

            var hits = new List<SearchHit>();
            foreach (var t in _topics.Values)
            {
                double score = 0;
                var all = true;
                foreach (var term in terms)
                {
                    var inTitle = Count(t.Title, term);
                    var inText = Count(t.Text, term);
                    if (inTitle + inText == 0) { all = false; break; }
                    score += inTitle * 10 + Math.Min(inText, 20);
                }
                if (!all) continue;
                if (t.Title.Contains(query!.Trim(), StringComparison.OrdinalIgnoreCase)) score += 25;
                hits.Add(new SearchHit(t, score, Snippet(t.Text, terms)));
            }
            return hits.OrderByDescending(h => h.Score).ThenBy(h => h.Topic.Title).Take(max).ToList();
        }

        public static IReadOnlyList<string> Tokenize(string? query) =>
            string.IsNullOrWhiteSpace(query)
                ? []
                : Regex.Matches(query, "\"([^\"]+)\"|(\\S+)")
                    .Select(m => (m.Groups[1].Success ? m.Groups[1].Value : m.Groups[2].Value).Trim())
                    .Where(s => s.Length > 0)
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();

        private static int Count(string text, string term)
        {
            int n = 0, i = 0;
            while ((i = text.IndexOf(term, i, StringComparison.OrdinalIgnoreCase)) >= 0) { n++; i += term.Length; }
            return n;
        }

        private static List<(string, bool)> Snippet(string text, IReadOnlyList<string> terms, int radius = 90)
        {
            var first = terms.Select(t => text.IndexOf(t, StringComparison.OrdinalIgnoreCase))
                             .Where(i => i >= 0).DefaultIfEmpty(0).Min();
            var start = Math.Max(0, first - radius);
            var end = Math.Min(text.Length, first + radius * 2);
            var excerpt = (start > 0 ? "…" : "") + text[start..end] + (end < text.Length ? "…" : "");

            var pattern = string.Join("|", terms.OrderByDescending(t => t.Length).Select(Regex.Escape));
            var parts = new List<(string, bool)>();
            var pos = 0;
            foreach (Match m in Regex.Matches(excerpt, pattern, RegexOptions.IgnoreCase))
            {
                if (m.Index > pos) parts.Add((excerpt[pos..m.Index], false));
                parts.Add((m.Value, true));
                pos = m.Index + m.Length;
            }
            if (pos < excerpt.Length) parts.Add((excerpt[pos..], false));
            return parts;
        }
    }
}
