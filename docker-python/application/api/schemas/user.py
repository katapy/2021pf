from pydantic import BaseModel

class User(BaseModel):
	name: str
	email: str
	password: str
	created: str
	updated: str
	chara_id: int
