import sqlalchemy
from ..db import metadata, engine

users = sqlalchemy.Table(
	"users",
	metadata,
	sqlalchemy.Column("name", sqlalchemy.String, nullable=False, index=True),
	sqlalchemy.Column("email", sqlalchemy.String, nullable=False, primary_key=True, index=True),
	sqlalchemy.Column("password", sqlalchemy.String, nullable=False),
	sqlalchemy.Column("created", sqlalchemy.String, nullable=False, index=True),
	sqlalchemy.Column("updated", sqlalchemy.String, nullable=False, index=True),
	sqlalchemy.Column("chara_id", sqlalchemy.Integer, nullable=False, index=True),
)
