using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

using Config;

namespace ConnectServer
{
    public abstract class BaseController : MonoBehaviour
    {
        [SerializeField]
        protected string route;

        protected long statusCode;

        public string Result { get; private set; }

        public string ErrMsg;

        public abstract IEnumerator Connect(IModelJsonConvert model);

        protected IEnumerator Post(string route, IModelJsonConvert model)
        {
            var request = new UnityWebRequest(Config.Config.URL + route, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(model.ToJson());

            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            statusCode = request.responseCode;
            Result = request.downloadHandler.text;
        }
    }
}