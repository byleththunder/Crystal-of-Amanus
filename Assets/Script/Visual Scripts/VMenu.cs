using UnityEngine;
using UnityEngine.UI;
using System.Collections;
[AddComponentMenu("Scripts/VisualScripts/Menu", 0)]
public class VMenu : MonoBehaviour {
    public GameObject Menu,Escape,Help;
    bool OpenWindow = false;
    bool Configuracoes = false;
	// Use this for initialization
	void Start () {
        //Menu = GameObject.Find("Canvas").transform.FindChild("Menu").gameObject;
        //Escape = Menu.transform.FindChild("Escape").gameObject;
        Help = GameObject.FindGameObjectWithTag("Help");
        Help.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        if (GameInput.GetKeyDown(InputsName.Start) && !Escape.activeInHierarchy && !Help.activeInHierarchy && !Configuracoes)
        {
            if (Menu.activeInHierarchy == false && !GameStates.IsAWindowOpen)
            {
                GameStates.IsAWindowOpen = true;
                Menu.SetActive(true);
                Time.timeScale = 0;
                
            }else if(Menu.activeInHierarchy == true && GameStates.IsAWindowOpen)
            {
                GameStates.IsAWindowOpen = false;
                Menu.SetActive(false);
                Time.timeScale = 1;
            }
            

        }
        if (GameInput.GetKeyDown(InputsName.Select) && !Menu.activeInHierarchy && !Help.activeInHierarchy && !Configuracoes)
        {
            if (Escape.activeInHierarchy == false && !GameStates.IsAWindowOpen)
            {
                GameStates.IsAWindowOpen = true;
                Escape.SetActive(true);
                Time.timeScale = 0;

            }
            else if (Escape.activeInHierarchy == true && GameStates.IsAWindowOpen)
            {
                GameStates.IsAWindowOpen = false;
                Escape.SetActive(false);
                Time.timeScale = 1;
            }


        }
        if (Input.GetKeyDown(KeyCode.F1) && !Menu.activeInHierarchy && !Escape.activeInHierarchy && !Configuracoes)
        {
            if (Help.activeInHierarchy == false && !GameStates.IsAWindowOpen)
            {
                GameStates.IsAWindowOpen = true;
                Help.SetActive(true);
                Time.timeScale = 0;

            }
            else if (Help.activeInHierarchy == true && GameStates.IsAWindowOpen)
            {
                GameStates.IsAWindowOpen = false;
                Help.SetActive(false);
                Time.timeScale = 1;
            }


        }
	}
    public void Continuar()
    {
        Escape.SetActive(false);
        Configuracoes = false;
        GameStates.IsAWindowOpen = false;
        Time.timeScale = 1;
    }
    public void IsWindowOpen(bool awnser)
    {
        if(awnser)
        {
            Time.timeScale = 0;
            GameStates.IsAWindowOpen = true;

        }else
        {
            Time.timeScale = 1;
            GameStates.IsAWindowOpen = false;
        }
    }
    public void OpenOrClose(bool awnser)
    {
        Configuracoes = awnser;
    }
}
