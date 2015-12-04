using UnityEngine;
using System.Collections;

public class ChangeCameraView : MonoBehaviour {

    public CameraRendering main;
    [Range(0,3)]
    public int indice = 0;
    int indicedefaut = 0;
    bool mudou = false;
	// Use this for initialization
	void Start () {
        main = Camera.main.GetComponent<CameraRendering>();
        indicedefaut = main.indice;
	}
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            main.indice = indice;
        }
    }
	
	
}
