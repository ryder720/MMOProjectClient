using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GAME_STATE { IN_GAME, MAINMENU}
public enum PLAYER_STATE { NONE, CHAT_ACTIVE }

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
    public static Dictionary<int, ActorManager> actors = new Dictionary<int, ActorManager>();

    public static PLAYER_STATE playerState = PLAYER_STATE.NONE;
    public static GAME_STATE gameState = GAME_STATE.MAINMENU;

    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;
    public GameObject actorPrefab;

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

    private void Update()
    {
        if(gameState == GAME_STATE.MAINMENU)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
        else if(gameState == GAME_STATE.IN_GAME)
        {
            // bring up menu here
        }
    }

    public void SpawnPlayer(int _id, string _username, Vector3 _position, Quaternion _rotation)
    {
        GameObject _player;
        if(_id == Client.instance.myID)
        {
            _player = Instantiate(localPlayerPrefab, _position, _rotation);
        }
        else
        {
            _player = Instantiate(playerPrefab, _position, _rotation);
        }
        _player.GetComponent<PlayerManager>().id = _id;
        _player.GetComponent<PlayerManager>().username = _username;
        players.Add(_id, _player.GetComponent<PlayerManager>());
        UIManager.instance.UpdateNameplates();
    }

    public void SpawnActor(int _id, string _name, Vector3 _position, Quaternion _rotation)
    {
        GameObject _actor;
        _actor = Instantiate(actorPrefab, _position, _rotation);
        _actor.GetComponent<ActorManager>().id = _id;
        _actor.GetComponent<ActorManager>().actorName = _name;

        actors.Add(_id, _actor.GetComponent<ActorManager>());
        UIManager.instance.UpdateNameplates();
    }
}
