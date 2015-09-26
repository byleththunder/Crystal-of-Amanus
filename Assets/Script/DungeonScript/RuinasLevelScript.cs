using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class RuinasLevelScript : DungeonScript {

    public static string Ruinas = string.Empty;
	// Use this for initialization
	void Start () {
        GameObject[] _obj =  GameObject.FindGameObjectsWithTag("Chest");
        for (int i = 0; i < _obj.Length;i++)
        {
            Baus.Add(_obj[i].GetComponent<Chest>());
        }

            try
            {
                if (!string.IsNullOrEmpty(Game.current.RuinaDungeon))
                {
                    Ruinas = Game.current.RuinaDungeon;
                    Load();
                }
            }
            catch
            {
                Debug.LogError("invent é null");
            }
        //ChangeColorByStage();
        
	}
	
	// Update is called once per frame
	void Update () {
        Save();
	}
    public override void Save()
    {
        string _temp = string.Empty;
        List<bool> ChestOpen = new List<bool>();
        for(int i = 0; i <Baus.Count; i ++)
        {
            ChestOpen.Add(Baus[i].Open);
        }
        _temp += SaveGameState.SalvarLista<bool>(ChestOpen, "ChestOpen");
        Ruinas = _temp;
    }
    public override void Load()
    {
        List<bool> ChestOpen = LoadGameState.LoadBooleanList(Ruinas, "ChestOpen");
        for (int i = 0; i < Baus.Count; i++)
        {
            Baus[i].Open = ChestOpen[i];
        }
    }
}
