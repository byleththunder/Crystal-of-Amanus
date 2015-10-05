using UnityEngine;
using System.Collections;

public class ShiedActions : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnParticleCollision(GameObject obj)
    {
        if(obj.layer == 11)//Monster Layer
        {
            
        }
    }
}
