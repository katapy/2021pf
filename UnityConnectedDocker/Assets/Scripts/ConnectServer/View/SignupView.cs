using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Spine.Unity;

namespace ConnectServer
{
    public class SignupView : MonoBehaviour
    {
        [SerializeField]
        private Text errMsgText;

        [SerializeField]
        private Button signUpButton;

        [SerializeField]
        private InputField usernameText;

        [SerializeField]
        private InputField passwordText;

        [SerializeField]
        private InputField emailText;

        [SerializeField]
        private int index = -1;

        [SerializeField]
        private Button[] buttons = new Button[3];

        [SerializeField]
        private GameObject waitingPanel;

        // Start is called before the first frame update
        void Start()
        {
            signUpButton.onClick.AddListener(onClickButton);

            foreach (var btn in buttons)
            {
                btn.image.color = Color.blue;
                btn.onClick.AddListener(() => onClickButton(btn));
                btn.transform.parent.GetComponentInChildren<SkeletonAnimation>().timeScale = 0;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        void onClickButton()
        {
            #region Validation
            if (string.IsNullOrWhiteSpace(emailText.text))
            {
                errMsgText.text = "Input Email.";
                return;
            }
            if (string.IsNullOrWhiteSpace(passwordText.text))
            {
                errMsgText.text = "Input Password.";
                return;
            }
            if (string.IsNullOrWhiteSpace(usernameText.text))
            {
                errMsgText.text = "Input Username.";
                return;
            }
            if (index < 0)
            {
                errMsgText.text = "Select Character.";
                return;
            }
            #endregion Validation

            StartCoroutine(signup());
        }

        private void onClickButton(Button btn)
        {
            foreach (var b in buttons)
            {
                b.image.color = Color.blue;
                b.transform.parent.GetComponentInChildren<SkeletonAnimation>().timeScale = 0;
            }
            btn.image.color = Color.yellow;
            btn.transform.parent.GetComponentInChildren<SkeletonAnimation>().timeScale = 1;
            index = btn.transform.parent.GetSiblingIndex();
        }

        private IEnumerator signup()
        {
            waitingPanel.SetActive(true);

            var signupController = GetComponent<SignupController>();
            User user = new User();
            user.name = usernameText.text;
            user.password = Hash.HashString(passwordText.text);
            user.email = emailText.text;
            user.chara_id = index;
            yield return StartCoroutine(signupController.Connect(user));

            waitingPanel.SetActive(false);
            errMsgText.text = signupController.ErrMsg;

            if(signupController.isSuccess)
            {
                SceneManager.LoadScene("Login");
            }
        }
    }
}