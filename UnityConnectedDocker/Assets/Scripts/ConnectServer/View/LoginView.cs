using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ConnectServer
{
    public class LoginView : MonoBehaviour
    {
        [SerializeField]
        private Text errMsgText;

        [SerializeField]
        private Button signUpButton;

        [SerializeField]
        private Button loginButton;

        [SerializeField]
        private InputField passwordText;

        [SerializeField]
        private InputField emailText;

        [SerializeField]
        private GameObject waitingPanel;

        // Start is called before the first frame update
        void Start()
        {
            signUpButton.onClick.AddListener(onClickSignupButton);
            loginButton.onClick.AddListener(onClickLoginButton);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void onClickSignupButton()
        {
            SceneManager.LoadScene("Signup");
        }

        void onClickLoginButton()
        {
            StartCoroutine(login());
        }

        private IEnumerator login()
        {
            waitingPanel.SetActive(true);

            var loginController = GetComponent<LoginController>();
            User user = new User();
            user.password = Hash.HashString(passwordText.text);
            user.email = emailText.text;
            yield return StartCoroutine(loginController.Connect(user));

            waitingPanel.SetActive(false);
            errMsgText.text = loginController.ErrMsg;

            if (loginController.isLogin)
            {
                SceneManager.LoadScene("Home");
            }
        }
    }
}