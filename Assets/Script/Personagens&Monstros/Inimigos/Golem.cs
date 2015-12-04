using UnityEngine;
using System.Collections;

public class Golem : Monster {

    [Header("Agent")]
    [SerializeField]
    private NavMeshAgent nav;

    public GameObject Player;
    public ParticleSystem Rochas;
    public bool Hunting = true;
    bool RageAttck = false;
    bool Attack = false;
    
    int VidaPercent = 100;
	// Use this for initialization
	void Start () {
        nav = GetComponent<NavMeshAgent>();
        try
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }catch
        {
            Debug.LogWarning("O Golem não achou o personagem no método Start");
        }
        Vida = VidaTotal;
        Ataque = 2;
	}
	
	// Update is called once per frame
	void Update () {

        VidaPercent = (VidaAtual * 100) / VidaTotal;
        VidaPercent = Mathf.Clamp(VidaPercent, 0, 100);
        anim.SetInteger("Vida", VidaPercent);
        anim.SetFloat("Vel",nav.speed);
        
        //O Golem precisa saber quem é o personagem para começar a perseguir ele.
        if(Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            Debug.Log("O Golem achou o personagem");
        }
        //Se ele começar a caçar o jogar, ele coloca o jogador como destino.
        if(Hunting)
        {
            nav.SetDestination(Player.transform.position);
           // print("Seguindo");
        }
       //Se o Golem chegou ao jogador, ele ataca, e para de caçar. Ele só vai voltar a caçar quando o método Chase for chamado.
        if (!nav.pathPending)
        {
            if (nav.remainingDistance <= nav.stoppingDistance)
            {
                if (!nav.hasPath || nav.velocity.sqrMagnitude == 0f)
                {
                    if (!IsInvoking("Chase") && !Attack)
                    {
                        print("Atacar");
                        Hunting = false;
                        Invoke("Chase", 10f);
                        Attack = true;
                        anim.SetTrigger("Combo1");
                       
                    }
                }
            }
        }
        
        //Se a vida do Golem chegar à 50% e ele não tiver entrado em fúria, ele entra em fúria.
        if(VidaPercent == 50 && !RageAttck)
        {
            anim.SetTrigger("Rage");
            RageAttck = true;
        }

        DamageCheck();

	}
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if(Attack)
            {
                print("Dano");
                col.gameObject.GetComponent<Target>().HealOrDamage(Ataque, 0);
            }
        }
    }
    void Chase()
    {
        Hunting = true;
        Attack = false;
        Invoke("Atacar", 2f);
    }
    void Atacar()
    {
        Hunting = false;
        Invoke("Chase", 10f);
        Attack = true;
        anim.SetTrigger("Combo1");
    }
}
