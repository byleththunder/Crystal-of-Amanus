using UnityEngine;
using System.Collections;
using System;

public class Teleport : MonoBehaviour {
    public bool Trigger = false;
    public string SceneName = string.Empty;
   
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
        
	
	}
   
    void OnTriggerStay(Collider other)
    {
        if (this.enabled)
        {
            if (Trigger)
            {
                if (other.tag == "Player")
                {
                    try
                    {
                        Application.LoadLevel(SceneName);
                    }
                    catch (Exception e)
                    {
                        print(e.Message);
                    }
                }
            }
        }
    }
}
