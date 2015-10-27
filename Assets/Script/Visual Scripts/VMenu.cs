using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TeamUtility.IO;
[AddComponentMenu("Scripts/VisualScripts/Menu", 0)]
public class VMenu : MonoBehaviour {
    public GameObject Menu,Escape;
    bool OpenWindow = false;
	// Use this for initialization
	void Start () {
        //Menu = GameObject.Find("Canvas").transform.FindChild("Menu").gameObject;
        //Escape = Menu.transform.FindChild("Escape").gameObject;
	}
	
	// Update is called once per frame
	void Update () {

	    if(InputManager.GetButtonDown("Start")  && !Escape.activeInHierarchy)
        {
            if (Menu.activeInHierarchy == false && !GameStates.IsAWindowOpen)
            {
                GameStates.IsAWindowOpen = true;
                Menu.SetActive(true);
                Time.timeScale = 0;
                
            }else
            {
                GameStates.IsAWindowOpen = false;
                Menu.SetActive(false);
                Time.timeScale = 1;
            }
            

        }
        if (InputManager.GetButtonDown("Select")  && !Menu.activeInHierarchy)
        {
            if (Escape.activeInHierarchy == false && !GameStates.IsAWindowOpen)
            {
                GameStates.IsAWindowOpen = true;
                Escape.SetActive(true);
                Time.timeScale = 0;

            }
            else
            {
                GameStates.IsAWindowOpen = false;
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
    public void IsWindowOpen(bool awnser)
    {
        OpenWindow = awnser;
    }
}
