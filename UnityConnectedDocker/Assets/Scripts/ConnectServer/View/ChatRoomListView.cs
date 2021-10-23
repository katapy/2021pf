using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ConnectServer
{
    public class ChatRoomListView : MonoBehaviour
    {
        [SerializeField]
        private Button createButton;

        [SerializeField]
        private InputField roomNameInput;

        [SerializeField]
        private Button sampleButton;

        [SerializeField]
        private Button okButton;

        [SerializeField]
        private GameObject inputRoomNamePanel;

        [SerializeField]
        private GameObject waitingPanel;

        [SerializeField]
        private ChatManager chatManager;

        private GameManager gameManager;

        // Start is called before the first frame update
        void Start()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            okButton.onClick.AddListener(OnClick);

            StartCoroutine(GetRoomList());
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void OnClick()
        {
            StartCoroutine(CreateRoom());
        }

        IEnumerator CreateRoom()
        {
            #region Validation
            if(string.IsNullOrEmpty(roomNameInput.text))
            {
                yield break;
            }
            #endregion Validation


            var createRoomController = GetComponent<CreateRoomController>();
            createRoomController.RoomName = roomNameInput.text;
            yield return StartCoroutine(createRoomController.Connect(gameManager.User));
            inputRoomNamePanel.SetActive(false);
            yield return StartCoroutine(GetRoomList());
        }

        IEnumerator GetRoomList()
        {
            waitingPanel.SetActive(true);

            var getRoomListController = GetComponent<ChatRoomListController>();

            yield return StartCoroutine(getRoomListController.Connect(gameManager.User));

            foreach (var btn in sampleButton.transform.parent.GetComponentsInChildren<Button>())
            {
                GameObject.Destroy(btn.gameObject);
            }

            foreach(var room in getRoomListController.ChatRooms)
            {
                Debug.Log(room.room_name);
                var clone = GameObject.Instantiate(sampleButton, sampleButton.transform.parent);
                clone.GetComponentInChildren<Text>().text = room.room_name;
                clone.gameObject.SetActive(true);
                clone.GetComponent<ChatRoomTable>().ChatRoom = room;
                clone.onClick.AddListener(() => chatManager.JoinRoom(clone.GetComponent<ChatRoomTable>()));
            }

            waitingPanel.SetActive(false);
        }
    }
}