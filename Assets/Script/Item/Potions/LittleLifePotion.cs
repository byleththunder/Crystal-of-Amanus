using UnityEngine;
using System.Collections;

public class LittleLifePotion : Item {

    
	// Use this for initialization
    public LittleLifePotion ()
    {
        Vida = 30;
        Nome = "Poção de Vida (Menor)";
        Descricao = "Recupera 10% da sua vida";
        MetodoItem = BeberPocao;
        Tipo = TipoDeItem.Potion;
        
    }
	// Update is called once per frame
     bool BeberPocao(ITarget alvo)
    {
        
        if (alvo.Vida == alvo.VidaTotal)
        {
            return false;
        }
        alvo.StatsChange(-((Vida * alvo.VidaTotal) / 100), 0);
        return true;
    }
}
