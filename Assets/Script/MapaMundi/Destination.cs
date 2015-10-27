using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/MapaMundi/Destination")]
public class Destination : MonoBehaviour {

    public string LevelName;
    CharacterPivot Pivot;
    NavMeshAgent Nav;
	// Use this for initialization
	void Start () {
        Pivot = GameObject.Find("Pivot").GetComponent<CharacterPivot>();
        Nav = GameObject.Find("nAV").GetComponent<NavMeshAgent>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseDown()
    {
        if (Nav.destination == transform.position)
        {
            if (!string.IsNullOrEmpty(LevelName))
            {
                LoadingScreen.NextLevelName = LevelName;
                Application.LoadLevel("LoadingScene");
            }
            Debug.Log("Mudando de cena");
        }
        Nav.SetDestination(transform.position);
    }
}
