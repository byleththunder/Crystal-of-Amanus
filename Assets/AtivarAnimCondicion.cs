using UnityEngine;
using System.Collections;

public class AtivarAnimCondicion : MonoBehaviour {

    public Animator anim;
    public string TriggerName = "Cai";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger(TriggerName);
        }
    }
}
