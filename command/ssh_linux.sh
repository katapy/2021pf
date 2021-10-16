#!/bin/bash

cd ..

# SSH接続でLinuxと接続する

# hostnameの確認方法
# hostname -I
HOSTNAME=192.168.1.10

# 公開鍵
KEYPUB="./mac/authorized_keys"

# 古いフォルダの削除
echo "delete old file."
ssh katapy@$HOSTNAME "cd mac;rm -rf *;"

# ファイル転送
echo "send file."
tar zcf - ./docker-python | ssh katapy@$HOSTNAME 'tar zxf -'
tar zcf - ./command | ssh katapy@$HOSTNAME 'tar zxf -'

echo "run command"
ssh katapy@$HOSTNAME "mv ./docker-python ./mac;mv ./command ./mac;"

# リモートに接続
ssh katapy@$HOSTNAME

