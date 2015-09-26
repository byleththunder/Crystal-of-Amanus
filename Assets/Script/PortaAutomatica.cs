using UnityEngine;
using System.Collections;

public class PortaAutomatica : MonoBehaviour
{
    public Animator anim;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerStay(Collider col)
    {
        print("oi");
        if (col.gameObject.tag == "Player")
        {
            print("oi");
            anim.SetFloat("speed", 1f);
        }
    }
    void OnTriggerExit(Collider col)
    {

        if (col.gameObject.tag == "Player")
        {
            anim.SetFloat("speed", -1f);
        }
    }
}
