using UnityEngine;
using System.Collections;

public class CallMenu : MonoBehaviour {
    public GameObject Menu;
    bool funcionou = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (this.enabled)
        {
            if (!Menu.active && !funcionou)
            {
                Menu.SetActive(true);
                funcionou = true;
            }
            this.enabled = Menu.active;
            if (!Menu.active && funcionou)
            {
                funcionou = false;
            }
        }
	}
}
