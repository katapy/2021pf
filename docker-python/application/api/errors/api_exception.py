
from fastapi import HTTPException
from fastapi.encoders import jsonable_encoder
from fastapi.responses import JSONResponse

from ..schemas.message import Message

# APIの処理内で発生する想定内のエラーを処理します。
class ApiException(HTTPException):
    __status_code: int  # HTTPステータスコード
    __message: str      # エラーメッセージ
    def __init__(self, message: str, status_code: int):
        self.__message = message
        self.__status_code = status_code

    # リスポンスを返します。
    def error_response(self):
        message = Message(message=self.__message)
        return JSONResponse(status_code=self.__status_code, content=jsonable_encoder(message))
