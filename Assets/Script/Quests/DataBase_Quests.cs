using UnityEngine;
using System.Collections;
using System.Collections.Generic;



[AddComponentMenu("Scripts/Quests/Quest Database")]
public class DataBase_Quests : MonoBehaviour
{

    public static List<Quest> Lista = new List<Quest>();
    // Use this for initialization
    public DataBase_Quests()
    {
        if(Game.current != null)
        {
            if(Game.current.Quests.Count > 0)
            {
                Lista = Game.current.Quests;
            }
        }
    }
    static void SetQuest()
    {

    }
    public static List<Quest> PreencherLista(List<Quest> _temp)
    {
        if(_temp.Count == 0)
        {
            for (int i = 0; i < 6; i++)
            {
                _temp.Add(new Quest());
                _temp[i].GerarQuestAleatória();
            }
        }else
        {
            _temp.Add(new Quest());
            _temp[_temp.Count - 1].GerarQuestAleatória();
        }
        return _temp;
    }
    public static List<Quest> OrganizarLista()
    {
        for (int i = 0; i < 6; i++)
        {
            Lista.Add(new Quest());
            Lista[i].GerarQuestAleatória();
        }
        //Não mudar
        return Lista;
    }
    public Quest ChamarQuest(string nome)
    {
        List<Quest> Lista = OrganizarLista();
        return Lista.Find(X => X.Nome == nome);
    }
    public static List<Quest> GetList()
    {
        return Lista;
    }
    

}
