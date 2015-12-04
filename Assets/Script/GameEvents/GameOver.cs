using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEngine.EventSystems;
#if UNITY_EDITOR
using UnityEditor;
#endif

[AddComponentMenu("Scripts/Game Events/Game Over")]
public class GameOver : MonoBehaviour
{

    /// <summary>
    /// Quando o personagem morre (vida = 0), a tela entra em grayscale e a animação começa.
    /// O jogador tem duas escolhas, Continuar, aonde eu recarrego a cena, e Sair.
    /// </summary>

    bool FadeOver = false;
    Character Personagem;
    Grayscale Fade;
    public GameObject Painel;
    public EventSystem evento;
    public GameObject FirstButton;
    // Use this for initialization
    void Start()
    {
        Fade = GetComponent<Grayscale>();
        Personagem = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        evento = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        if (FirstButton == null)
            FirstButton = Painel.transform.FindChild("Continua_bt").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (Personagem.VidaAtual <= 0)
        {
            if (evento != null)
            {
                if(FirstButton!= null)
                {
                    evento.SetSelectedGameObject(FirstButton);
                }
            }
            if (!FadeOver)
            {
                Personagem.EstadoDoJogador = GameStates.CharacterState.DontMove;
                Fade.effectAmount += 0.01f;
                if (Fade.effectAmount > 0.9f)
                {
                    FadeOver = true;
                }
            }
            else
            {
                if (Painel.activeInHierarchy == false)
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
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
