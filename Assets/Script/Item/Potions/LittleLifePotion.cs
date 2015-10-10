using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Item/Itens/Pocao/Pocao de Vida")]
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
     bool BeberPocao(Target alvo)
    {
        
        if (alvo.VidaAtual == alvo.VidaTotal)
        {
            return false;
        }
        alvo.HealOrDamage(-((Vida * alvo.VidaTotal) / 100), 0);
        return true;
    }
}
