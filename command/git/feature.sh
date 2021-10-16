#!/bin/bash

# rootフォルダまで移動
cd ../..

URL=https://github.com/katapy-note/2021pf.git

# リモートが設定されていなければ追加する
ORIGIN=`git remote`
echo "remote: " $ORIGIN
if [ `test "$ORIGIN" != "origin" ; echo $?` -eq 0 ]; then
    echo "Add remote"
    git remote add origin $URL
fi

git pull origin main

NUMBER=$(($RANDOM % 10000))
echo "make ticket: " $NUMBER

git checkout -b "feature-$NUMBER"
