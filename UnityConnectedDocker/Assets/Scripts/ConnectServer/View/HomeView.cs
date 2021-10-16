using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ConnectServer
{
    public class HomeView : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] charaObjects;

        [SerializeField]
        private Canvas canvas;

        [SerializeField]
        private Text nameText;

        [SerializeField]
        private Button chatRoomButton;

        private GameManager gameManager;

        // Start is called before the first frame update
        void Start()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

            nameText.text = gameManager.User.name;
            var clone = GameObject.Instantiate(
                charaObjects[gameManager.User.chara_id],
                canvas.transform);
            clone.transform.localPosition = Vector3.zero;
            clone.transform.localScale = new Vector3(2, 2, 2);

            chatRoomButton.onClick.AddListener(onClickChatRoomListButton);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void onClickChatRoomListButton()
        {
            SceneManager.LoadScene("ChatRoomSelect");
        }
    }
}