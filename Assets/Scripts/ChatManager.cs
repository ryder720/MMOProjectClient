using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatManager : MonoBehaviour
{
    public static ChatManager instance;
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

    private List<Message> chatLog = new List<Message>();
    public GameObject chatPanel, serverTextObject, playerTextObject;
    public TMP_InputField input;

    public int maxMessages = 25;

    public void SendMessageToChat(int _id, string _text)
    {
        if (chatLog.Count >= maxMessages)
        {
            Destroy(chatLog[0].textObject.gameObject);
            chatLog.Remove(chatLog[0]);
        }

        Message _newMessage = new Message();

        if (_id == 0) // Message from server
        {
            
            _newMessage.text = _text;
            _newMessage.textObject = Instantiate(playerTextObject, chatPanel.transform);

            _newMessage.textObject.GetComponentInChildren<Text>().text = "Server: ";
            _newMessage.textObject.GetComponentInChildren<TMP_Text>().text = _text;

        }
        else // Message from player
        {
            _newMessage.playerID = _id;
            _newMessage.text = _text;
            _newMessage.textObject = Instantiate(playerTextObject, chatPanel.transform);

            _newMessage.textObject.GetComponentInChildren<Text>().text = GameManager.players[_id].username + ":";
            _newMessage.textObject.GetComponentInChildren<TMP_Text>().text = _text;
        }
        

        chatLog.Add(_newMessage);
    }
    public void SendTellToChat(int _id, string _text)
    {
        if (chatLog.Count >= maxMessages)
        {
            Destroy(chatLog[0].textObject.gameObject);
            chatLog.Remove(chatLog[0]);
        }

        Message _newMessage = new Message();

        if (_id == 0) // Message from server
        {
            _newMessage.text = "<color = purple>";
            _newMessage.text += _text;
            _newMessage.text += "<color>";
            _newMessage.textObject = Instantiate(playerTextObject, chatPanel.transform);

            _newMessage.textObject.GetComponentInChildren<Text>().text = "Server: ";
            _newMessage.textObject.GetComponentInChildren<TMP_Text>().text = _text;

        }
        else // Message from player
        {
            _newMessage.playerID = _id;
            _newMessage.text = "<color = purple>";
            _newMessage.text += _text;
            _newMessage.text += "<color>";
            _newMessage.textObject = Instantiate(playerTextObject, chatPanel.transform);

            _newMessage.textObject.GetComponentInChildren<Text>().text = GameManager.players[_id].username + ":";
            _newMessage.textObject.GetComponentInChildren<TMP_Text>().text = _text;
        }


        chatLog.Add(_newMessage);
    }

    public void SendMessageToServer()
    {
        if (!Input.GetKeyDown(KeyCode.Return))
            return;
        if (string.IsNullOrWhiteSpace(input.text))
            return;
        string _msg = input.text;

        ClientSend.ChatMessage(_msg);

        input.text = "";

    }

    public void ChatActive()
    {
        GameManager.playerState = PLAYER_STATE.CHAT_ACTIVE;
    }

    public void ChatInactive()
    {
        GameManager.playerState = PLAYER_STATE.NONE;
    }
}

public class Message 
{
    public int playerID;
    public string text;
    public GameObject textObject;
}
