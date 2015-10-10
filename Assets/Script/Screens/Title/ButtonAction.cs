using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Title Screnn/Commands Scripts")]
public class ButtonAction : MonoBehaviour
{

    [Header("Nome da primeira cena")]
    public string NomeDaFase;
    [Header("Painel aonde irei colocar os Loads")]
    public GameObject Painel;
    [Header("Prefab que vai carregar o load")]
    public VCLoad Prefab;
    [Header("Load Screen do Canvas")]
    public GameObject LoadGameScreen;
    //Esse bool serve para saber se eu já carreguei todos os loads
    bool IsDone = false;
    //Quando clicado em novo jogo, eu começo um novo jogo e levo a primeira fase.
    public void NovoJogo()
    {
        Game.current = new Game();
        LoadingScreen.NextLevelName = NomeDaFase;
        Application.LoadLevel("LoadingScene");
    }
    //Quando clidado em Load Game eu carrego os jogos anteriores e coloco eles na tela para serem selecionados.
    public void LoadGame()
    {
        SaveLoad.Load();
        if (SaveLoad.savedGames.Count == 0) return;
        LoadGameScreen.SetActive(true);
        if (!IsDone)
        {
            for (int i = 0; i < SaveLoad.savedGames.Count; i++)
            {
                VCLoad _temp = (VCLoad)Instantiate(Prefab);
                _temp.transform.SetParent(Painel.transform, false);
                _temp.id = i;
            }
            IsDone = true;
        }
    }
}
