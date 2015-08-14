using UnityEngine;
using System.Collections;
using System;

public class Item : MonoBehaviour
{
    //CallBack
    public Func<ITarget, bool> MetodoItem;
    //Enum
    public enum TipoDeItem { NaoConsumivel, Potion, Equipamento, Trigger};
    //Propriedades de Status
    public int Ataque { get; set; }
    public int Defesa { get; set; }
    public int Vida { get; set; }
    public int Amanus { get; set; }
    //Propriedade de itens
    public TipoDeItem Tipo;
    public string Nome;
    public string Descricao;
    public Sprite Img;
    public bool IsEquip;
    public Item ()
    {

    }
}
