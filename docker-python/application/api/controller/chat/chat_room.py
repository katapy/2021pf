
"""
test
wscat -c ws://localhost:8000/chat/join/1
{"name":"","email":"string","password":"string","created":"","updated":"","chara_id":0}
"""

from sqlalchemy.sql.expression import update
import crud
import datetime
import json
from starlette.websockets import WebSocket
from sqlalchemy.sql import text
from typing import List

from ...db import database, session
from ...schemas.user import User
from ...schemas.chatmessage import ChatMessage
from ...schemas.chatroommember import ChatRoomMember
from ...schemas.chatroom import ChatRoom
from ...models.chatrooms import chatrooms
from ...models.chatroommembers import chatroommembers
from ...models.chatmessages import  chatmessages
from ...errors.api_exception import ApiException
from ..user import login


class Room:
    __room_id: int
    __room_name: str
    __clients = {}
    __is_init = False

    def __init__(self):
        pass

    async def get_room_data(self, room_id):
        if not self.__is_init:
            get_query = chatrooms.select().where(chatrooms.columns.room_id==room_id)
            _room = await database.fetch_one(get_query)
            self.__room_id = _room["room_id"]
            self.__room_name = _room["room_name"]
            self.__is_init = True


    def get_room_id(self):
        return self.__room_id

    # chatroomの新規作成
    @staticmethod
    async def create(room_name: str, user: User, database):
        try:
            session.begin()
            room_sql = chatrooms.insert().values(room_name=room_name)
            session.execute(room_sql)

            # room_idの取得
            session.flush()
            t = text('SELECT LASTVAL();')
            for r in session.execute(t):
                id = int(r[0])

            # ChatRoomMember作成
            member_sql = chatroommembers.insert().\
                values(room_id=id, chat_id=0, email=user.email, \
                    update=datetime.datetime.now().strftime('%Y%m%d'))
            session.execute(member_sql)

            session.commit()
        except Exception as e:
            raise Exception(e)

    # chatroomの取得
    @staticmethod
    async def get_room_list(user: User, database):
        room_list: List[ChatRoom] = []
        user = await login(user=user, database=database)
        get_query = chatroommembers.select().where(chatroommembers.columns.email==user.email)
        members = await database.fetch_all(get_query)
        for member in members:
            query = chatrooms.select().where(chatrooms.columns.room_id==member["room_id"])
            room = await database.fetch_one(query)
            room_list.append(ChatRoom(room_id=room["room_id"], room_name=room["room_name"]))
        return room_list
    
    @staticmethod
    async def invite_member(member: ChatRoomMember, user: User, database):
        user = await login(user=user, database=database)
        get_query = chatroommembers.select()\
            .where(chatroommembers.columns.email==user.email and chatroommembers.columns.room_id==member.room_id)
        my_member = await database.fetch_one(get_query)
        if my_member is None:
            raise ApiException("You cannot invite this room.")
        insert_query = chatroommembers.insert()
        await database.execute(insert_query, member.dict())

    async def join_room(self, ws: WebSocket, room_id: int):
        try:
            await ws.accept()

            # 認証
            user_json = await ws.receive_text()
            user = User.parse_raw(user_json)
            user = await login(user=user, database=database)
            get_query = chatroommembers.select()\
                .where(chatroommembers.columns.email==user.email and chatroommembers.columns.room_id==self.__room_id)
            my_member = await database.fetch_one(get_query)
            if my_member is None:
                raise ApiException("You cannot join this room.")

            # チャット開始！！
            self.__clients[user.email] = ws
            await ws.send_text("Success to join room")
            
            # 今までのチャットを送信
            try:
                await self.get_room_data(room_id=room_id)
                get_query = chatmessages.select().where(chatmessages.columns.room_id==self.__room_id)
                # get_query = chatmessages.select()
                messages = await database.fetch_all(get_query)
                for message in messages:
                    await ws.send_text(json.dumps(dict(message.items())))
            except Exception as e:
                print("message error")
                print(e)
                await ws.send_text("send error")


            while True:
                chat_json = await ws.receive_text()
                if chat_json == "quit": # quit で退出
                    break
                
                try:
                    for client in self.__clients.values():
                        await client.send_text(chat_json)
                        
                    # チャットの内容を保存
                    save_query = chatmessages.insert()
                    chat = json.loads(chat_json)
                    chat.pop('chat_id')
                    await database.execute(save_query, chat)
                except Exception as e:
                    print("message error")
                    print(e)
                    await ws.send_text("send error")

        except ApiException as e:
            await ws.send_text("Email or password missing")
            raise ApiException(e)

        except Exception as e:
            raise ApiException(e)

        finally:
            pass
            """
            if user is not None:
                client.pop(user.email)
            """
            # self.__clients.pop(user.email)
            # await ws.close()
