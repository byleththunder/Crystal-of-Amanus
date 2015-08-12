using UnityEngine;
using System.Collections;

public class WorldObserver : MonoBehaviour {
    public static Item ItemUtilizado;
    public static string MonstroMorto;
    public static bool ChangeItem = false;
    public static bool ChangeMonster = false;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (ChangeItem)
        {
            print(ItemUtilizado.Nome);
            ChangeItem = false;
        }
        if (ChangeMonster)
        {
            print(MonstroMorto);
            ChangeMonster = false;
        }
	}
}
