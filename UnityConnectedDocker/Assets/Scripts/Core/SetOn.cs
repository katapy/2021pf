using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetOn : MonoBehaviour
{
    [SerializeField]
    private GameObject gameObject;

    [SerializeField]
    private Button button;

    [SerializeField]
    private bool isOn;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnClick()
    {
        gameObject.SetActive(isOn);
    }
}
