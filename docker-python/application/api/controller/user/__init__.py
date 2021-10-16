
import asyncio
import datetime

from sqlalchemy.sql.expression import false, true, update

from asyncpg.exceptions import UniqueViolationError
from passlib.context import CryptContext

from ...models.users import users
from ...schemas.user import User
from ...errors.api_exception import ApiException

pwd_context = CryptContext(schemes=["bcrypt"], deprecated="auto")

# 入力したパスワード（平文）をハッシュ化して返します。
def get_users_insert_dict(user: User):
    values = User(
        name=user.name,
        email=user.email,
        password=pwd_context.hash(user.password),
        created=datetime.datetime.now().strftime('%Y%m%d'),
        updated=datetime.datetime.now().strftime('%Y%m%d'),
        chara_id=user.chara_id
    )
    return values.dict()

async def signup(user: User, database):
    try:
        query = users.insert()
        hashed_user = get_users_insert_dict(user)
        await database.execute(query, hashed_user)
    except UniqueViolationError as e:
        # SQLのユニーク制約によるエラー、今回はmailが対象
        raise ApiException('Email Already Exist.', 400)

async def login(user: User, database):
    get_query = users.select().where(users.columns.email==user.email)
    _user = await database.fetch_one(get_query)
    if _user is None:
        raise ApiException('Email or password is wrong.', 401)
    if not pwd_context.verify(user.password, _user["password"]):
        raise ApiException('Email or password is wrong.', 401)

    # 更新日をupdateする
    update_query = users.update().\
        where(users.columns.email==user.email).\
            values(updated=datetime.datetime.now().strftime('%Y%m%d'))
    await database.execute(update_query)
    user.name = _user["name"]
    user.updated = datetime.datetime.now().strftime('%Y%m%d')
    user.created = _user["created"]
    user.password = "********"
    user.chara_id = _user["chara_id"]
    return user
