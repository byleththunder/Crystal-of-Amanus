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
	}
	
	// Update is called once per frame
	void Update () {
	    if(InputManager.GetButtonDown("Start") && !OpenWindow && !Escape.activeInHierarchy)
        {
            if(Menu.activeInHierarchy == false)
            {
                Menu.SetActive(true);
                Time.timeScale = 0;
                
            }else
            {
                Menu.SetActive(false);
                Time.timeScale = 1;
            }
            

        }
        if (InputManager.GetButtonDown("Select") && !OpenWindow && !Menu.activeInHierarchy)
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
    public void IsWindowOpen(bool awnser)
    {
        OpenWindow = awnser;
    }
}
