﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[AddComponentMenu("Scripts/VisualScripts/Informações")]
public class VIformacao : MonoBehaviour
{

    public Text Info, Arma_TXT, Armadura_TXT;
    Character Personagem;
    // Use this for initialization
    void Start()
    {
        Personagem = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Info.text = Personagem.Nome + "\n" + "Lv "+Personagem.Level+"\nExp para o proximo level "+Personagem.NextLevel+"\n\n\n" + "Vida: " + Personagem.VidaTotal + "\nAmanus: " + Personagem.AmanusTotal + "\nAtaque: " + Personagem.AtaqueAtual;
        if (Personagem.Equipamentos[0] != null)
            Arma_TXT.text = Personagem.Equipamentos[0].Nome;
        else
            Arma_TXT.text = "---";
        if (Personagem.Equipamentos[1] != null)
            Armadura_TXT.text = Personagem.Equipamentos[1].Nome;
        else
            Armadura_TXT.text = "---";
    }
}
