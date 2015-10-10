using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Find/Resouce")]
public class ResourceFind : MonoBehaviour {

    /// <summary>
    /// Eu coloquei em uma classe, métodos estáticos que acessam a pasta Resource para pegar um
    /// certo tipo prefab.
    /// </summary>
    

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
