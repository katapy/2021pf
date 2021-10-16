#!/bin/bash

# 公開鍵作成ファイル

# hostnameの確認方法
# hostname -I
HOSTNAME=192.168.1.10

cd \

# クライアントPCで鍵を作る
ssh-keygen -t rsa -b 4096

# リモートへ公開鍵を転送する
ssh-copy-id -i ~/.ssh/id_rsa.pub katapy@$HOSTNAME

# リモートにSSHで接続する
ssh -i id_rsa katapy@$HOSTNAME