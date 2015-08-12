using UnityEngine;
using System.Collections;

public class LittleAmanusPotion : Item {

    public LittleAmanusPotion()
    {
        Vida = 30;
        Nome = "Poção de Amanus (Menor)";
        Descricao = "Recupera 10% do seu Amanus";
        MetodoItem = BeberPocao;
    }
    bool BeberPocao(ITarget alvo)
    {

        if (alvo.Amanus == alvo.AmanusTotal)
        {
            return false;
        }
        alvo.StatsChange(0, -((Vida * alvo.AmanusTotal) / 100));
        return true;
    }
}
