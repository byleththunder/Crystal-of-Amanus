using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public enum TiposDeQuests { Principal, Repetitiva, Sub };
public enum ObjetivesType { Coleta = 0, Eliminacao = 1 };

public class Quest
{
    //Padrão
    public string Nome = string.Empty;
    public string Descricao = string.Empty;
    public ObjetivesType Objetivo;
    public bool IsComplete = false;
    public bool Repete = false;
    public Item[] ItensColetaveis;
    public Monster[] Monstros;
    public int[] Quantidades;
    public int RecompensaEmDinheiro = 0;
    //Principal
    public bool GetQuest = false;
    public TiposDeQuests Tipo;
    //Métodos
    public Quest()
    {
        
    }
    public void GerarQuestAleatória()
    {
        Objetivo = (ObjetivesType)(Random.Range(0,2));
        if(Objetivo == ObjetivesType.Coleta)
        {
            GerarColeta();
        }else 
        {
            GerarEliminacao();
        }
    }
    public void AtualizarDescricao()
    {
        Descricao = string.Empty;
        if (!IsComplete)
        {
            for (int i = 0; i < Quantidades.Length; i++)
            {
                if (Objetivo == ObjetivesType.Coleta)
                {
                    if (Quantidades[i] > 0)
                    {
                        Descricao += string.Format("\t- {0} x{1}", ItensColetaveis[i].Nome, Quantidades[i].ToString());
                    }else
                    {
                        Descricao += string.Format("\t- {0} OK", ItensColetaveis[i].Nome);
                    }
                }else
                {
                    if (Quantidades[i] > 0)
                    {
                        Descricao += string.Format("\t- {0} x{1}", Monstros[i].Nome, Quantidades[i].ToString());
                    }else
                    {
                        Descricao += string.Format("\t- {0} Ok", Monstros[i].Nome);
                    }
                }
            }
        }else
        {
            for (int i = 0; i < Quantidades.Length; i++)
            {
                if (Objetivo == ObjetivesType.Coleta)
                {
                    Descricao += string.Format("\t- {0} OK", ItensColetaveis[i].Nome);
                }
                else
                {
                    Descricao += string.Format("\t- {0} Ok", Monstros[i].Nome);
                }
            }
        }
    }
    void GerarColeta()
    {
        Nome = "Coleta";
        Descricao = "Colete:";
        Repete = true;
        Tipo = TiposDeQuests.Repetitiva;
        Objetivo = ObjetivesType.Coleta;
        IsComplete = false;
        //Peguei todos os itens já criados como prefabs
        Item[] ItensExistentes = Resources.LoadAll<Item>("ItemPrefabs");
        //Debug.Log("ItensExistentes na pasta: " + ItensExistentes.Length);
        if (ItensExistentes.Length > 0)
        {
            //Sortiei quantos itens terão que ser coletados
            ItensColetaveis = new Item[Random.Range(1, 3)];
            Quantidades = new int[ItensColetaveis.Length];
            #region selecionando itens
            for (int i = 0; i < ItensColetaveis.Length; i++)
            {
                int indice = Random.Range(0, ItensExistentes.Length);
                Item _item = ItensExistentes[indice];

                if (i == 0)
                {

                    ItensColetaveis[i] = _item;
                    ItensExistentes[indice] = null;
                    Quantidades[i] = Random.Range(1, 5);
                }
                else
                {

                    while (_item == null)
                    {
                        indice = Random.Range(0, ItensExistentes.Length);
                        _item = ItensExistentes[indice];
                    }
                    ItensColetaveis[i] = _item;
                    ItensExistentes[indice] = null;
                    Quantidades[i] = Random.Range(1, 5);
                }
                Descricao += string.Format("\n\t- {0} x{1}", ItensColetaveis[i].Nome, Quantidades[i].ToString());
            }
            #endregion
            RecompensaEmDinheiro = 100;
        }
    }
    void GerarEliminacao()
    {
        Nome = "Caçada";
        Descricao = "Mate:";
        Repete = true;
        Tipo = TiposDeQuests.Repetitiva;
        Objetivo = ObjetivesType.Eliminacao;
        IsComplete = false;
        //Peguei todos os itens já criados como prefabs
        Monster[] MonstrosExistentes = Resources.LoadAll<Monster>("Monsters");
       // Debug.Log("Monstros na pasta: " + MonstrosExistentes.Length);
        //Sortiei quantos itens terão que ser coletados
        if (MonstrosExistentes.Length > 0)
        {
            Monstros = new Monster[Random.Range(1, MonstrosExistentes.Length)];
            Quantidades = new int[Monstros.Length];
            #region selecionando Monstros
            for (int i = 0; i < Monstros.Length; i++)
            {
                int indice = Random.Range(0, MonstrosExistentes.Length);
                Monster _monster = MonstrosExistentes[indice];

                if (i == 0)
                {

                    Monstros[i] = _monster;
                    MonstrosExistentes[indice] = null;
                    Quantidades[i] = Random.Range(1, 5);
                }
                else
                {

                    while (_monster == null)
                    {
                        indice = Random.Range(0, MonstrosExistentes.Length);
                        _monster = MonstrosExistentes[indice];
                    }
                    Monstros[i] = _monster;
                    MonstrosExistentes[indice] = null;
                    Quantidades[i] = Random.Range(1, 5);
                }
                Descricao += string.Format("\n\t- {0} x{1}", Monstros[i].Nome, Quantidades[i].ToString());
            }
            #endregion
            RecompensaEmDinheiro = 100;
        }
    }
    
    public string Recompensa()
    {
        return RecompensaEmDinheiro.ToString() + (RecompensaEmDinheiro  <2?" Prata":" Pratas");
    }
    
    
   
}
