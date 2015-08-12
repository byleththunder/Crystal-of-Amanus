using UnityEngine;
using System.Collections;

public class GameTesteOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            if(GameObject.FindGameObjectsWithTag("Respawn").Length == 0)
            {
                print("oi");
                LoadingScreen.NextLevelName = "dungeon";
                Application.LoadLevel("LoadingScene");
            }
        }
    }
}
