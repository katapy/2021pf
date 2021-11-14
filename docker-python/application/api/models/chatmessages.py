import sqlalchemy
from ..db import metadata, engine

chatmessages = sqlalchemy.Table(
	"chatmessages",
	metadata,
	sqlalchemy.Column("chat_id", sqlalchemy.Integer, nullable=False, primary_key=True, autoincrement=True),
	sqlalchemy.Column("room_id", sqlalchemy.Integer, nullable=False, index=True),
	sqlalchemy.Column("email", sqlalchemy.String, nullable=False, index=True),
	sqlalchemy.Column("message", sqlalchemy.String, nullable=False, index=True),
	sqlalchemy.Column("created_date", sqlalchemy.String, nullable=False, index=True),
	sqlalchemy.Column("created_time", sqlalchemy.String, nullable=False, index=True),
)
