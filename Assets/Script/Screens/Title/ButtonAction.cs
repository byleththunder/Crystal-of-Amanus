using UnityEngine;
using System.Collections;

public class ButtonAction : MonoBehaviour {
    public string NomeDaFase;
    void Update()
    {
        if (Input.GetButtonDown("Action"))
        {
            NovoJogo();
        }
    }
	public void NovoJogo()
    {
        LoadingScreen.NextLevelName = NomeDaFase;
        Application.LoadLevel("LoadingScene");
    }
}
