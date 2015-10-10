using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Interaction Script/Plataforma Move")]
public class PlataformaMove : MonoBehaviour
{
    /// <summary>
    /// Quando o jogador pula na plataforma, ele vira filho do objeto. Quando sai do objeto, ele perde o parentesco.
    /// </summary>
    
    
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
