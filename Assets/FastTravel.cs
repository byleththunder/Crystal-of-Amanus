using UnityEngine;
using System.Collections;

public class FastTravel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Travel(string name)
    {
        LoadingScreen.NextLevelName = name;
        Application.LoadLevel("LoadingScene");
    }
}
