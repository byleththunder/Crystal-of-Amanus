using UnityEngine;
using System.Collections;

public class Monster : Target
{
    public Animator anim;
    void Update()
    {
       
        DamageCheck();
    }
    public virtual Item Loot()
    {
        return null;
    }
    public virtual void AI()
    {

    }
    void DamageCheck()
    {
        if (Vida <= 0)
        {
            Destroy(gameObject);
        }
    }

}
