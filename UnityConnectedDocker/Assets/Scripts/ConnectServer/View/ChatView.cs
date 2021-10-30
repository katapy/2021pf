using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ConnectServer
{
    /// <summary>
    /// Control view where chat scene.
    /// </summary>
    public class ChatView : MonoBehaviour
    {
        /// <summary>
        /// Send button.
        /// </summary>
        [SerializeField]
        private Button sendButton;

        /// <summary>
        /// Input Text on chat message.
        /// </summary>
        [SerializeField]
        private InputField chatInputText;

        /// <summary>
        /// Sample od message pannel.
        /// </summary>
        [SerializeField]
        private GameObject sampleMessagePanel;

        /// <summary>
        /// Manage chat function.
        /// </summary>
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

        /// <summary>
        /// Send message on input.
        /// </summary>
        private void SendMessage()
        {
            chatManager.Send(chatInputText.text);
            chatInputText.text = "";
        }

        /// <summary>
        /// Show chat message.
        /// </summary>
        private void ShowChat()
        {
            if(chatManager?.UnreadMessages?.Count > 0)
            {
                var clone = GameObject.Instantiate(sampleMessagePanel, sampleMessagePanel.transform.parent);
                clone.GetComponentInChildren<Text>().text = chatManager.UnreadMessages[ 0 ].message;
                clone.gameObject.SetActive(true);
                chatManager.UnreadMessages.RemoveAt(0);
            }
        }
    }
}