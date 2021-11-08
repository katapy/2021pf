
# API Test command

# URL=127.0.0.1 #Local
URL=192.168.1.10 #オンプレ
PORT=8000

# 入力文字によって処理を条件分岐
echo "0: Local"
echo "1: On premises"
read -p "Select environment: " DATA
case "$DATA" in
    # Local
    [0]) URL=127.0.0.1 ; #Local
esac


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
echo ""
echo "$URL:$PORT/$USER_TEST_ROUTE1"
curl -X POST -H "Content-Type: application/json" -d \
    "$DATA" \
    $URL:$PORT/$USER_TEST_ROUTE1

USER_TEST_ROUTE2=user/signup
echo ""
echo "$URL:$PORT/$USER_TEST_ROUTE2"
curl -X POST -H "Content-Type: application/json" -d \
    "$DATA" \
    $URL:$PORT/$USER_TEST_ROUTE2

USER_TEST_ROUTE3=user/login
echo ""
echo "login"
curl -X POST -H "Content-Type: application/json" -d \
    "$DATA" \
    $URL:$PORT/$USER_TEST_ROUTE3

USER_TEST_ROUTE3=user/login
echo ""
echo "email変更"
curl -X POST -H "Content-Type: application/json" -d \
    "$DATA2" \
    $URL:$PORT/$USER_TEST_ROUTE3

USER_TEST_ROUTE3=user/login
echo ""
echo "password不一致"
curl -X POST -H "Content-Type: application/json" -d \
    "$DATA3" \
    $URL:$PORT/$USER_TEST_ROUTE3

echo ""
echo "チャットルーム作成"
curl -X POST -H "Content-Type: application/json" -d \
    "$DATA" \
    $URL:$PORT/chat/new/test001

MEMBER="\"member\": {\
    \"room_id\": 1,\
    \"chat_id\": -1,\
    \"email\": \"test_friend\",\
    \"update\": \"string\"\
  }"

echo ""
echo "メンバー招待"
curl -X POST -H "Content-Type: application/json" -d \
    "{$MEMBER , \"user\":$DATA }" \
    $URL:$PORT/chat/invite

echo ""
echo "チャットルーム一覧"
curl -X POST -H "Content-Type: application/json" -d \
    "$DATA" \
    $URL:$PORT/chat/roomlist

echo ""

