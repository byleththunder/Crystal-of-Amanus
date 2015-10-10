using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu("Scripts/VisualScripts/Visual Contents/Load")]
public class VCLoad : Selectable
{
    //Texto para escrver as informações
    public Text Texto;
    //O ID é o indice dentro da lista de Games.
    [HideInInspector]
    public int id;

    protected override void Start()
    {
        base.Start();
        if (!Application.isPlaying) return;
        //Estou escrevendo qual é o Save do botão.
        Texto.text = "Save " + id + " - " + SaveLoad.savedGames[id].LastPlace;
        
    }

    //Quando clicar no botão, eu vou carregar o ultimo jogo e levar o jogador para o ultimo lugar que ele estava.
    void LoadCurent()
    {
        Game.current = SaveLoad.savedGames[id];
        LoadingScreen.NextLevelName = Game.current.LastPlace;
        Application.LoadLevel("LoadingScene");
    }
}
