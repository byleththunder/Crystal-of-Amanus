using UnityEngine;
using System.Collections;

public class LiberarPorta : MonoBehaviour {

    public Animator Porta;
    public Animator Alavanca;
    void Start()
    {

    }
    void OnCollisionStay(Collision col)
    {
        if(col.collider.gameObject.CompareTag("Player"))
        {
            if(GameInput.GetKeyDown(InputsName.Action))
            {
                print("Oi");
                Alavanca.SetTrigger("Press");
                Porta.enabled = true;
                this.enabled = false;
            }
        }
    }
}
