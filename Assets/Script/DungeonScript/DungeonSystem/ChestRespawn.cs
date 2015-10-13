using UnityEngine;
using System.Collections;

public class ChestRespawn : MonoBehaviour {

    [Header("ChestPrefab")]
    public Chest Prefab;
    [Header("Item que está dentro do bau.")]
    public Item Iten;
    [Header("Quantidade do iten")]
    [Range(0,5)]
    public int _Quantidade = 0;
    [Header("Tamanho da caixa")]
    [Range(0, 1)]
    public float tamanho = 1;
    
	void Start () {
        //Você tem 50% de chance colocar o bau.
        if(Random.Range(0, 2) == 0)
        {
            Chest _temp = (Chest)Instantiate(Prefab);
            _temp.transform.SetParent(transform);
            _temp.transform.position = transform.position;
            _temp.transform.rotation = transform.rotation;
            _temp.transform.localScale = new Vector3(tamanho, tamanho, tamanho);
            _temp.loot = Iten;
            _temp.quantidade = _Quantidade;
        }
	}
	

    
}
