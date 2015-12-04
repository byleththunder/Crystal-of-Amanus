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
        Calendar.IncreaseDay(1);
        LoadingScreen.NextLevelName = name;
        Application.LoadLevel("LoadingScene");
        GameStates.IsAWindowOpen = false;

    }
    public void Sair()
    {
        GameStates.IsAWindowOpen = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().EstadoDoJogador = GameStates.CharacterState.Playing;
    }
}
