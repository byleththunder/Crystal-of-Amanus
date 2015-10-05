using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TeamUtility.IO;

public class VMenu : MonoBehaviour {
    public GameObject Menu,Escape;
    LayerMask Tudo;
	// Use this for initialization
	void Start () {
        Tudo = Camera.main.cullingMask;
	}
	
	// Update is called once per frame
	void Update () {
	    if(InputManager.GetButtonDown("Start"))
        {
            if(Menu.activeInHierarchy == false)
            {
                Menu.SetActive(true);
                //Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("HUD"));
                Time.timeScale = 0;
                
            }else
            {
                Menu.SetActive(false);
               // Camera.main.cullingMask = Tudo;
                Time.timeScale = 1;
            }
            

        }
        if (InputManager.GetButtonDown("Select"))
        {
            if (Escape.activeInHierarchy == false)
            {
                Escape.SetActive(true);
                Time.timeScale = 0;

            }
            else
            {
                Escape.SetActive(false);
                Time.timeScale = 1;
            }


        }
	}
    public void Continuar()
    {
        Escape.SetActive(false);
        Time.timeScale = 1;
    }
}
