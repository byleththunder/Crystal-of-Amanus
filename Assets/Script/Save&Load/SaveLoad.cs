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
        Game.current = new Game();
        //Load();
        if (!Directory.Exists(Pasta))
        {
            Directory.CreateDirectory(Pasta);
        }
        savedGames.Add(Game.current);
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
            Debug.Log(savedGames.Count);
            Debug.Log(savedGames[0].Invent);
            Game.current = savedGames[0];
        }
        else
        {
            Debug.LogError("Não existe Save");
        }
    }
}
