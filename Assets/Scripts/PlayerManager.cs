using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public TargetableTag target; // Change this to a targetable script or something
    public TextMesh nameTag;

    private void Start()
    {
        nameTag = GetComponentInChildren<TextMesh>();
    }

}
