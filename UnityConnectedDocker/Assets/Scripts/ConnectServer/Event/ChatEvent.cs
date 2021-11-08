using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ConnectServer
{
    /// <summary>
    /// Chat event class.
    /// </summary>
    public class ChatEvent : MonoBehaviour
    {
        /// <summary>
        /// Model for use invite member API.
        /// </summary>
        private class ModelDict : IModelJsonConvert
        {
            private User user;
            private ChatRoomMember member;

            public ModelDict(User user, ChatRoomMember member)
            {
                this.user = user;
                this.member = member;
            }

            public void JsonToModel(string json)
            {
                throw new System.NotImplementedException();
            }

            public string ToJson()
            {
                return $"{{ \"user\": {user.ToJson()}, \"member\": {member.ToJson()} }}";
            }
        }

        /// <summary>
        /// Input field for input email address of invite user.
        /// </summary>
        [SerializeField]
        private InputField mailInput;

        /// <summary>
        /// controller for invite new member.
        /// </summary>
        [SerializeField]
        private ChatInviteController inviteController;

        /// <summary>
        /// Manage chat function.
        /// </summary>
        private ChatManager chatManager;

        /// <summary>
        /// Main game manager.
        /// </summary>
        private GameManager gameManager;

        // Start is called before the first frame update
        private void Start()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            chatManager = GameObject.Find("ChatManager").GetComponent<ChatManager>();
        }

        /// <summary>
        /// Innvite new member in chat room.
        /// </summary>
        public void InviteMember()
        {
            var member = new ChatRoomMember();
            member.room_id = chatManager.chatRoomTable.ChatRoom.room_id;
            member.chat_id = -1;
            member.email = mailInput.text;
            var modelDict = new ModelDict(gameManager.User, member);
            StartCoroutine(inviteController.Connect(modelDict));
        }
    }
}