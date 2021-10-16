#!/bin/bash

# rootフォルダまで移動
cd ../..

URL=https://github.com/katapy-note/2021pf.git


BRANCH=`git rev-parse --abbrev-ref HEAD`
git init
# git checkout -b main
git branch -a
git add .
read -p "Input comment > " COMMENT
git commit -m "$COMMENT"
# git push -f --set-upstream $URL main

# git push $URL HEAD:develop
git push $URL HEAD:$BRANCH
