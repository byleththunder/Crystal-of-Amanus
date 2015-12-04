using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[AddComponentMenu("Scripts/Save e Load/Save e Load Game")]
public static class SaveLoad
{

    public static List<Game> savedGames = new List<Game>();
    public static string Pasta = Application.dataPath + "/Saves/";
    public static string Arquivo = "Crystal of Amanus.sav";
    public static void Save()
    {
        //Load();
        if (savedGames.Count == 0)
        {
            savedGames.Add(Game.current);
            Game.current = new Game();
        }
        else
        {
            savedGames[0] = Game.current;
            //Debug.Log(Game.current.MinhasQuests.Count);
        }
        
        if (!Directory.Exists(Pasta))
        {
            Directory.CreateDirectory(Pasta);
        }
        
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Pasta+Arquivo);
        bf.Serialize(file, SaveLoad.savedGames);
        file.Close();
    }

    public static void Load()
    {
        if (!Directory.Exists(Pasta))
        {
            Directory.CreateDirectory(Pasta);
        }
        if (File.Exists(Pasta+Arquivo))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Pasta+Arquivo, FileMode.Open);
            SaveLoad.savedGames = (List<Game>)bf.Deserialize(file);
            file.Close();
            
            Game.current = savedGames[0];
        }
        else
        {
            //Debug.LogError("Não existe Save");
        }
    }
}
