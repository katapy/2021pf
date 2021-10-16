using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(onClickButton);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void onClickButton()
    {
        SceneManager.LoadScene(button.name.Substring(0, button.name.Length - 6));
    }
}