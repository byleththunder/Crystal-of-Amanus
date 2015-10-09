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
    bool BeberPocao(Target alvo)
    {

        if (alvo.AmanusAtual == alvo.AmanusTotal)
        {
            return false;
        }
        alvo.HealOrDamage(0, -((Vida * alvo.AmanusTotal) / 100));
        return true;
    }
}
