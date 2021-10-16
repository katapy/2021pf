
import asyncio
from fastapi import APIRouter, Depends
from fastapi.encoders import jsonable_encoder
from databases import Database

from ..db import get_connection
from ..schemas.user import User
from ..schemas.message import Message
from ..controller.user import login, signup
from ..errors.api_exception import ApiException
from ..errors.error_handler import HandleError

router = APIRouter()

@router.post("/test")
async def test(user: User):
    return {"test", "success"}

@router.post("/signup", \
    response_model=Message, \
        summary="Login", \
            responses={400: {"model": Message}})
async def users_create(user: User, database: Database = Depends(get_connection)):
    try:
        await signup(user=user, database=database)
        return jsonable_encoder(Message(message="Success"))
    except ApiException as e:
        return e.error_response()
    except Exception as e:
        error_handler = HandleError(e)
        error_handler.write_log()
        return error_handler.error_response()

@router.post("/login", \
    response_model=User, \
        summary="Login", \
            responses={400: {"model": Message}, 401: {"model": Message}})
async def login_user(user: User, database: Database = Depends(get_connection)):
    try:
        return await login(user=user, database=database)
    except ApiException as e:
        return e.error_response()
    except Exception as e:
        error_handler = HandleError(e)
        error_handler.write_log()
        return error_handler.error_response()
