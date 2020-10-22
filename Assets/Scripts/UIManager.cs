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

    public void UpdateNameplates() // I suck at ui and know this sucks
    {
        for (int i = 1; i <= GameManager.players.Count; i++)
        {
            GameManager.players[i].nameTag.text = GameManager.players[i].username;
            
        }

        for (int i = 1; i <= GameManager.actors.Count; i++)
        {
            GameManager.actors[i].nameTag.text = GameManager.actors[i].actorName;
            
        }
    }
}
