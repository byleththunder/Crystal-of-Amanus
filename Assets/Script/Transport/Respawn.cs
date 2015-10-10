using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Transportes/Respawn")]
public class Respawn : MonoBehaviour {
    public GameObject Personagem;
	// Use this for initialization
    
	void Start () {
        Personagem = GameObject.FindGameObjectWithTag("Player");
        Personagem.transform.position = transform.position;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
