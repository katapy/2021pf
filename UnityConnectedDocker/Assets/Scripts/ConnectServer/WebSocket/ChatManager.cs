using System.Security.Cryptography.X509Certificates;
using System.Net.WebSockets;
/// <summary>
/// WebSocketManager
/// </summary>
/// ref
/// https://qiita.com/nisei275/items/a60cb3d3e97aeb429fa7

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WebSocketSharp;

namespace ConnectServer
{
    /// <summary>
    /// Manage chat function.
    /// </summary>
    public class ChatManager : MonoBehaviour
    {
        WebSocketSharp.WebSocket ws = null;

        /// <summary>
        /// Is connect server.
        /// </summary>
        public static bool isConnect{get; private set;}

        /// <summary>
        /// Chat room data table.
        /// </summary>
        public ChatRoomTable chatRoomTable{get; private set;}

        /// <summary>
        /// Unread messages.
        /// </summary>
        public List<ChatMessage> UnreadMessages = new List<ChatMessage>();

        /// <summary>
        /// Game Manager.
        /// </summary>
        private GameManager gameManager;

        // Start is called before the first frame update
        void Start()
        {
            isConnect = false;

            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        /// <summary>
        /// Join chat room
        /// </summary>
        /// <param name="chatRoomTable">Chat room</param>
        public void JoinRoom(ChatRoomTable chatRoomTable)
        {
            if(isConnect)
            {
                return;
            }

            this.chatRoomTable = chatRoomTable;

            // 接続先のURLとポート番号を指定する
            ws = new WebSocketSharp.WebSocket($"{Config.Config.WS}/chat/join/{chatRoomTable.ChatRoom.room_id}");

            // WebSocketの接続を開始した時に実行されるイベント
            ws.OnOpen += (sender, e) => OnOpen();
            // メッセージを受信した時に実行されるイベント
            ws.OnMessage += (sender, e) => GetMessage(sender, e);
            // 接続に失敗した時に実行されるイベント
            ws.OnError += (sender, e) => Debug.Log("WebSocket Error Message: " + e.Message);
            // 接続を終了した時に実行されるイベント
            ws.OnClose += (sender, e) => Debug.Log("WebSocket Close");

            // WebSocketの接続を開始
            ws.Connect();

            isConnect = true;

            DontDestroyOnLoad(this);
            SceneManager.LoadScene("Chat");
        }

        /// <summary>
        /// Close ws connection when this is delete.
        /// </summary>
        void OnDestroy()
        {
            // ゲームオブジェクトが削除されたときにWebSocketの接続を閉じる
            if(ws != null)
            {
                ws.Send("quit");
                ws.Close();
                ws = null;
            }

            SceneManager.LoadScene("ChatRoomSelect");
        }

        /// <summary>
        /// This method start when open WS.
        /// </summary>
        private void OnOpen()
        {
            Debug.Log("WebSocket Open");
            ws.Send(gameManager.User.ToJson());
        }

        /// <summary>
        /// Send message.
        /// </summary>
        /// <param name="message">
        /// message.
        /// </param>
        public void Send(string message)
        {
            var chatMessage = new ChatMessage();
            chatMessage.room_id = chatRoomTable.ChatRoom.room_id;
            chatMessage.email = gameManager.User.email;
            chatMessage.message = message;

            var time = DateTime.Now;
            chatMessage.created_date = time.ToString("yyyyMMdd");
            chatMessage.created_time = time.ToString("HHmmss");

            Debug.Log(chatMessage.ToJson());
            ws.Send(chatMessage.ToJson());
        }

        /// <summary>
        /// Get message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetMessage(object sender, MessageEventArgs e)
        {
            var data = e.Data;

            try
            {
                var chatMessage = new ChatMessage();
                chatMessage.JsonToModel(data);
                UnreadMessages.Add(chatMessage);
                Debug.Log(chatMessage.message);
            }
            catch
            {
                Debug.LogWarning($"Input NO message : {data}");
            }
        }
    }
}
