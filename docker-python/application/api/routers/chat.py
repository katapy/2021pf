
import asyncio

from databases import Database
from fastapi import APIRouter, Depends
from fastapi.encoders import jsonable_encoder
from starlette.websockets import WebSocket
from typing import List

from ..db import get_connection
from ..errors.api_exception import ApiException
from ..errors.error_handler import HandleError
from ..controller.chat.chat_room import Room

from ..schemas.user import User
from ..schemas.message import Message
from ..schemas.chatroom import ChatRoom
from ..schemas.chatroommember import ChatRoomMember

router = APIRouter()

# 接続中のクライアントを識別するためのIDを格納
chat_rooms: List[Room] = []

# WebSockets用のエンドポイント
@router.websocket("/join/{room_id}")
async def websocket_endpoint(ws: WebSocket, room_id: int):
    try:
        # roomの検索
        my_room = next( (r for r in chat_rooms if r.get_room_id()==room_id), None)

        # リストに無い場合はSQLで取得
        if my_room is None:
            my_room = Room()
            await my_room.get_room_data(room_id=room_id)
            chat_rooms.append(my_room)

        # roomに入る
        await my_room.join_room(ws=ws)

    except ApiException as e:
        error_handler = HandleError(e)
        error_handler.write_log()
        return error_handler.error_response()
    
    except Exception as e:
        error_handler = HandleError(e)
        error_handler.write_log()

    finally:
        await  ws.close()

# チャットルーム新規作成
@router.post("/new/{room_name}", \
    response_model=Message, \
        summary="Create new chat room", \
            responses={400: {"model": Message}, 401: {"model": Message}})
async def create_room(room_name: str, user: User, database: Database = Depends(get_connection)):
    try:
        await Room.create(room_name, user=user, database=database)
        return jsonable_encoder(Message(message="Success"))
    except ApiException as e:
        return e.error_response()
    except Exception as e:
        error_handler = HandleError(e)
        error_handler.write_log()
        return error_handler.error_response()


# チャットルーム一覧取得
@router.post("/roomlist", \
    response_model=List[ChatRoom], \
        summary="Get chat room list", \
            responses={400: {"model": Message}, 401: {"model": Message}})
async def create_room(user: User, database: Database = Depends(get_connection)):
    try:
        # await Room.create(room_name, user=user, database=database)
        print("get chat room list")
        return await Room.get_room_list(user=user, database=database)
    except ApiException as e:
        return e.error_response()
    except Exception as e:
        error_handler = HandleError(e)
        error_handler.write_log()
        return error_handler.error_response()
