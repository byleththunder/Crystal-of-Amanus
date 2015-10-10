using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TesteSave : MonoBehaviour
{
    
    // Use this for initialization
    void Awake()
    {
        SaveLoad.Load();
        if (Game.current == null)
        {
            Game.current = new Game();
            print("oi");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveLoad.Save();
            print("Salvou");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveLoad.Load();
            print("Carregou");
        }
    }
}
