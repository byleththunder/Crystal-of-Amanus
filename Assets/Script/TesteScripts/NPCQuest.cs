using UnityEngine;
using System.Collections;
using System;

public class NPCQuest : MonoBehaviour
{
    bool GetQuest = false;
    bool msgOn = false;
    public string NomeDaQuest;
    DataBase_Quests database;
    Quest QuestDoNpc;
    bool Complete = false;
    // Use this for initialization
    void Start()
    {
        database = new DataBase_Quests();
        try
        {
            QuestDoNpc = database.ChamarQuest(NomeDaQuest);
        }
        catch (Exception e)
        {
            Debug.Log("Nome da quest está errado ou a quest de nome: " + NomeDaQuest + " não existe");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Complete)
        {
            NPCQuest n = GetComponent<NPCQuest>();
            n.enabled = false;
        }

    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            msgOn = true;
            if (msgOn)
            {
                
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            msgOn = false;
        }
    }

   
}
