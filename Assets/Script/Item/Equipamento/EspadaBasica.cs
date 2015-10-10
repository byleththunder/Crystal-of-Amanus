using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Item/Itens/Equipamentos/Espadas/Espada Basica")]
public class EspadaBasica : Item
{

    // Use this for initialization
    public EspadaBasica()
    {
        Nome = "Espada Basica";
        Descricao = "Espada simples";
        Ataque = 10;
        MetodoItem = Equipar;
    }
    bool Equipar(Target alvo)
    {
        Character player = alvo.obj.GetComponent<Character>();
        Item _temp = player.Equipar(this);
        if (_temp != null)
        {
            Inventario inv = alvo.obj.GetComponent<Inventario>();
            inv.PickItem(_temp, 1);
        }
        return true;
    }
}
