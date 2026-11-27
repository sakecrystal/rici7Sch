// Printer Driver Help - client scripts
// 旧 ehlpdhtm.js (RoboHelp DHTML 効果) の機能を Bootstrap ベースで再実装:
//   - ドロップダウン (kadovTextPopup)   → Bootstrap collapse + 「すべて表示」
//   - ファイルポップアップ (BSSCPopup) → モーダル表示
//   - 印刷ボタン / 目次の同期 / 索引の絞り込み / テーマ切替
(function () {
    'use strict';

    const $ = (sel, root = document) => root.querySelector(sel);
    const $$ = (sel, root = document) => Array.from(root.querySelectorAll(sel));

    // ------------------------------------------------------------ theme
    const themeToggle = $('#themeToggle');
    if (themeToggle) {
        themeToggle.addEventListener('click', () => {
            const next = document.documentElement.getAttribute('data-bs-theme') === 'dark' ? 'light' : 'dark';
            document.documentElement.setAttribute('data-bs-theme', next);
            try { localStorage.setItem('help-theme', next); } catch (e) { /* storage unavailable */ }
        });
    }

    // ------------------------------------------------------------ print
    $$('[data-action="print"]').forEach(btn => btn.addEventListener('click', () => window.print()));

    // ------------------------------------------------------------ TOC sync
    // 現在のトピックを目次内で見える位置までスクロール
    const current = $('.toc .toc-link.current');
    if (current) current.scrollIntoView({ block: 'center' });

    const collapseAll = $('#tocCollapseAll');
    if (collapseAll) {
        collapseAll.addEventListener('click', () => {
            const anyOpen = $$('.toc details[open]').length > 0;
            $$('.toc details').forEach(d => { d.open = !anyOpen; });
            collapseAll.textContent = anyOpen ? 'すべて開く' : 'すべて閉じる';
        });
    }

    // ------------------------------------------------------------ dropdowns (kadovTextPopup)
    const panels = $$('.help-content .help-dropdown');
    const expandAll = $('#expandAll');
    if (expandAll && panels.length > 0 && window.bootstrap) {
        expandAll.hidden = false;
        let shown = false;
        expandAll.addEventListener('click', () => {
            shown = !shown;
            panels.forEach(p => bootstrap.Collapse.getOrCreateInstance(p, { toggle: false })[shown ? 'show' : 'hide']());
            expandAll.textContent = shown ? 'すべて折りたたむ' : 'すべて展開';
        });
    }

    // ------------------------------------------------------------ file popups (BSSCPopup)
    const modalEl = $('#helpPopup');
    if (modalEl && window.bootstrap) {
        const modal = bootstrap.Modal.getOrCreateInstance(modalEl);
        const titleEl = $('.modal-title', modalEl);
        const bodyEl = $('.modal-body', modalEl);
        const openEl = $('#helpPopupOpen', modalEl);
        const cache = new Map();

        document.addEventListener('click', async ev => {
            const link = ev.target.closest('a[data-help-popup]');
            if (!link || ev.ctrlKey || ev.metaKey || ev.shiftKey) return;
            ev.preventDefault();
            const url = link.getAttribute('href');
            titleEl.textContent = link.textContent.trim();
            bodyEl.innerHTML = '<div class="text-center py-4"><div class="spinner-border text-secondary" role="status"><span class="visually-hidden">読み込み中…</span></div></div>';
            openEl.href = url;
            modal.show();
            try {
                let doc = cache.get(url);
                if (!doc) {
                    const res = await fetch(url, { headers: { 'Accept': 'text/html' } });
                    if (!res.ok) throw new Error(res.status);
                    doc = new DOMParser().parseFromString(await res.text(), 'text/html');
                    cache.set(url, doc);
                }
                const content = doc.querySelector('.topic .help-content');
                const heading = doc.querySelector('.topic-title');
                if (heading) titleEl.textContent = heading.textContent.trim();
                bodyEl.replaceChildren(...(content ? Array.from(content.cloneNode(true).childNodes) : []));
            } catch (e) {
                bodyEl.innerHTML = '';
                const p = document.createElement('p');
                p.textContent = 'トピックを読み込めませんでした。';
                bodyEl.appendChild(p);
            }
        });
    }

    // ------------------------------------------------------------ keyword index filter
    const filterForm = $('[data-keyword-filter]');
    if (filterForm) {
        const input = $('input[type="search"]', filterForm);
        const countEl = $('[data-count]', filterForm);
        const entries = $$('.keyword-entry');
        const groups = $$('.keyword-group');
        const empty = $('[data-empty]');
        filterForm.addEventListener('submit', ev => ev.preventDefault());

        // サーバー側で絞り込み済み (?k=) の場合、条件が広がったら再取得する
        const serverFilter = input.defaultValue.trim().toLowerCase();
        let timer = 0;

        const apply = () => {
            const q = input.value.trim().toLowerCase();
            if (serverFilter && !q.includes(serverFilter)) {
                clearTimeout(timer);
                timer = setTimeout(() => {
                    const url = new URL(location.href);
                    if (q) url.searchParams.set('k', input.value.trim()); else url.searchParams.delete('k');
                    location.replace(url);
                }, 300);
                return;
            }
            let visible = 0;
            entries.forEach(e => {
                // WebHelp の索引と同じく前方一致を優先し、部分一致も含める
                const hit = !q || e.dataset.keyword.includes(q);
                e.hidden = !hit;
                if (hit) visible++;
            });
            groups.forEach(g => {
                const show = $$('.keyword-entry', g).some(e => !e.hidden);
                g.hidden = !show;
                const a = $(`.alpha-nav a[data-alpha="${CSS.escape(g.dataset.group)}"]`);
                if (a) a.hidden = !show;
            });
            countEl.textContent = visible;
            if (empty) empty.hidden = visible > 0 || !q;
            // 最初の前方一致へスクロール
            if (q) {
                const first = entries.find(e => !e.hidden && e.dataset.keyword.startsWith(q));
                if (first) first.scrollIntoView({ block: 'nearest' });
            }
            const url = new URL(location.href);
            if (q) url.searchParams.set('k', input.value.trim()); else url.searchParams.delete('k');
            history.replaceState(null, '', url);
        };
        input.addEventListener('input', apply);
    }
})();
