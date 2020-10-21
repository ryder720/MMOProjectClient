using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLAYER_STATE { NONE, CHAT_ACTIVE}
public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public TargetableTag target; // Change this to a targetable script or something
    
}
