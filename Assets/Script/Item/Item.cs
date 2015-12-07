using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
[AddComponentMenu("Scripts/Item/Item")]
public class Item : ScriptableObject
{

    ///<summary>
    ///Classe base de itens
    ///</summary>

    //CallBack
    public Func<Target, bool> MetodoItem;
    //Enum
    public enum TipoDeItem { NaoConsumivel, Potion, Equipamento, Trigger};
    public enum EquipmentTypes { Arma, Armadura}
    //Propriedades de Status
    public int Ataque;
    public int Defesa;
    public int Vida;
    public int Amanus;
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
        SetItem();
    }

    public void SetItem()
    {
        if(this.Tipo == TipoDeItem.Equipamento)
        {
            MetodoItem = Equipar;
        }
        else if(this.Tipo == TipoDeItem.Potion)
        {
            MetodoItem = Recuperar;
        }
        else
        {
            MetodoItem = Nada;
        }
    }
     bool Equipar(Target alvo)
    {
        Character player = alvo.GetComponent<Character>();
        Item _temp = player.Equipar(this);
        if (_temp != null)
        {
            Inventario inv = alvo.GetComponent<Inventario>();
            inv.PickItem(_temp, 1);
        }
        return true;
    }
     bool Recuperar(Target alvo)
    {
        if (alvo.VidaAtual == alvo.VidaTotal)
        {
            return false;
        }
        alvo.HealOrDamage(-((Vida * alvo.VidaTotal) / 100), 0);
        return true;
    }
     bool Nada(Target alvo)
    {
        return false;
    }
}
