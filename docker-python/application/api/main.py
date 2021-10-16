"""
main.py
Main api class.
"""
# ref
## fastAPI on docker
## https://qiita.com/satto_sann/items/fcd3832a1fea2c607b85
## postgreSQL on docker
## https://iteng-pom.com/archives/1075

# cd ..
# bash start.sh

# Connect DB on command
# docker exec -it postgresql /bin/bash
# psql -h postgres -p 5432 -U root -d testdb
# テーブル一覧
# \dt;

from fastapi import FastAPI
from fastapi.middleware.cors import CORSMiddleware
from starlette.requests import Request

from .db import Base, database, metadata, engine
from ..api import routers

def get_application():
    app = FastAPI(title="FastAPI to Unity connection", version="1.0.0")

    app.add_middleware(
        CORSMiddleware,
        allow_origins=["*"],
        allow_credentials=True,
        allow_methods=["*"],
        allow_headers=["*"],
    )

    app.include_router(routers.user_router, prefix="/user")
    app.include_router(routers.chat_router, prefix="/chat")

    return app

app = get_application()

@app.get("/")
def read_root():
    return {"Hello": "World"}

# 起動時にDatabaseに接続する。
@app.on_event("startup")
async def startup():
    print("*****************")
    print("--- Connent DB---")
    try:
        await database.connect()
    except Exception as e:
        print(e)
    try:
        metadata.create_all(bind=engine)
    except Exception as e:
        print(e)
    print("*****************")

# 終了時にDatabaseを切断する。
@app.on_event("shutdown")
async def shutdown():
    await database.disconnect()

# middleware state.connectionにdatabaseオブジェクトをセットする。
@app.middleware("http")
async def db_session_middleware(request: Request, call_next):
    request.state.connection = database
    response = await call_next(request)
    return response
