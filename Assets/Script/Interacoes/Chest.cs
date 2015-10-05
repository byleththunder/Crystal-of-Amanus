using UnityEngine;
using System.Collections;

public class Chest : MonoBehaviour {

    public Animator anim;
    public Item loot;
    public int quantidade = 1;
    public bool Open = false;
    public SpriteRenderer Imagens;
	// Use this for initialization
	void Start () {
        Imagens = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Imagens.sprite = loot.Img;
        if (!Open)
        {
            anim.SetFloat("speed", 0f);

        }else
        {
            anim.SetFloat("speed", 1f);
        }
	}
	void Update()
    {
        if(anim.GetFloat("speed") == 0 && Open)
        {
            anim.SetFloat("speed", 1f);
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (!Open)
        {
            if (col.gameObject.tag == "Player")
            {
                if (anim.GetFloat("speed") == 0)
                {
                    if (Input.GetButtonDown("Action"))
                    {
                        anim.SetFloat("speed", 1f);
                        if (loot)
                        {
                            Open = true;
                            col.gameObject.GetComponent<Inventario>().PickItem(loot, quantidade);
                            //MessageBox.Instance.WriteMessage(loot.Nome + " x"+quantidade);
                        }
                    }
                }
            }
        }
    }
}
