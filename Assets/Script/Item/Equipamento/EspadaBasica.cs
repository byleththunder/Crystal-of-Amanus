using UnityEngine;
using System.Collections;

public class EspadaBasica : Item {

	// Use this for initialization
    public EspadaBasica()
    {
        Nome = "Espada Basica";
        Descricao = "Espada simples";
        Ataque = 10;
        MetodoItem = Equipar;
    }
	bool Equipar(ITarget alvo)
    {
        PlayerMovement player = alvo.obj.GetComponent<PlayerMovement>();
        Item _temp = player.Equipar(this,0);
        if(_temp != null)
        {
            Inventario inv = alvo.obj.GetComponent<Inventario>();
            inv.PickItem(_temp, 1);
        }
        return true;
    }
}
