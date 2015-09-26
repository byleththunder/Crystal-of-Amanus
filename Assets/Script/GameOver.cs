using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class GameOver : MonoBehaviour {
    bool FadeOver = false;
    Character Personagem;
    Grayscale Fade;
    public GameObject Painel;
	// Use this for initialization
	void Start () {
        Fade = GetComponent<Grayscale>();
        Personagem = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Personagem.Vida <=0)
        {
            
            if(!FadeOver)
            {
                Personagem.EstadoDoJogador = GameStates.CharacterState.DontMove;
                Fade.effectAmount += 0.01f;
                if(Fade.effectAmount>0.9f)
                {
                    FadeOver = true;
                }
            }
            else
            {
                if(Painel.activeInHierarchy == false)
                {
                    Personagem.DeathState();
                    Painel.SetActive(true);
                }
            }
        }
	
	}
    public void Retry()
    {
        Personagem.ReviveState();
        Personagem.HealOrDamage(-Personagem.VidaTotal, 0);
        LoadingScreen.NextLevelName = Application.loadedLevelName;
        Application.LoadLevel("LoadingScene");
    }
    public void Sair()
    {
        Application.Quit();
    }
}
