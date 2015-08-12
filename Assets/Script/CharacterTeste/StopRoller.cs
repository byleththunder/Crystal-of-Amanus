using UnityEngine;
using System.Collections;

public class StopRoller : MonoBehaviour {
    public GameObject Objeto,Texto;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Objeto.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Texto.SetActive(true);
            Invoke("Sair", 2);
        }
    }
    void Sair()
    {
        Application.Quit();
    }
}
