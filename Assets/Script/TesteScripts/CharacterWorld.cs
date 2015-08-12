using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterWorld : MonoBehaviour {
    public List<Quest> MinhasMissoes;
   // List<Item> _Mochila;
    bool check = true;
    bool SwitchTela = false;
    public GameObject Menu;
    public MonoBehaviour[] ScriptsMovimento;
    public int Dinheiro = 0;
	// Use this for initialization
	void Start () {
        MinhasMissoes = new List<Quest>();
        //_Mochila = gameObject.GetComponent<Inventario>()._mochila;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            
            SwitchTela = !SwitchTela;
            WorldVars.Freeze = SwitchTela;
            Menu.SetActive(SwitchTela);
            //Menu.GetComponent<Menu>().AttInformacoes();
            
        }
        if (WorldVars.Freeze)
        {
            for (int i = 0; i < ScriptsMovimento.Length; i++)
            {
                ScriptsMovimento[i].enabled = false;
            }
        }
        else
        {
            for (int i = 0; i < ScriptsMovimento.Length; i++)
            {
                ScriptsMovimento[i].enabled = true;
            }
        }
        
	}
    //public void ChecarQuestColeta()
    //{
        
    //    for (int i = 0; i < MinhasMissoes.Count; i++)
    //    {
    //        if (MinhasMissoes[i].Condicao == Condicoes.Coleta && !MinhasMissoes[i].Complete)
    //        {
    //            MinhasMissoes[i].ChecarQuestColeta(_Mochila);
    //        }
    //    }
        
    //}
    public void ChecarQuestConversa(string nomeDoNpc)
    {

        for (int i = 0; i < MinhasMissoes.Count; i++)
        {
            if (MinhasMissoes[i].Condicao == Condicoes.Conversar && !MinhasMissoes[i].Complete)
            {
                MinhasMissoes[i].ChecarQuestConversa(nomeDoNpc);
            }
        }

    }
}
