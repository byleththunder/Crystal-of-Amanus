using UnityEngine;
using System.Collections;


public class Fire : Projectile {
    
    public FireMagic Pool;
    bool contdown = false;
    float Time = 2f;
    public int Damage = 0;
    
    public Fire()
    {
        Type = ProjectType.Fire;
        Reflect = false;
        
    }
    void Start()
    {
        if(Pool == null)
        {
            Pool = GameObject.Find("Fogo").GetComponent<FireMagic>();
        }
    }
    void Update()
    {
        if(!contdown)
        {
            Invoke("Fade",Time);
            contdown = true;
        }
        if(Reflect)
        {
            if(IsInvoking("Fade"))
            {
                CancelInvoke("Fade");
                Invoke("Fade", Time);
            }
            Damage = (Pool.Personagem.Ataque * 100) / 100;
            Reflect = false;
        }
        transform.position += transform.forward*0.1f;
    }
	void OnCollisionEnter(Collision col)
    {
        if ( col.gameObject.tag != WhoShoot && col.gameObject.tag != "Reflect")
        {
            if (col.gameObject.tag == Tag)
            {
                ITarget temp = col.gameObject.GetComponent<ITarget>();
                temp.StatsChange(Damage, 0);
            }
            if (IsInvoking())
            {
                CancelInvoke();
                contdown = false;
            }
            gameObject.SetActive(false);
            
        }
    }
    void Fade()
    {
        contdown = false;
        gameObject.SetActive(false);
    }
}
