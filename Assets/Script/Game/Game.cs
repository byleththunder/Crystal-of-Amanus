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
    //Inventario
    public List<string> ItensArmazenado = new List<string>();
    public List<int> QuantidadeDeItens = new List<int>();
    //
    public List<Quest> Quests;
    public List<Quest> MinhasQuests;
    public List<Quest> Nevena = new List<Quest>();
    public string[] Skills = new string[4];
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
    public  float Player_Posx;
    public float Player_Posy;
    public float Player_Posz;
    public  int Player_Level;
    public  int Player_Argento;
    public  int Player_Vida;
    public  int Player_Amanus;
    public  int Player_Ataque;
    public  int Player_Exp;
    public  string[] Player_Equipamentos = new string[2];
    public long Player_Divida;
    public bool begin = false;
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
       
        Quests = DataBase_Quests.Lista;
        RuinaDungeon = RuinasLevelScript.Ruinas;
        LastPlace = LoadingScreen.NextLevelName;
        begin = true;
        current = this;
    }
    
    
    

}
