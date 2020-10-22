using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public int id;
    public string actorName;
    public TargetableTag target; 
    public TextMesh nameTag;

    private void Start()
    {
        nameTag = GetComponentInChildren<TextMesh>();
    }

}