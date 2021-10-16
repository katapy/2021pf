
import datetime

from fastapi.encoders import jsonable_encoder
from fastapi.responses import JSONResponse

# 想定外のエラーを処理するクラス
class HandleError():
    __exp: Exception                # 例外クラス
    __message = "Unexpected error"  # エラーメッセージ
    def __init__(self, exception: Exception):
        self.__exp = exception

    # エラーログを出力します。
    def write_log(self):
        print("************************")
        print("--- Unexpected Error ---")
        print('Time: {0}'.format(datetime.datetime.now()))
        print('File:    ', __file__)
        print('Type: {0}'.format(type(self.__exp)))
        print(self.__exp)
        print("************************")

    # エラー時のリスポンス
    def error_response(self):
        return JSONResponse(status_code=400, content=jsonable_encoder(self.__message))
