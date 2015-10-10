using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Scripts/Game/Game Script (Ins´t a MonoBehavior)")]
[System.Serializable]
public class Game {

    /// <summary "Como Funciona ?">
    /// Essa classe salva os estados do jogo que eu solicitar.
    /// </summary>

    public static Game current;
    public string Player;
    public string Invent;
    public List<Quest> Quests;
    public string RuinaDungeon;
    public string LastPlace;
    public Game()
    {
        Player = Eran2.Player;
        Invent = Inventario.SaveInvent;
        Quests = DataBase_Quests.Lista;
        RuinaDungeon = RuinasLevelScript.Ruinas;
        LastPlace = LoadingScreen.NextLevelName;
        current = this;
    }
    
    
    

}
