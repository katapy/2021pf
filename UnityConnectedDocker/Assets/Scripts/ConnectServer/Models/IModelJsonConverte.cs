using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConnectServer
{
    public interface IModelJsonConvert
    {
        public string ToJson();

        public void JsonToModel(string json);
    }
}