using UnityEngine;
using System.Collections;

public class RochaDano : MonoBehaviour
{
    bool dano = false;
   
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (dano == false)
            {
                col.gameObject.GetComponent<Character>().HealOrDamage(5, 0);
                dano = true;
            }
        }
    }
}
