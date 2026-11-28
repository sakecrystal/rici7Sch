#!/usr/bin/env bash
# Claude Code のセッション (VS Code 拡張機能が起動する claude プロセス) を一覧表示・停止する。
#
#   scripts/claude-sessions.sh list            一覧を表示
#   scripts/claude-sessions.sh stop-old [-y]   最新以外のセッションを停止 (-y で確認なし)
#   scripts/claude-sessions.sh stop PID [-y]   指定したセッションを停止
#   scripts/claude-sessions.sh stop-app [-y]   残っている rici7Sch アプリ (ポート 5276 など) を停止
#
# 安全のため、次のプロセスは stop-old の対象にしない:
#   - 開始時刻が最も新しいセッション (いま使っている可能性が高い)
#   - このスクリプトを実行しているセッション自身 (Claude から実行した場合)
set -u

PATTERN='native-binary/claude'
APP_PATTERN='rici7Sch/bin/.*/rici7Sch.dll'

# このスクリプトの祖先プロセスの PID 一覧
ancestors() {
    local p=$$
    while [ -n "$p" ] && [ "$p" -gt 1 ] 2>/dev/null; do
        echo "$p"
        p=$(ps -o ppid= -p "$p" 2>/dev/null | tr -d ' ')
    done
}

# 「開始時刻(epoch) PID」を古い順に出力
sessions() {
    local pid
    for pid in $(pgrep -f "$PATTERN"); do
        [ -r "/proc/$pid/stat" ] || continue
        # 子プロセス (claude が起動したシェル等) は除外し、claude 本体だけを数える
        ps -o cmd= -p "$pid" | grep -q "^[^ ]*$PATTERN" || continue
        echo "$(date -d "$(ps -o lstart= -p "$pid")" +%s) $pid"
    done | sort -n
}

show() {
    local list self newest
    list=$(sessions)
    if [ -z "$list" ]; then
        echo "Claude のセッションは動いていません。"
        return
    fi
    self=$(ancestors | tr '\n' ' ')
    newest=$(echo "$list" | tail -1 | cut -d' ' -f2)
    printf '%-8s %-20s %-12s %s\n' PID 開始時刻 経過時間 状態
    echo "$list" | while read -r _ pid; do
        local mark=""
        [ "$pid" = "$newest" ] && mark="最新"
        case " $self " in *" $pid "*) mark="${mark:+$mark・}このスクリプトの実行元";; esac
        printf '%-8s %-20s %-12s %s\n' "$pid" \
            "$(date -d "$(ps -o lstart= -p "$pid")" '+%Y-%m-%d %H:%M:%S')" \
            "$(ps -o etime= -p "$pid" | tr -d ' ')" "${mark:-古い}"
    done
}

confirm() {
    [ "${YES:-0}" = 1 ] && return 0
    read -r -p "$1 [y/N] " ans
    [ "$ans" = y ] || [ "$ans" = Y ]
}

# SIGTERM で止め、5 秒たっても残っていれば SIGKILL
terminate() {
    local pid=$1 i
    kill -TERM "$pid" 2>/dev/null || { echo "PID $pid は既に終了しています。"; return; }
    for i in 1 2 3 4 5; do
        kill -0 "$pid" 2>/dev/null || { echo "PID $pid を停止しました。"; return; }
        sleep 1
    done
    kill -KILL "$pid" 2>/dev/null && echo "PID $pid を強制終了しました。"
}

YES=0
args=()
for a in "$@"; do
    if [ "$a" = "-y" ]; then YES=1; else args+=("$a"); fi
done
cmd=${args[0]:-list}

case "$cmd" in
    list)
        show
        ;;
    stop-old)
        list=$(sessions)
        newest=$(echo "$list" | tail -1 | cut -d' ' -f2)
        self=" $(ancestors | tr '\n' ' ') "
        targets=$(echo "$list" | while read -r _ pid; do
            [ -z "$pid" ] && continue
            [ "$pid" = "$newest" ] && continue
            case "$self" in *" $pid "*) continue;; esac
            echo "$pid"
        done)
        if [ -z "$targets" ]; then
            echo "止める古いセッションはありません。"
            show
            exit 0
        fi
        show
        echo
        confirm "古いセッション ($(echo $targets)) を停止しますか?" || { echo "中止しました。"; exit 1; }
        for pid in $targets; do terminate "$pid"; done
        ;;
    stop)
        pid=${args[1]:-}
        if ! echo "$pid" | grep -qE '^[0-9]+$' || ! sessions | grep -q " $pid$"; then
            echo "Claude のセッションの PID を指定してください。" >&2
            show
            exit 1
        fi
        case " $(ancestors | tr '\n' ' ') " in
            *" $pid "*) echo "PID $pid はこのスクリプトを実行しているセッション自身なので停止しません。" >&2; exit 1;;
        esac
        confirm "PID $pid のセッションを停止しますか?" || { echo "中止しました。"; exit 1; }
        terminate "$pid"
        ;;
    stop-app)
        pids=$(pgrep -f "$APP_PATTERN")
        if [ -z "$pids" ]; then
            echo "動いている rici7Sch アプリはありません。"
            exit 0
        fi
        for pid in $pids; do
            ports=$(ss -ltnpH 2>/dev/null | grep "pid=$pid," | awk '{print $4}' | sed 's/.*://' | sort -u | tr '\n' ' ')
            echo "PID $pid  ポート: ${ports:-なし}  $(ps -o etime= -p "$pid" | tr -d ' ') 経過"
        done
        confirm "これらのアプリを停止しますか?" || { echo "中止しました。"; exit 1; }
        for pid in $pids; do terminate "$pid"; done
        ;;
    *)
        sed -n '2,12p' "$0" | sed 's/^# \{0,1\}//'
        exit 1
        ;;
esac
