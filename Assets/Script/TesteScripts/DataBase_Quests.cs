using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DataBase_Quests  {

	static Quest primeira = new Quest(TiposDeQuests.Principal, "Quest 1 - Socialize com o Npc Teleport", "Você está muito só, precisa conversar com alguém, por quê não o NpcTeleport?");
    static Quest Segunda = new Quest(TiposDeQuests.Principal, "Quest 2 - Socialize com o Npc Quest", "Você está muito só, precisa conversar com alguém, por quê não o Npc Quest?");
    static Quest Terceira = new Quest(TiposDeQuests.Principal, "Quest 3", "Sem descrição");
    static Quest Quarta = new Quest(TiposDeQuests.Principal, "Quest 4", "Sem descrição");
    static Quest Quinta = new Quest(TiposDeQuests.Principal, "Quest 5", "Sem descrição");
    static Quest Sexta = new Quest(TiposDeQuests.Principal, "Quest 6", "Sem descrição");
    static Quest Setimo = new Quest(TiposDeQuests.Principal, "Quest 7", "Sem descrição");
    static Quest Oitavo = new Quest(TiposDeQuests.Principal, "Quest 8", "Sem descrição");
    static Quest Nona = new Quest(TiposDeQuests.Principal, "Quest 9", "Sem descrição");
	// Use this for initialization
	public DataBase_Quests () {
        
        
        
	}
	static void SetQuest()
	{
        primeira.SetupConversa("NpcTeleport");
        Segunda.SetupConversa("NpcQuest");
        /*Terceira.SetupColeta(Item_DataBase.LocalizarItem("Poção de HP"), 3);
        Quarta.SetupColeta(Item_DataBase.LocalizarItem("Poção de HP"), 6);
        Quinta.SetupColeta(Item_DataBase.LocalizarItem("Poção de HP"), 7);
        Sexta.SetupColeta(Item_DataBase.LocalizarItem("Poção de HP"), 10);
        Setimo.SetupColeta(Item_DataBase.LocalizarItem("Poção de HP"), 10);
        Oitavo.SetupColeta(Item_DataBase.LocalizarItem("Poção de HP"), 10);
        Nona.SetupColeta(Item_DataBase.LocalizarItem("Poção de HP"), 10);*/
	}
    public static List<Quest> OrganizarLista()
    {
		SetQuest ();
        List<Quest> Lista = new List<Quest>();
        //Adicione a Quest criada
        Lista.Add(primeira);
		Lista.Add(Segunda);
        /*Lista.Add(Terceira);
        Lista.Add(Quarta);
        Lista.Add(Quinta);
        Lista.Add(Sexta);
        Lista.Add(Setimo);
        Lista.Add(Oitavo);
        Lista.Add(Nona);*/
        //Não mudar
        return Lista;
    }
    public Quest ChamarQuest(string nome)
    {
        List<Quest> Lista = OrganizarLista();
        return Lista.Find(X => X.Nome == nome);
    }
    
	
	
}
