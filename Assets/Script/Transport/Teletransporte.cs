using UnityEngine;
using System.Collections;

public class Teletransporte : MonoBehaviour {
    public Transform Destino;
	// Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.position = Destino.position;
        }
    }
}
