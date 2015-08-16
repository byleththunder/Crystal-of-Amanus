using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class InformationWindow : MonoBehaviour {
    Character Persoangem;
    
    //------
    public Text _Informacoes, _Nome, _Equip;
    //------
    int teste = 0;
    //Awake
    void Awake()
    {
        
        DontDestroyOnLoad(gameObject);
        GameObject _pers = GameObject.FindGameObjectWithTag("Player");
        if (_pers != null)
            Persoangem = _pers.GetComponent<Character>();
    }
    void OnEnable()
    {
        _Nome.text = Persoangem.Nome;
        _Informacoes.text = "Informação:\n\nVida Total: "+Persoangem.VidaTotal +"\n\nAmanus: "+Persoangem.AmanusTotal + "\n\nAtaque: "+Persoangem.Ataque;

        _Equip.text = "Equipamentos:\n\nArma: " + (Persoangem.Equipamentos[0] == null ? "Slot Vázio" : Persoangem.Equipamentos[0].Nome) + "\n\nArmadura: " + (Persoangem.Equipamentos[1] == null ? "Slot Vázio" : Persoangem.Equipamentos[1].Nome);
        
    }
}
