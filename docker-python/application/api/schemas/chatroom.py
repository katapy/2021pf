from typing import List
from pydantic import BaseModel

from .chatmessage import ChatMessage

class ChatRoom(BaseModel):
	room_name: str
	room_id: int
	chat_message: List[ChatMessage] = []
