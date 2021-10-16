using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConnectServer
{
	[Serializable]
	public class ChatRoom : IModelJsonConvert
	{
		public string room_name;
		public int room_id;

		public string ToJson()
		{
			return JsonUtility.ToJson(this);
		}

		public void JsonToModel(string json)
		{
			var m = JsonUtility.FromJson<ChatRoom>(json);

		}
	}
}
