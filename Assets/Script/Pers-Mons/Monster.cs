using UnityEngine;
using System.Collections;

public class Monster : Target
{
    public Animator anim;
    public GameObject Morte;
    public bool HitTeste = false;
    protected int ExpEarn;
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
            ExpEarn = GameObject.FindObjectOfType<Character>().NextLevel / (5 * GameObject.FindObjectOfType<Character>().Level);
            print(ExpEarn);
            GameObject.FindGameObjectWithTag("Player").SendMessage("ChecarQuestMonstros", this);
            GameObject.FindGameObjectWithTag("Player").SendMessage("LevelUp", ExpEarn);
            if (Morte)
            {
                Instantiate(Morte, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
    void OnParticleCollision(GameObject other)
    {
        print(other.name);
        if (other.transform.parent.tag == "Player")
        {
            HitTeste = true;
            if (other.tag != "Reflect")
            {
                HealOrDamage(other.transform.parent.GetComponent<Character>().AtaqueAtual, 0);
            }else
            {
                print("Escudo-particle");
            }
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
