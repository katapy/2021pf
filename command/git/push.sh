#!/bin/bash

# rootフォルダまで移動
cd ../..

URL=https://github.com/katapy-note/2021pf.git
BRANCH=`git rev-parse --abbrev-ref HEAD`

# キャッシュの削除(ファイルを削除した時に使う)
read -p "Delete cashed? (Y/n): " DATA
case "$DATA" in
    # Y(es)の時はキャッシュを削除
    [Y]) git rm -r --cached .
esac

git init
# git branch -a
git add .
read -p "Input comment > " COMMENT
git commit -m "$COMMENT"
git push $URL HEAD:$BRANCH

echo "Complete to push"
echo "Your branch is " $BRANCH
