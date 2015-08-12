using UnityEngine;
using System.Collections;

public class ShiedActions : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Projetil")
        {
            col.transform.forward = transform.forward;
            Projectile _missle = col.gameObject.GetComponent<Projectile>();
            _missle.Reflect = true;
            _missle.Tag = "Monsters";
            _missle.WhoShoot = "Player";
            
        }
    }
}
