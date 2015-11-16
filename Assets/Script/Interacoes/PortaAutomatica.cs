using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Interaction Script/Porta Automatica")]
public class PortaAutomatica : MonoBehaviour
{

    /// <summary>
    /// Quando se aproxima da porta, ela abre.
    /// </summary>

    public Animator anim;
    
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            anim.ResetTrigger("Close");
            anim.SetTrigger("Open");
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            anim.ResetTrigger("Close");
            anim.SetTrigger("Close");
        }
    }
}
