// Classic Viewer - 旧 WebHelp のナビゲーション (目次・索引・検索・用語集・前へ/次へ・目次の同期) を再現
(function () {
    'use strict';
    const root = document.querySelector('[data-classic]');
    if (!root) return;

    const $ = (sel, r = document) => r.querySelector(sel);
    const $$ = (sel, r = document) => Array.from(r.querySelectorAll(sel));
    const frame = $('#classicFrame');
    const sequence = JSON.parse(root.dataset.sequence || '[]');
    const prefix = '/Classic/HTMLFILE/';
    const urlFor = id => prefix + id + '.htm';

    // フレームに表示中のトピック ID
    const currentId = () => {
        try {
            const path = decodeURIComponent(frame.contentWindow.location.pathname);
            if (!path.startsWith(prefix)) return null;
            return path.slice(prefix.length).replace(/\.htm$/i, '');
        } catch (e) { return null; }
    };
    const seqIndex = id => id ? sequence.findIndex(s => s.toLowerCase() === id.toLowerCase()) : -1;

    // ---------------------------------------------------------------- 目次の同期
    const tocLinks = $$('.classic-toc a[data-topic]');
    const sync = (scroll) => {
        const id = currentId();
        tocLinks.forEach(a => a.classList.toggle('current', !!id && a.dataset.topic.toLowerCase() === id.toLowerCase()));
        const cur = $('.classic-toc a.current');
        if (cur && scroll) cur.scrollIntoView({ block: 'nearest' });
        const i = seqIndex(id);
        $('[data-cmd="prev"]').disabled = i <= 0;
        $('[data-cmd="next"]').disabled = i < 0 || i >= sequence.length - 1;
        const modern = $('[data-cmd="modern"]');
        if (modern && id) modern.href = '/Topics/' + id;
        if (id) {
            const url = new URL(location.href);
            url.searchParams.set('topic', id);
            history.replaceState(null, '', url);
        }
    };
    frame.addEventListener('load', () => sync(true));

    // ---------------------------------------------------------------- ツールバー
    root.addEventListener('click', ev => {
        const btn = ev.target.closest('[data-cmd]');
        if (!btn) return;
        const cmd = btn.dataset.cmd;
        if (cmd === 'modern') return;
        ev.preventDefault();
        if (cmd === 'prev' || cmd === 'next') {
            const i = seqIndex(currentId());
            const j = cmd === 'prev' ? i - 1 : i + 1;
            if (j >= 0 && j < sequence.length) frame.src = urlFor(sequence[j]);
        } else if (cmd === 'sync') {
            const tab = $('[data-bs-target="#c-toc"]');
            if (tab && window.bootstrap) bootstrap.Tab.getOrCreateInstance(tab).show();
            sync(true);
        } else if (cmd === 'print') {
            try { frame.contentWindow.focus(); frame.contentWindow.print(); } catch (e) { window.print(); }
        } else if (cmd === 'nav') {
            const hidden = root.classList.toggle('nav-hidden');
            btn.textContent = hidden ? (btn.dataset.show || 'Show') : (btn.dataset.hide || 'Hide');
            btn.title = hidden ? root.dataset.msgShow : root.dataset.msgHide;
        }
    });
    const navBtn = $('[data-cmd="nav"]');
    navBtn.dataset.hide = navBtn.textContent.trim();
    navBtn.dataset.show = 'Show';

    // ---------------------------------------------------------------- 索引
    const indexInput = $('#cIndexInput');
    const indexItems = $$('#cIndexList > li');
    indexInput.addEventListener('input', () => {
        const q = indexInput.value.trim().toLowerCase();
        // WebHelp と同じく入力に前方一致する最初のキーワードへ移動し、一致しないものは隠す
        let first = null;
        indexItems.forEach(li => {
            const hit = !q || li.dataset.keyword.includes(q);
            li.hidden = !hit;
            if (hit && !first && li.dataset.keyword.startsWith(q)) first = li;
        });
        if (first) first.scrollIntoView({ block: 'nearest' });
    });

    const chooser = $('#cChooser');
    const chooserList = $('#cChooserList');
    $('#cIndexList').addEventListener('click', ev => {
        const b = ev.target.closest('[data-choose]');
        if (!b) return;
        chooserList.replaceChildren(...$('#' + b.dataset.choose).content.cloneNode(true).children);
        chooserList.selectedIndex = 0;
        chooser.hidden = false;
        chooserList.focus();
    });
    const display = () => {
        const opt = chooserList.selectedOptions[0];
        if (!opt) { chooserList.setCustomValidity(root.dataset.msgSelect); chooserList.reportValidity(); return; }
        frame.src = opt.value;
        chooser.hidden = true;
    };
    $('#cChooserDisplay').addEventListener('click', display);
    chooserList.addEventListener('dblclick', display);
    chooserList.addEventListener('change', () => chooserList.setCustomValidity(''));
    $('#cChooserCancel').addEventListener('click', () => { chooser.hidden = true; });

    // ---------------------------------------------------------------- 検索
    const form = $('#cSearchForm');
    const status = $('#cSearchStatus');
    const results = $('#cSearchResults');
    form.addEventListener('submit', async ev => {
        ev.preventDefault();
        const q = $('#cSearchInput').value.trim();
        results.replaceChildren();
        if (!q) { status.textContent = ''; return; }
        status.textContent = root.dataset.msgSearching;
        try {
            const url = new URL(root.dataset.searchUrl, location.href);
            url.searchParams.set('q', q);
            const res = await fetch(url);
            const hits = await res.json();
            status.textContent = hits.length ? `${hits.length} ${root.dataset.msgFound}` : root.dataset.msgNotfound;
            hits.forEach(h => {
                const li = document.createElement('li');
                const a = document.createElement('a');
                a.href = urlFor(h.id);
                a.target = 'classicFrame';
                a.textContent = h.title;
                li.appendChild(a);
                results.appendChild(li);
            });
        } catch (e) {
            status.textContent = root.dataset.msgNotfound;
        }
    });

    // ---------------------------------------------------------------- 用語集
    const glossary = $('#cGlossaryList');
    if (glossary) {
        glossary.addEventListener('change', () => {
            const opt = glossary.selectedOptions[0];
            $('#cGlossaryTerm').textContent = opt ? opt.textContent : '';
            $('#cGlossaryDef').textContent = opt ? opt.value : '';
        });
    }
})();
