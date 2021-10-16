#!bin/bash

cd ..
cd docker-python
NAMES=`docker ps --format '{{.Names}}'`
echo $NAMES
NAME_ARR=(${NAMES// / })
NO=0
echo "Select container"
for NAME in "${NAME_ARR[@]}";do
    echo "$NO. $NAME";
    let NO++
done

read -p "input? > " INPUT

echo '*** Log making ***'

if [ ! -d log/ ]; then
    mkdir 'log'
fi

echo '***　Log　***' >> ./log/${NAME_ARR[$INPUT]}.log

echo `docker logs ${NAME_ARR[$INPUT]}` | \
 sed -E "s/"$'\E'"\[([0-9]{1,2}(;[0-9]{1,2})*)?m//g" \
 >> ./log/${NAME_ARR[$INPUT]}.log

docker logs ${NAME_ARR[$INPUT]}
