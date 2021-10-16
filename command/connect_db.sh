#!/bin/bash

# Example
# sh connect_db.sh 'SELECT * FROM users;'

cd ..
cd docker-python

SQL=$1

echo "Excute > $1"
RESULT=`docker exec -it postgresql \
 psql "postgresql://root:root@postgres:5432/testdb" \
 -c "$SQL"`

echo $RESULT
echo $RESULT > ./log/sql_commnand.txt
