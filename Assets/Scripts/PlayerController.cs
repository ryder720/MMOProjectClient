using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private PlayerManager playerManager;
    private Camera cam;
    private Collider tabTargetRange;

    

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        cam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if (GameManager.playerState == PLAYER_STATE.NONE)
        {
            if (Input.GetMouseButtonDown(0)) // change to pressed instead of held some how
            {
                playerManager.target = GetTarget();
                //if (Input.GetMouseButtonDown(1))
                //{
                // Code for calling auto attack
                //}
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                playerManager.target = TabTarget(playerManager.target);
            }
        }
    }

    private void FixedUpdate()
    {
        SendInputToServer();
    }

    private void SendInputToServer()
    {
        if (GameManager.playerState == PLAYER_STATE.NONE)
        {
            bool[] _inputs = new bool[] // Grabbing inputs in order for server
            {
            Input.GetKey(KeyCode.W),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.D),
            Input.GetKey(KeyCode.Space)
            };

            ClientSend.PlayerMovement(_inputs);
        }
    }

    private TargetableTag GetTarget() // finding a target that has a player manager Change to targetable tag later on or change playermanager to actormanager etc
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.GetComponentInParent<TargetableTag>())
            {
                if(hit.collider.GetComponentInParent<PlayerManager>())
                    Debug.Log(this.GetComponentInParent<PlayerManager>().username + " is targetting " + hit.collider.GetComponentInParent<PlayerManager>().username);
                else
                    Debug.Log(this.GetComponentInParent<PlayerManager>().username + " is targetting " + hit.collider.GetComponentInParent<ActorManager>().actorName);
                return hit.collider.GetComponentInParent<TargetableTag>();
            }
            Debug.Log(hit.collider.gameObject.name);
        }

        return null;
    }

    private TargetableTag TabTarget(TargetableTag _currTarget)
    {
        Collider[] _hitColliders = Physics.OverlapBox(transform.position, transform.localScale * 100, Quaternion.identity);
        List<Collider> _targetableColliders = new List<Collider>();
        foreach (Collider i in _hitColliders)
        {
            if(i != GetComponentInChildren<Collider>() && i.GetComponentInParent<TargetableTag>()) // Could use a layer mask to be more efficient probably
            {
                _targetableColliders.Add(i);
            }
        }

        if(_targetableColliders.Count > 0)
        {
            if(_currTarget != null && _targetableColliders.Contains(_currTarget.GetComponentInChildren<Collider>()))
            {
                var curIndex = _targetableColliders.IndexOf(_currTarget.GetComponentInChildren<Collider>());
                curIndex++;
                if(curIndex > (_targetableColliders.Count - 1))
                {
                    return _targetableColliders[0].GetComponentInParent<TargetableTag>();
                }
                return _targetableColliders[curIndex].GetComponentInParent<TargetableTag>();
            }
            return _targetableColliders[0].GetComponentInParent<TargetableTag>();
        }

        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale*200);
    }
}
