using UnityEngine;
using System.Collections;
using System;
public class SunLight : MonoBehaviour {
    public int Hora;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float rotation = (90 * Hora) / 12;
        Debug.Log(DateTime.Now.Hour);
        
        transform.localEulerAngles = new Vector3(rotation, 90, 0);
        
	}
}
