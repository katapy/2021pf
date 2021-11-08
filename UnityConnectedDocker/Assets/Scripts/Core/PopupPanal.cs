using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupPanal : MonoBehaviour
{
    /// <summary>
    /// OK button.
    /// </summary>
    [SerializeField]
    private Button okButton;

    /// <summary>
    /// Close button.
    /// </summary>
    [SerializeField]
    private Button[] closeButtons;

    private void Awake()
    {
        foreach (var btn in closeButtons)
        {
            btn.onClick.AddListener(Close);
        }
    }

    /// <summary>
    /// Close popup window.
    /// </summary>
    private void Close()
    {
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Add action "on click ok button".
    /// </summary>
    /// <param name="executeAction">
    /// Execute action on click ok buttion.
    /// </param>
    public void AddClickOkButtn(UnityAction executeAction)
    {
        Debug.Log("Add ok button");
        okButton.onClick.AddListener(executeAction);
    }
}