using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public enum TiposDeQuests { Principal, Repetitiva, Sub };
public enum Condicoes { Conversar, Coleta, Matar };
public class Quest
{
    //Padrão
    public string Nome = string.Empty;
    public string Texto = string.Empty;
    public string c = string.Empty;
    public TiposDeQuests Tipo;
    public int RecompensaEmDinheiro = 0;
    public Condicoes Condicao;
    public bool Complete = false;
    public bool GetQuest = false;
    //Conversa
    public string NomeDoNPC = string.Empty;
    //Matar
    public string NomeDoMonstro = string.Empty;
    //Coleta e Matar
    public int Quantidade = 0;
    Item item;
    public Quest(TiposDeQuests t, string nome, string Descricao)
    {
        Tipo = t;
        Nome = nome;
        Texto = Descricao;
    }

    public string Recompensa()
    {
        return "Dinheiro:" + RecompensaEmDinheiro.ToString();
    }
    public void SetupColeta(Item item, int quantidade)
    {
        Condicao = Condicoes.Coleta;
        Quantidade = quantidade;
        this.item = item;
        c = "Colete " + Quantidade + " "+ item.Nome;

    }
    public void SetupEliminacao(string monstro, int quantidade)
    {
        Condicao = Condicoes.Matar;
        NomeDoMonstro = monstro;
        Quantidade = quantidade;
        c = "Mate " + Quantidade + " " + NomeDoMonstro;

    }
    public void SetupConversa(string nome)
    {
        Condicao = Condicoes.Conversar;
        NomeDoNPC = nome;
        c = "Converse com " + NomeDoNPC + ".";

    }
    //public void ChecarQuestColeta(List<Item> Mochila)
    //{
    //    if(!Complete){
    //        for (int i = 0; i < Mochila.Count; i++)
    //        {
    //            if (Mochila[i] == item)
    //            {
    //                if (Mochila[i].Quantidade >= Quantidade)
    //                {
    //                   // Debug.Log("Item: " + Mochila[i].Nome + "- Quantidade na Mochila: " + Mochila[i].Quantidade + " - Quantidade da Quest: " + Quantidade);
    //                    Mochila[i].Quantidade -= Quantidade;
    //                   // Debug.Log("Item: " + Mochila[i].Nome + "- Quantidade na Mochila: " + Mochila[i].Quantidade + " - Quantidade da Quest: " + Quantidade);
    //                    if (Mochila[i].Quantidade <= 0)
    //                    {
    //                        Mochila.RemoveAt(i);
    //                    }
                        
    //                    Complete = true;
    //                }
    //            }
    //        }
    //    }
    //}
    public void ChecarQuestConversa(string NomedoNpc)
    {
        if (!Complete)
        {
            if (this.NomeDoNPC == NomedoNpc)
            {
                Complete = true;
            }
        }
    }
    public void SaveQuest()
    {
        PlayerPrefs.SetString(Nome+"_QuestNome", Nome);
        PlayerPrefs.SetString(Nome + "_QuestCondicoes",Condicao.ToString());
        PlayerPrefs.SetString(Nome + "_QuestDescricao", Texto);
        PlayerPrefs.SetString(Nome + "_QuestTipo", Tipo.ToString());
        PlayerPrefs.SetInt(Nome + "_QuestRecompensa", RecompensaEmDinheiro);
        PlayerPrefs.SetString(Nome + "_QuestComplete", Complete.ToString());
        switch (Condicao)
        {
            case Condicoes.Coleta:
                PlayerPrefs.SetString(Nome + "_QuestItem", this.item.Nome);
                PlayerPrefs.SetInt(Nome + "_QuestQuantidade", Quantidade);
                break;
            case Condicoes.Conversar:
                PlayerPrefs.SetString(Nome + "_QuestNPC", NomeDoNPC);
                break;
            case Condicoes.Matar:
                PlayerPrefs.SetString(Nome + "_QuestMonstro", NomeDoMonstro);
                PlayerPrefs.SetInt(Nome + "_QuestQuantidade", Quantidade);
                break;
        }
    }
    public void LoadQuest()
    {
        Nome = PlayerPrefs.GetString(Nome + "_QuestNome");
        Condicao = (Condicoes)((object)PlayerPrefs.GetString(Nome + "_QuestCondicoes"));
        Texto = PlayerPrefs.GetString(Nome + "_QuestDescricao");
        Tipo = (TiposDeQuests)((object)PlayerPrefs.GetString(Nome + "_QuestTipo"));
        RecompensaEmDinheiro = PlayerPrefs.GetInt(Nome + "_QuestRecompensa");
        PlayerPrefs.GetString(Nome + "_QuestComplete");
        switch (Condicao)
        {
            case Condicoes.Coleta:
                item = (Item)((object)PlayerPrefs.GetString(Nome + "_QuestItem"));
                Quantidade = PlayerPrefs.GetInt(Nome + "_QuestQuantidade");
                break;
            case Condicoes.Conversar:
                NomeDoNPC = PlayerPrefs.GetString(Nome + "_QuestNPC");
                break;
            case Condicoes.Matar:
                NomeDoMonstro = PlayerPrefs.GetString(Nome + "_QuestMonstro");
                Quantidade = PlayerPrefs.GetInt(Nome + "_QuestQuantidade");
                break;
        }
    }
}
