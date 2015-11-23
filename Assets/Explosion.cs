using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    public Material mat;
    bool inicio = false;
	// Use this for initialization
	void Start () {
        mat.SetFloat("_Swt", 0);
	}
	
	public void ExplosionObject()
    {
        if (!inicio)
        {
            //mat.SetFloat("_Swt", 1);
            //Invoke("Destroy", 1);
            inicio = true;
        }
    }
    void Destroy()
    {
        Destroy(gameObject);
    }
}
