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
    #region Eran
    /* Variaveis para salvar
     * Posição
     * Vida
     * Amanus
     * Ataque
     * Experiencia
     * Equipamentos[2]
     */
    public  Vector3 Player_Pos;
    public  int Player_Level;
    public  int Player_Argento;
    public  int Player_Vida;
    public  int Player_Amanus;
    public  int Player_Ataque;
    public  int Player_Exp;
    public  string[] Player_Equipamentos = new string[2];
    #endregion
    #region Dungeon - Ruinas
    /* Variaveis para salvar
     * Baus
     * Eventos
     */
    public  bool[] Ruinas_Baus;
    public  bool Ruinas_ArrestingCinematic;
    #endregion

    public Game()
    {
        #region Eran_Initialize
        
        #endregion
        Invent = Inventario.SaveInvent;
        Quests = DataBase_Quests.Lista;
        RuinaDungeon = RuinasLevelScript.Ruinas;
        LastPlace = LoadingScreen.NextLevelName;
        current = this;
    }
    
    
    

}
