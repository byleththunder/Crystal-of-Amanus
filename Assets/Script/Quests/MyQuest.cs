using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyQuest : MonoBehaviour
{

    List<Quest> _Quests = new List<Quest>();

    void Start()
    {
        _Quests = DataBase_Quests.OrganizarLista();
    }

    public int QuestsComprimento()
    {
        return _Quests.Count;
    }
    public Quest QuestInformation(int indice)
    {
        return _Quests[indice];
    }
    public void AdicionarNovaQuest(Quest nova)
    {
        nova.GetQuest = true;
        _Quests.Add(nova);
    }
    public void ChecarQuestMonstros(Monster mon)
    {
        for (int i = 0; i < _Quests.Count; i++)
        {
            if (_Quests[i].Objetivo == ObjetivesType.Eliminacao && !_Quests[i].IsComplete)
            {
                for (int j = 0; j < _Quests[i].Monstros.Length; j++)
                {
                   
                    if (_Quests[i].Monstros[j].Nome == mon.Nome)
                    {
                        if (_Quests[i].Quantidades[j] > 0)
                        {
                            _Quests[i].Quantidades[j]--;
                            
                        }
                        _Quests[i].AtualizarDescricao();
                        break;
                    }
                }
            }
        }
        ChecarQuestsCompletadas();
    }
    public void ChecarQuestItem(Item iten)
    {
        var quantidade = gameObject.GetComponent<Inventario>().QuantityInformations(iten);
        for (int i = 0; i < _Quests.Count; i++)
        {
            if (_Quests[i].Objetivo == ObjetivesType.Coleta && !_Quests[i].IsComplete)
            {
                for (int j = 0; j < _Quests[i].ItensColetaveis.Length; j++)
                {
                    if (_Quests[i].ItensColetaveis[j].Nome == iten.Nome)
                    {
                        if (_Quests[i].Quantidades[j] > 0)
                        {
                            _Quests[i].Quantidades[j] -= quantidade;
                            if (_Quests[i].Quantidades[j] < 0)
                                _Quests[i].Quantidades[j] = 0;
                            
                        }
                        _Quests[i].AtualizarDescricao();
                        break;
                    }
                }
            }
        }
        ChecarQuestsCompletadas();
    }
    public void ChecarQuestsCompletadas()
    {
       
        for (int i = 0; i < _Quests.Count; i++)
        {
            if (!_Quests[i].IsComplete)
            {
                for (int j = 0; j < _Quests[i].Quantidades.Length; j++)
                {
                    if (_Quests[i].Quantidades[j] > 0)
                    {
                        break;
                    }
                   if( i == _Quests.Count-1)
                   {
                       _Quests[i].IsComplete = true;
                   }
                }
                
            }
        }
    }
}
