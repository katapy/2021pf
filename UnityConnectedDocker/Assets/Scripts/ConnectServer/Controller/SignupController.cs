using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ConnectServer
{
    public class SignupController : BaseController
    {
        public bool isSuccess = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override IEnumerator Connect(IModelJsonConvert user)
        {
            ErrMsg = string.Empty;
            yield return StartCoroutine(Post(route, user));

            switch((int)statusCode)
            {
                case 200:
                    isSuccess = true;
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