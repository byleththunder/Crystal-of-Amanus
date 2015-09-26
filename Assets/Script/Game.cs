using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Game {
    public static Game current;
    public string Player;
    public string Invent;
    public List<Quest> Quests;
    public string RuinaDungeon;
    public Game()
    {
        Player = Eran2.Player;
        Invent = Inventario.SaveInvent;
        Quests = DataBase_Quests.Lista;
        RuinaDungeon = RuinasLevelScript.Ruinas;
        current = this;
    }
    

}
