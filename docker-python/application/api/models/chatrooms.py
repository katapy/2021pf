import sqlalchemy
from ..db import metadata, engine

chatrooms = sqlalchemy.Table(
	"chatrooms",
	metadata,
	sqlalchemy.Column("room_name", sqlalchemy.String),
	sqlalchemy.Column("room_id", sqlalchemy.Integer, primary_key=True, autoincrement=True),
)
