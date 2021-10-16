import sqlalchemy
from ..db import metadata, engine

chatroommembers = sqlalchemy.Table(
	"chatroommembers",
	metadata,
	sqlalchemy.Column("room_id", sqlalchemy.Integer, nullable=False),
	sqlalchemy.Column("chat_id", sqlalchemy.Integer, nullable=False),
	sqlalchemy.Column("email", sqlalchemy.String, nullable=False),
	sqlalchemy.Column("update", sqlalchemy.String, nullable=False),
)
