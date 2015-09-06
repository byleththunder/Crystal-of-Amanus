using UnityEngine;
using System.Collections;

public class Chest : MonoBehaviour {

    public Animator anim;
    public Item loot;
    public int quantidade = 1;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        
            anim.SetFloat("speed", 0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            if (anim.GetFloat("speed") == 0)
            {
                if (Input.GetButtonDown("Action"))
                {
                    anim.SetFloat("speed", 4f);
                    if(loot)
                    {
                        col.gameObject.GetComponent<Inventario>().PickItem(loot, quantidade);
                    }
                }
            }
        }
    }
}
