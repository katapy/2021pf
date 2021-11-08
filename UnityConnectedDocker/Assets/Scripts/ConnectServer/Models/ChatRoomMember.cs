using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConnectServer
{
	[Serializable]
	public class ChatRoomMember : IModelJsonConvert
	{
		public int room_id;
		public int chat_id;
		public string email;
		public string update;

		public string ToJson()
		{
			return JsonUtility.ToJson(this);
		}

		public void JsonToModel(string json)
		{
			var m = JsonUtility.FromJson<ChatRoomMember>(json);

			this.room_id = m.room_id;
			this.chat_id = m.chat_id;
			this.email = m.email;
			this.update = m.update;
		}
	}
}
