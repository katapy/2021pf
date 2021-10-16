using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ConnectServer
{
    public class ChatView : MonoBehaviour
    {
        [SerializeField]
        private Button sendButton;

        [SerializeField]
        private InputField chatInputText;

        [SerializeField]
        private GameObject sampleMessagePanel;

        private ChatManager chatManager;

        // Start is called before the first frame update
        void Start()
        {
            chatManager = GameObject.Find("ChatManager").GetComponent<ChatManager>();

            sendButton.onClick.AddListener(SendMessage);
        }

        // Update is called once per frame
        void Update()
        {
            ShowChat();
        }

        private void SendMessage()
        {
            chatManager.Send(chatInputText.text);
        }

        private void ShowChat()
        {
            if(chatManager?.UnreadMessages?.Count > 0)
            {
                var clone = GameObject.Instantiate(sampleMessagePanel, sampleMessagePanel.transform.parent);
                clone.GetComponentInChildren<Text>().text = chatManager.UnreadMessages[ 0 ].message;
                chatManager.UnreadMessages.RemoveAt(0);
            }
        }
    }
}