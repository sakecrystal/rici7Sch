#!/usr/bin/env python3
"""翻訳作業用: 番号付きの訳文 (N<TAB>訳) を英語キーと突き合わせて HelpSource/ja/<name>.json に書き出す。
使い方: python3 tools/apply_translation.py keys.json translations.txt name
"""
import json, sys
from pathlib import Path

keys = json.loads(Path(sys.argv[1]).read_text(encoding="utf-8"))
lines = [l for l in Path(sys.argv[2]).read_text(encoding="utf-8").splitlines() if l.strip()]
result = {}
for line in lines:
    num, _, text = line.partition("\t")
    result[keys[int(num) - 1]] = text.strip()
missing = [i + 1 for i, k in enumerate(keys) if k not in result]
out = Path(__file__).resolve().parent.parent / "HelpSource" / "ja" / f"{sys.argv[3]}.json"
out.write_text(json.dumps(result, ensure_ascii=False, indent=1), encoding="utf-8")
print(f"{out.name}: {len(result)} entries, missing {missing}")
