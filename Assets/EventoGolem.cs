using UnityEngine;
using System.Collections;

public class EventoGolem : MonoBehaviour {

    public GameObject Golem;
    public GameObject Rocha;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Golem != null)
        {
            if (Rocha == null && !Golem.activeInHierarchy)
            {
                Golem.SetActive(true);
                Camera.main.GetComponent<AudioSource>().mute = true;

            }
        }else
        {
            LoadingScreen.NextLevelName = "Falansterio";
            Application.LoadLevel("LoadingScene");
        }
	}
}
