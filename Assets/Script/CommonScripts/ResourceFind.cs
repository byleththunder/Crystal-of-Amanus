using UnityEngine;
using System.Collections;

public class ResourceFind : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public static Item FindItem(string name)
    {
        Item[] ItensExistentes = Resources.LoadAll<Item>("ItemPrefabs");
        Item _temp = (Item)(new object());
        foreach(Item iten in ItensExistentes)
        {
            if(iten.Nome == name)
            {
                _temp = iten;
                break;
            }
        }
        return _temp;
    }
    public static Monster FindMonster(string name)
    {
        Monster[] MonstrosExistentes = Resources.LoadAll<Monster>("Monsters");
        Monster _temp = new Monster();
        foreach (Monster monstro in MonstrosExistentes)
        {
            if (monstro.name == name)
            {
                _temp = monstro;
                break;
            }
        }
        return _temp;
    }
}
