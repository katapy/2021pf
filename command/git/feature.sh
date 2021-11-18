#!/bin/bash

# リモートからコードをpullし
# ランダムな4桁の番号のブランチを作成します

# rootフォルダまで移動
cd ../..

URL=git@github.com:katapy/2021pf.git

# リモートが設定されていなければ追加する
ORIGIN=`git remote`
echo "remote: " $ORIGIN
if [ `test "$ORIGIN" != "origin" ; echo $?` -eq 0 ]; then
    echo "Add remote"
    git remote add origin $URL
fi

git pull origin develop

NUMBER=$(($RANDOM % 10000))
USERNAME=`git config user.name`
echo "make ticket: " $NUMBER

git checkout -b "feature-$USERNAME-$NUMBER"
