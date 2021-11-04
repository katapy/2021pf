using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace ConnectServer
{
    public class ChatRoomListController : BaseController
    {
        public List<ChatRoom> ChatRooms{get; private set;}

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        /// <summary>
        /// Connect API server.
        /// </summary>
        /// <param name="user"> User info. </param>
        /// <returns></returns>
        public override IEnumerator Connect(IModelJsonConvert user)
        {
            ErrMsg = string.Empty;
            yield return StartCoroutine(Post(route, user));
            ChatRooms = new List<ChatRoom>();

            switch((int)statusCode)
            {
                case 200:
                    ChatRooms = JsonConvert.DeserializeObject<List<ChatRoom>>(Result);
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
