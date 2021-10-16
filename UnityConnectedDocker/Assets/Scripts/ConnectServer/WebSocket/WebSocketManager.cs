/// <summary>
/// WebSocketManager
/// </summary>
/// ref
/// https://qiita.com/nisei275/items/a60cb3d3e97aeb429fa7

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

namespace CoonctServer
{
    public class WebSocketManager : MonoBehaviour
    {
        WebSocket ws;

        void Start()
        {
            // 接続先のURLとポート番号を指定する
            ws = new WebSocket("ws://localhost:8000/chat/7777");

            // WebSocketの接続を開始した時に実行されるイベント
            ws.OnOpen += (sender, e) => Debug.Log("WebSocket Open");
            // メッセージを受信した時に実行されるイベント
            ws.OnMessage += (sender, e) => Debug.Log("WebSocket Message Data: " + e.Data);
            // 接続に失敗した時に実行されるイベント
            ws.OnError += (sender, e) => Debug.Log("WebSocket Error Message: " + e.Message);
            // 接続を終了した時に実行されるイベント
            ws.OnClose += (sender, e) => Debug.Log("WebSocket Close");

            // WebSocketの接続を開始
            ws.Connect();
        }

        void Update()
        {
            // sキーを押下したときにメッセージを送信する
            if (Input.GetKeyUp("s")) {
                ws.Send("Test Message");
            }
        }

        void OnDestroy()
        {
            // ゲームオブジェクトが削除されたときにWebSocketの接続を閉じる
            ws.Close();
            ws = null;
        }
    }
}
