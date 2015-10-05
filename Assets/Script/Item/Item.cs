using UnityEngine;
using System.Collections;
using System;


public class Item : MonoBehaviour
{
    //CallBack
    public Func<Target, bool> MetodoItem;
    //Enum
    public enum TipoDeItem { NaoConsumivel, Potion, Equipamento, Trigger};
    public enum EquipmentTypes { Arma, Armadura}
    //Propriedades de Status
    public int Ataque { get; set; }
    public int Defesa { get; set; }
    public int Vida { get; set; }
    public int Amanus { get; set; }
    //Propriedade de itens
    public TipoDeItem Tipo;
    public EquipmentTypes TipoDeEquipamentos;
    public string Nome;
    public string Descricao;
    public Sprite Img;
    public Sprite NameImg;
    public bool IsEquip;
    public int Preco;
    public Item ()
    {

    }
}
