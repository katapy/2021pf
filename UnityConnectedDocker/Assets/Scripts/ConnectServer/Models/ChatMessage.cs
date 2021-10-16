using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConnectServer
{
	[Serializable]
	public class ChatMessage : IModelJsonConvert
	{
		public int chat_id;
		public int room_id;
		public string email;
		public string message;
		public string created_date;
		public string created_time;

		public string ToJson()
		{
			return JsonUtility.ToJson(this);
		}

		public void JsonToModel(string json)
		{
			var m = JsonUtility.FromJson<ChatMessage>(json);

			this.chat_id = m.chat_id;
			this.room_id = m.room_id;
			this.email = m.email;
			this.message = m.message;
			this.created_date = m.created_date;
			this.created_time = m.created_time;
		}
	}
}
