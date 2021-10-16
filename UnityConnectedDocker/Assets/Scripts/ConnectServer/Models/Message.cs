using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConnectServer
{
	[Serializable]
	public class Message : IModelJsonConvert
	{
		public string message;

		public string ToJson()
		{
			return JsonUtility.ToJson(this);
		}

		public void JsonToModel(string json)
		{
			var m = JsonUtility.FromJson<Message>(json);

			this.message = m.message;
		}
	}
}
