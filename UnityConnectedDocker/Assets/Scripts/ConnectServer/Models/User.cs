using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConnectServer
{
	[Serializable]
	public class User : IModelJsonConvert
	{
		public string name;
		public string email;
		public string password;
		public string created;
		public string updated;
		public int chara_id;

		public string ToJson()
		{
			return JsonUtility.ToJson(this);
		}

		public void JsonToModel(string json)
		{
			var m = JsonUtility.FromJson<User>(json);

			this.name = m.name;
			this.email = m.email;
			this.password = m.password;
			this.created = m.created;
			this.updated = m.updated;
			this.chara_id = m.chara_id;
		}
	}
}
