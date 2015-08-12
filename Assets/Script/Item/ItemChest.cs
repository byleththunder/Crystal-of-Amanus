using UnityEngine;
using System.Collections;

public class ItemChest : MonoBehaviour {
    public Item ItemEscondido;
    public int Quantidade;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            Inventario temp = col.gameObject.GetComponent<Inventario>();
            temp.PickItem(ItemEscondido, Quantidade);
            Destroy(gameObject);
        }
    }
}
