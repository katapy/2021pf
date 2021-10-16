
# Heroku Test command

URL=https://docker-python-001.herokuapp.com
# PORT=8000

DATA="{ \
    \"name\": \"string\", \
    \"email\": \"string\", \
    \"password\": \"string\", \
    \"created\": \"string\", \
    \"updated\": \"string\", \
    \"chara_id\": 0 \
    }"

# email 変更
DATA2="{ \
    \"name\": \"string\", \
    \"email\": \"str\", \
    \"password\": \"string\", \
    \"created\": \"string\", \
    \"updated\": \"string\", \
    \"chara_id\": 0 \
    }"

# パスワード変更
DATA3="{ \
    \"name\": \"string\", \
    \"email\": \"string\", \
    \"password\": \"str\", \
    \"created\": \"string\", \
    \"updated\": \"string\", \
    \"chara_id\": 0 \
    }"

echo $DATA

USER_TEST_ROUTE1=user/test
echo "\n"
echo "$URL/$USER_TEST_ROUTE1"
curl -X POST -H "Content-Type: application/json" -d \
    "$DATA" \
    $URL/$USER_TEST_ROUTE1

USER_TEST_ROUTE2=user/signup
echo "\n"
echo "$URL/$USER_TEST_ROUTE2"
curl -X POST -H "Content-Type: application/json" -d \
    "$DATA" \
    $URL/$USER_TEST_ROUTE2

USER_TEST_ROUTE3=user/login
echo "\n"
echo "login"
curl -X POST -H "Content-Type: application/json" -d \
    "$DATA" \
    $URL:$PORT/$USER_TEST_ROUTE3

USER_TEST_ROUTE3=user/login
echo "\n"
echo "email変更"
curl -X POST -H "Content-Type: application/json" -d \
    "$DATA2" \
    $URL:$PORT/$USER_TEST_ROUTE3

USER_TEST_ROUTE3=user/login
echo "\n"
echo "password"
curl -X POST -H "Content-Type: application/json" -d \
    "$DATA3" \
    $URL:$PORT/$USER_TEST_ROUTE3

echo "\n"