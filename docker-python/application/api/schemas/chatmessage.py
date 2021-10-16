from pydantic import BaseModel

class ChatMessage(BaseModel):
	chat_id: int
	room_id: int
	email: str
	message: str
	created_date: str
	created_time: str
