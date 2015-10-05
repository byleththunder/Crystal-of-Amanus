using UnityEngine;
using System.Collections;

public class Monster : Target
{
    public Animator anim;
    public GameObject Morte;
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
            GameObject.FindGameObjectWithTag("Player").SendMessage("ChecarQuestMonstros", this);
            if (Morte)
            {
                Instantiate(Morte, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
    void OnParticleCollision(GameObject other)
    {
        if (other.transform.parent.tag == "Player")
        {
            HealOrDamage(other.transform.parent.GetComponent<Character>().Ataque, 0);
            try
            {
                anim.SetTrigger("Dano");
            }
            catch
            {

            }
        }
    }

}
