using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConnectServer
{
    public class LoginController : BaseController
    {
        [SerializeField]
        private GameManager GameManager;

        public bool isLogin { get; private set; }

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
            isLogin = false;
            yield return StartCoroutine(Post(route, user));

            switch((int)statusCode)
            {
                case 200:
                    GameManager.User = new User();
                    GameManager.User.JsonToModel(Result);
                    GameManager.User.password = (user as User).password;
                    isLogin = true;
                    DontDestroyOnLoad(GameManager);
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
