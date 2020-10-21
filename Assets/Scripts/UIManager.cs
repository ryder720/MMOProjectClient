using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject startMenu;
    public GameObject playerUI;
    public TMP_InputField usernameField;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying");
            Destroy(this);
        }
    }

    public void ConnectToServer()
    {
        if (!Input.GetKeyDown(KeyCode.Return))
            return;
        if (string.IsNullOrWhiteSpace(usernameField.text) || usernameField.text == "Username...")
            return;
        startMenu.SetActive(false);
        usernameField.interactable = false;
        Client.instance.ConnectToServer();
        playerUI.SetActive(true);
    }
}
