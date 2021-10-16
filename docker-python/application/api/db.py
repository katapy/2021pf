import os
import datetime

from databases import Database, DatabaseURL
from sqlalchemy import create_engine, MetaData
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import Session, sessionmaker, scoped_session
from starlette.requests import Request

# ローカルのデータベースと接続する場合
# DATABASE_URL = os.environ.get('DATABASE_URL') or "postgresql://localhost/testdb"
# DockerまたはHeroku上のDBと接続する場合
DATABASE_URL = os.environ.get('DATABASE_URL') or "postgresql://root:root@postgres:5432/testdb"
# URLの置換処理 (Heroku用)
DATABASE_URL = DATABASE_URL.replace('postgres:', 'postgresql:')

print('\n\n')
print('*********************')
print('--- set databases ---')
print('file:    ', __file__)
print('time:    ',datetime.datetime.now())
database = Database(DATABASE_URL, min_size=5, max_size=20)

ECHO_LOG = False

engine = create_engine(DATABASE_URL, echo=ECHO_LOG)
session = Session(engine, future=True)

metadata = MetaData()

# modelで使用する
Base = declarative_base()

# middlewareでrequestに格納したconnection(Databaseオブジェクト)を返します。
def get_connection(request: Request):
    return request.state.connection

print('*********************')
