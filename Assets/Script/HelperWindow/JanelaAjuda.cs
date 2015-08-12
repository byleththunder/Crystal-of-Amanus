using UnityEngine;
using System.Collections;

public class JanelaAjuda : MonoBehaviour {

    public GameObject Painel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	   if(Input.GetKeyDown(KeyCode.F1))
       {
           if(Painel.activeInHierarchy)
           {
               Painel.SetActive(false);
           }else
           {
               Painel.SetActive(true);
           }
       }
	}
}
