using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Interaction Script/Chest")]
public class Chest : MonoBehaviour {

    /// <summary>
    /// Quando o jogador ataca o baú, ele abre e entrega o item.
    /// </summary>

    public Animator anim;
    public Item loot;
    public int quantidade = 1;
    public bool Open = false;
    public SpriteRenderer Imagens;
    public bool FoiAberto = false; //Quero saber se o jogador realmente abriu ou já estava aberto.
    public bool Dinheiro = false;
    public int Quantia = 0;
    public Sprite spr;
	// Use this for initialization
	void Start () {
        Imagens = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        if (Dinheiro)
        {
            Imagens.sprite = spr;
        }
        else
        {
            Imagens.sprite = loot.Img;
        }
        if (!Open)
        {
            anim.SetFloat("speed", 0f);

        }else
        {
            anim.SetFloat("speed", 1f);
        }
        AlertMessage.PrefabPath = "Alert_Prefab";
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
                            if (Dinheiro == false)
                            {
                                col.gameObject.GetComponent<Inventario>().PickItem(loot, quantidade);
                            }else
                            {
                                col.gameObject.GetComponent<Character>().Gold += Quantia;
                            }
                            FoiAberto = true;
                            
                        }
                    }
                }
            }
        }
    }
}
