using UnityEngine;
using System.Collections;

public class Destination : MonoBehaviour {

    public string LevelName;
    CharacterPivot Pivot;
	// Use this for initialization
	void Start () {
        Pivot = GameObject.Find("Pivot").GetComponent<CharacterPivot>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseDown()
    {
        
        
        if (!Pivot.IsMoving() && Pivot.Destino == new Vector3(transform.position.x, 0, transform.position.z))
        {
            if (!string.IsNullOrEmpty(LevelName))
            {
                LoadingScreen.NextLevelName = LevelName;
                Application.LoadLevel("LoadingScene");
            }
            Debug.Log("Mudando de cena");
        }
        Pivot.Destino = new Vector3(transform.position.x, 0, transform.position.z);
    }
}
