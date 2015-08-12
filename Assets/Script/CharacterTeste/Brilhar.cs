using UnityEngine;
using System.Collections;

public class Brilhar : MonoBehaviour {
    int indice = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnParticleCollision(GameObject other)
    {
        Brilhando();
    }
    void Brilhando()
    {
        indice = (indice + 1) % 3;
        if (indice == 0)
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.black;
        }
    }
}
