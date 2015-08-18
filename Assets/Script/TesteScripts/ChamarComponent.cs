using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class ChamarComponent : MonoBehaviour
{

    public enum TipoDeAcao { SomenteScripts, ScriptsEGameObject };
    public List<MonoBehaviour> Scripts;
    public List<int> index, Index2;
    public List<GameObject> objs;
    public int size = 0;
    List<string> Ordem;
    public TipoDeAcao decisao;
    int indice = 0;
    bool start = false;
    bool comecar = true;
    
    // Use this for initialization
    void Start()
    {
        Ordem = new List<string>();
        if (decisao == TipoDeAcao.ScriptsEGameObject)
        {
            
            for (int j = 0; j < Scripts.Count; j++)
            {
                if (Scripts[j] != null)
                {
                    Ordem.Add("Script");
                    
                }
                else if (objs[j] != null)
                {
                    Ordem.Add("GameObject");
                    
                }
                
            }
        }
        for (int i = 0; i < Scripts.Count; i++)
        {
            if (Scripts[i] != null)
            {
                Scripts[i].enabled = false;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (start)
        {
            if (decisao == TipoDeAcao.SomenteScripts)
            {
                if (!Scripts[indice].enabled)
                {
                    if (indice < Scripts.Count - 1)
                    {
                        indice++;
                        Scripts[indice].enabled = true;

                    }
                    else
                    {
                        WorldVars.Freeze = false;
                        indice = 0;
                        start = false;

                        comecar = false;
                        Invoke("Recomecar", 1);
                    }

                }
            }
            else
            {
                if (Ordem[indice] == "GameObject")
                {
                    if (!objs[indice].active)
                    {
                        if (indice < size - 1)
                        {
                            indice++;
                            if (Ordem[indice] == "GameObject")
                            {
                                objs[indice].SetActive(true);
                            }
                            else if (Ordem[indice] == "Script")
                            {
                                Scripts[indice].enabled = true;
                            }
                        }
                        else
                        {
                            WorldVars.Freeze = false;
                            indice = 0;
                            start = false;

                            comecar = false;
                            Invoke("Recomecar", 1);
                        }
                    }
                }
                else if (Ordem[indice] == "Script")
                {
                    if (!Scripts[indice].enabled)
                    {
                        if (indice < size - 1)
                        {
                            indice++;
                            if (Ordem[indice] == "GameObject")
                            {
                                objs[indice].SetActive(true);
                            }
                            else if (Ordem[indice] == "Script")
                            {
                                Scripts[indice].enabled = true;
                            }
                        }
                        else
                        {
                            WorldVars.Freeze = false;
                            indice = 0;
                            start = false;

                            comecar = false;
                            Invoke("Recomecar", 1);
                        }
                    }
                }
            }
        }
    }
    void Recomecar()
    {
        comecar = true;
        
    }
   
    
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetButtonDown("Action") && !start && comecar)
            {
                
                if (other.tag == "Player" && gameObject.tag == "NPC")
                {
                    //Uma Conversa aconteceu
                }
                WorldVars.Freeze = true;
                if (decisao == TipoDeAcao.SomenteScripts)
                {
                    indice = 0;
                    start = true;
                    Scripts[indice].enabled = true;
                }
                else
                {
                    indice = 0;
                    start = true;
                    if (Ordem[indice] == "GameObject")
                    {
                        objs[indice].SetActive(true);
                    }
                    else if (Ordem[indice] == "Script")
                    {
                        Scripts[indice].enabled = true;
                    }
                }

            }
        }
    }
}
