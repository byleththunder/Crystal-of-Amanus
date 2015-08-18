using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShowQuests : MonoBehaviour
{
    List<Quest> QuestsAleatorias;
    public Text Info;
    // Use this for initialization
    void Start()
    {
        QuestsAleatorias = DataBase_Quests.OrganizarLista();
        Info.text = AllInfo();
        print("Quests");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F2))
        {
            DataBase_Quests.Save(1);
            Info.text = "Save";
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            DataBase_Quests.Load(1);
            QuestsAleatorias = DataBase_Quests.GetList();
            Info.text = AllInfo();
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            DataBase_Quests.Delete(1);
        }
    }
    string AllInfo()
    {
        string temp = string.Empty;
        for (int i = 0; i < QuestsAleatorias.Count; i++)
        {
            if (i == 0)
            {
                temp = string.Format("------------------&------------\nNome: {0}\nObjetivo: {1}\nDescrição: {2}\n", QuestsAleatorias[i].Nome, QuestsAleatorias[i].Objetivo.ToString(), QuestsAleatorias[i].Descricao);
            }else
            {
                temp += string.Format("------------------&------------\nNome: {0}\nObjetivo: {1}\nDescrição: {2}\n", QuestsAleatorias[i].Nome, QuestsAleatorias[i].Objetivo.ToString(), QuestsAleatorias[i].Descricao);
            }
        }
        return temp;
    }
}
