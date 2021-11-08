using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConnectServer
{
    /// <summary>
    /// Controller for invite new member.
    /// </summary>
    public class ChatInviteController : BaseController
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override IEnumerator Connect(IModelJsonConvert dict)
        {
            ErrMsg = string.Empty;
            yield return StartCoroutine(Post(route, dict));

            switch ((int)statusCode)
            {
                case 200:
                    break;
                case 400:
                case 401:
                    var message = new Message();
                    message.JsonToModel(Result);
                    ErrMsg = message.message;
                    break;
                case 422:
                    ErrMsg = "Validation Error";
                    break;
                default:
                    ErrMsg = "Unexpected Error";
                    break;
            }
        }
    }
}
