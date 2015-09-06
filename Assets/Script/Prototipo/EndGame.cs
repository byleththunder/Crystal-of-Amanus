using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var monstros = GameObject.FindGameObjectsWithTag("Monsters");
        if(monstros.Length == 0)
        {
            //Application.LoadLevel(0);
        }
	}
}
