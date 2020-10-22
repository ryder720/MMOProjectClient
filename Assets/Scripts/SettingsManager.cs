using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public SettingsManager instance;
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


    public bool nameTags = true;  // I'm lazy I'll code this later
}
