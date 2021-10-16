#!/bin/bash

# 1つ上の階層へ移動
cd ..
cd docker-python

# 入力文字によって処理を条件分岐
read -p "Delete container? (Y/n): " DATA
case "$DATA" in
    # Y(es)の時はコンテナを一旦削除
    [Y]) bash ../command/delete.sh ;;
    # その他が入力されれば削除しない
    *) echo "Up to date container."
esac

# コンテナをビルド
docker-compose up --build -d
