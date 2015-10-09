using UnityEngine;
using System.Collections;


public class Fire : Projectile {
    
    public FireMagic Pool;
    bool contdown = false;
    float Time = 2f;
    public int Damage = 0;
    Rigidbody rgd;
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
        rgd = GetComponent<Rigidbody>();
        rgd.velocity = Vector3.zero ;
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
            Damage = (Pool.Personagem.AtaqueAtual * 100) / 100;
            Reflect = false;
        }
       
    }
    void FixedUpdate()
    {
        rgd.AddRelativeForce(Vector3.forward*2);
    }
	void OnCollisionEnter(Collision col)
    {
        if ( col.gameObject.tag != WhoShoot && col.gameObject.tag != "Reflect")
        {
            if (col.gameObject.tag == Tag)
            {
                Target temp = col.gameObject.GetComponent<Target>();
                temp.HealOrDamage(Damage, 0);
                rgd.velocity = Vector3.zero;
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
        rgd.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
