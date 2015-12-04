using UnityEngine;
using System.Collections;

public class ActivateTrap : MonoBehaviour {

    public GameObject[] Objetos;
    public MonoBehaviour[] Scripts;
	
    void OnTriggerEnter(Collider col)
    {
       if(col.gameObject.CompareTag("Player"))
       {
           for(int i = 0; i <Objetos.Length; i++)
           {
               Objetos[i].SetActive(true);
           }
           for (int i = 0; i < Scripts.Length; i++)
           {
               Scripts[i].enabled = true;
           }
           this.enabled = false;
       }
    }
}
