using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace ConnectServer
{
    public class ChatRoomListController : BaseController
    {
        public List<ChatRoom> chatRooms{get; private set;}

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
            chatRooms = new List<ChatRoom>();

            switch((int)statusCode)
            {
                case 200:
                    chatRooms = JsonConvert.DeserializeObject<List<ChatRoom>>(Result);
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

            /*
            var roomList = JsonConvert.DeserializeObject<List<ChatRoom>>(Result);
            foreach(var room in roomList)
            {
                Debug.Log(room.room_name);
            }
            */
        }
    }
}
