using UnityEngine;
using System.Collections;

public class PlataformaMove : MonoBehaviour
{
    
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.transform.SetParent(transform);
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            transform.DetachChildren();
        }
    }
}
