using UnityEngine;
using System.Collections;

public class CallGameObject : MonoBehaviour {
    public string TagAlert = "Player";
    public GameObject Objeto;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagAlert)
        {
            Objeto.SetActive(true);
        }
    }
}
