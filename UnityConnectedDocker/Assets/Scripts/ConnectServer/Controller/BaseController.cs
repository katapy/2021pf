using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

using Config;

namespace ConnectServer
{
    /// <summary>
    /// Base connect server by post.
    /// </summary>
    public abstract class BaseController : MonoBehaviour
    {
        /// <summary>
        /// route path.
        /// </summary>
        [SerializeField]
        protected string route;

        /// <summary>
        /// Status code of post result.
        /// </summary>
        protected long statusCode;

        /// <summary>
        /// Result of post.
        /// </summary>
        public string Result { get; private set; }

        /// <summary>
        /// Error message.
        /// </summary>
        public string ErrMsg;

        /// <summary>
        /// Connect with API.
        /// </summary>
        /// <param name="model">
        /// Send parameter.
        /// </param>
        /// <returns></returns>
        public abstract IEnumerator Connect(IModelJsonConvert model);

        /// <summary>
        /// Post with API.
        /// </summary>
        /// <param name="path">
        /// Server path.
        /// </param>
        /// <param name="model">
        /// Send parameter.
        /// </param>
        /// <returns></returns>
        protected IEnumerator Post(string path, IModelJsonConvert model)
        {
            var request = new UnityWebRequest(Config.Config.URL + path, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(model.ToJson());

            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            statusCode = request.responseCode;
            Result = request.downloadHandler.text;
        }

        /// <summary>
        /// Stop post after 2 sec.
        /// </summary>
        protected IEnumerator StopPost(IEnumerator enumerator)
        {
            yield return new WaitForSeconds(2);
            ErrMsg = "Time out error.";
            StopCoroutine(enumerator);
        }
    }
}