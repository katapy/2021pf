from pydantic import BaseModel

class ChatRoomMember(BaseModel):
	room_id: int
	chat_id: int
	email: str
	update: str
