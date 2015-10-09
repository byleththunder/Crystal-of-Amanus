using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Scripts/VisualScripts/Inventário")]
public class VInventario : MonoBehaviour {

    public VCitem Prefab;
    List<VCitem> Itens = new List<VCitem>();
    Inventario inv;
	// Use this for initialization
	void Start () {
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventario>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        var quantidade = inv.ComprimentoDaMochila();
        for(int i=0;i<quantidade; i++)
        {
            if(i+1>Itens.Count)
            {
                Itens.Add(Instantiate(Prefab));
                Itens[i].transform.SetParent(gameObject.transform, false);
                Itens[i].Indice = i;
            }
            if(Itens[i] == null)
            {
                Itens.RemoveAt(i);
            }else
            {
                Itens[i].Indice = i;
            }
        }
	}
}
