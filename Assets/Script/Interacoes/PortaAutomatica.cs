using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Interaction Script/Porta Automatica")]
public class PortaAutomatica : MonoBehaviour
{

    /// <summary>
    /// Quando se aproxima da porta, ela abre.
    /// </summary>

    public Animator anim;
    
    void Start()
    {

    }
    void OnTriggerEnter(Collider col)
    {
        
            if (col.gameObject.CompareTag("Player"))
            {
                if (anim != null)
                {
                    //anim.ResetTrigger("Close");
                    anim.SetTrigger("Open");
                }
            }
        
    }
    void OnTriggerExit(Collider col)
    {
       
            if (col.gameObject.CompareTag("Player"))
            {
                if (anim != null)
                {
                    //anim.ResetTrigger("Open");
                    anim.SetTrigger("Close");
                }
            }
        
    }
}
