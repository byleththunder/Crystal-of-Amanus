using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Interaction Script/Porta Automatica")]
public class PortaAutomatica : MonoBehaviour
{

    /// <summary>
    /// Quando se aproxima da porta, ela abre.
    /// </summary>

    public Animator anim;
    
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
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
