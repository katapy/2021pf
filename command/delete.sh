#!/bin/bash
echo "delete"

# 1つ上の階層へ移動
cd ..
cd docker-python

# DBを全削除
sh ../command/connect_db.sh 'drop schema public cascade;'
sh ../command/connect_db.sh 'create schema public;'

# コンテナのID一覧を取得
IDS=`docker container ls -q`
# スペース区切りで分割
ID_ARR=(${IDS// / }) 
for ID in "${ID_ARR[@]}";
    do
    # コンテナを停止
    docker container stop "$ID"
    # コンテナを削除
    docker container rm "$ID";
done

docker system prune
docker volume rm $(docker volume ls -qf dangling=true)

# コンテナが削除できているか確認
echo "confirm to delete"
docker container ls

echo "delete complete!"
