using UnityEngine;
using System.Collections;
using TeamUtility.IO;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

[AddComponentMenu("Scripts/Personagens e Monstros/Personagens Sripts/Personagens/Eran")]
public class Eran2 : Character
{
    [HideInInspector]
    public static string Player;
    //Variaveis
    [Header("Animator Component para acessa-la do Script")]
    public Animator anim;
    Rigidbody rgd;
    bool OnTheFloor = false;
    bool IsRecover = false;
    bool left = false;
    //Velocidade da Fisica
    [HideInInspector]
    public Vector3 velocity;
    [HideInInspector]
    public float YOrigin;
    //
    [Header("Redução de Velocidade")]
    [Tooltip("Min 1 e Max 10")]
    [Range(1f, 10f)]
    public float Reducao;
    [Header("Trigger")]
    public SphereCollider AttackRange;
    bool wait = false;
    //
    bool ShowDamage = false;
    [Header("Painel ou Image que mostra vida do monstro")]
    public GameObject VidaMonstro;
    
    List<Vector3> posDamage = new List<Vector3>();
    //
    public Eran2()
    {
        ExpPadrao = 5;
        Nome = "Eran Airikina";
        AtaquePadrao = 10;
        VidaTotal = 100;
        Vida = VidaTotal;
        AmanusTotal = 100;
        Amanus = AmanusTotal;
        EstadoDoJogador = GameStates.CharacterState.Playing;
        Gold = 10000;
        UpdateStatus();


    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        obj = this.gameObject;
    }
    void Start()
    {

        rgd = GetComponent<Rigidbody>();
        rgd.freezeRotation = true;
        visao = TargetVision.Front;


    }
    void Update()
    {

        if (Alvo)
        {
            if (VidaMonstro != null)
            {
                if (!VidaMonstro.activeInHierarchy)
                    VidaMonstro.SetActive(true);
                ShowMonsterLife();
            }
        }else
        {
            if (VidaMonstro != null)
            {
                if (VidaMonstro.activeInHierarchy)
                    VidaMonstro.SetActive(false);
            }
        }
        if (IsRecover)
        {
            if (Amanus < AmanusTotal)
            {
                Amanus++;
            }
            else
            {
                IsRecover = false;
            }
        }



        if (EstadoDoJogador != GameStates.CharacterState.DontMove)
        {
            Jump();
            if (!IsInvoking("CheckRecoverAmanusAuto"))
            {
                Invoke("CheckRecoverAmanusAuto", 10f);
            }
            if (EstadoDoJogador != GameStates.CharacterState.Defense)
            {
                Attack();
            }
            if (!attack)
            {

                Movement();
                if (OnTheFloor) { velocity = new Vector3(moveX * Speed, rgd.velocity.y, moveZ * Speed); }
                else if (!wait) { velocity = new Vector3(moveX * Speed / Reducao, velocity.y, moveZ * Speed / Reducao); }


                moveX = InputManager.GetAxis("Horizontal");
                moveZ = InputManager.GetAxis("Vertical");

                anim.SetFloat("Speed", Mathf.Abs(moveZ));
                anim.SetFloat("SpeedX", Mathf.Abs(moveX));

            }
            
            rgd.velocity = velocity;

        }
        else
        {
            anim.SetTrigger("Idle");
            rgd.velocity = Vector3.zero;
        }
    }
    void FixedUpdate()
    {
        velocity.y = Mathf.Lerp(velocity.y, -1f, Time.deltaTime*2f );
        if(IsJump && OnTheFloor)
        {
            anim.SetBool("Jump", true);
            velocity.y = moveY;
            wait = true;
            StartCoroutine(JumpWait());
            OnTheFloor = false;
            IsJump = false;
        }
    }
    public override void Movement()
    {

        if (InputManager.GetAxisRaw("Vertical") == -1)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("Down"))
            {
                #region ResetTrigers
                anim.ResetTrigger("Up");
                anim.ResetTrigger("Right");
                anim.ResetTrigger("Left");
                #endregion
                anim.SetTrigger("Down");
                visao = TargetVision.Front;
            }
        }
        else if (InputManager.GetAxisRaw("Vertical") == 1)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("Up"))
            {
                #region ResetTrigers
                anim.ResetTrigger("Down");
                anim.ResetTrigger("Right");
                anim.ResetTrigger("Left");
                #endregion
                anim.SetTrigger("Up");
                visao = TargetVision.Back;
            }
        }
        else if (InputManager.GetAxisRaw("Horizontal") == -1)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("Left"))
            {
                #region ResetTrigers
                anim.ResetTrigger("Up");
                anim.ResetTrigger("Right");
                anim.ResetTrigger("Down");
                #endregion
                anim.SetTrigger("Left");
                visao = TargetVision.Left;
            }
        }
        else if (InputManager.GetAxisRaw("Horizontal") == 1)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("Right"))
            {
                #region ResetTrigers
                anim.ResetTrigger("Up");
                anim.ResetTrigger("Down");
                anim.ResetTrigger("Left");
                #endregion
                anim.SetTrigger("Right");
                visao = TargetVision.Right;
            }
        }

    }
    public override bool Jump()
    {
        if (OnTheFloor && InputManager.GetButtonDown("Jump") && !IsJump)
        {
            IsJump = true;
            return true;
        }
        return false;
    }
    IEnumerator JumpWait()
    {
        velocity = new Vector3(0, velocity.y, 0);
        yield return new WaitForSeconds(0.1f);
        velocity = new Vector3(moveX * Speed / Reducao, velocity.y, moveZ * Speed / Reducao);
        wait = false;
    }
    public override void Attack()
    {
        if (InputManager.GetButtonDown("Action") && !attack)
        {
            switch(visao)
            {
                case TargetVision.Back:
                    AttackRange.center = new Vector3(0, 0, 1);
                    break;
                case TargetVision.Front:
                    AttackRange.center = new Vector3(0, 0, -1);
                    break;
                case TargetVision.Left:
                    AttackRange.center = new Vector3(-1, 0, 0);
                    break;
                case TargetVision.Right:
                    AttackRange.center = new Vector3(1, 0, 0);
                    break;
            }
            moveX = 0;
            moveZ = 0;
            anim.SetTrigger("Attack");
            attack = true;
            StartCoroutine(Wait());
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.4f);
        attack = false;
    }
    public Vector3 Flip()
    {
        left = !left;
        Vector3 leftScale = transform.localScale;
        leftScale.x *= -1;
        transform.localScale = leftScale;
        return leftScale;
    }
    void CheckRecoverAmanusAuto()
    {
        if (Amanus < AmanusTotal)
        {
            IsRecover = true;
        }
    }

    void OnTriggerStay(Collider col)
    {
      

        if (col.gameObject.tag == "Monsters")
        {
            if (Alvo == null)
            {
                Alvo = col.gameObject.GetComponent<Target>();
            }
            if (attack)
            {
                col.gameObject.GetComponent<Target>().HealOrDamage(Ataque, 0);
                try
                {
                    col.gameObject.GetComponent<Monster>().anim.SetTrigger("Dano");
                }
                catch
                {

                }
                attack = false;
                ShowDamage = true;
                if (Alvo != null)
                {
                    var guiPosition = Camera.main.WorldToScreenPoint(Alvo.transform.position);
                    guiPosition.y = Screen.height - guiPosition.y;
                    posDamage.Add(guiPosition);
                }
            }
        }

    }

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "Restart")
        {
            transform.position = CheckPointPosition;
        }
        if (!OnTheFloor)
        {
            OnTheFloor = true;
            anim.SetBool("Jump", false);
        }
        if (col.gameObject.tag == "Monsters")
        {

            Alvo = col.gameObject.GetComponent<Monster>();

            print(Alvo.Nome);
        }

    }

    void OnGUI()
    {

        if (ShowDamage && Alvo != null)
        {

            for (int i = 0; i < posDamage.Count; i++)
            {
                posDamage[i] = new Vector3(posDamage[i].x, posDamage[i].y - 1, posDamage[i].z);
                GUI.Label(new Rect(posDamage[i].x, posDamage[i].y, Screen.width / 5, Screen.height / 5), "-" + Ataque.ToString());
                if (!IsInvoking("DesaparecerDano"))
                {
                    Invoke("DesaparecerDano", .5f);
                }
            }

        }
        else if (Alvo == null)
        {
            DesaparecerDano();
            CancelInvoke("DesaparecerDano");
        }
    }
    void DesaparecerDano()
    {
        for (int i = 0; i < posDamage.Count; i++)
        {
            posDamage.Remove(posDamage[i]);
        }
        ShowDamage = false;
    }

    public override void DeathState()
    {
        rgd.isKinematic = true;
        anim.SetTrigger("Death");

    }
    public override void ReviveState()
    {
        EstadoDoJogador = GameStates.CharacterState.Playing;
        rgd.isKinematic = false;
        anim.SetTrigger("Death");
    }
    void ShowMonsterLife()
    {
        float _life = (float)Alvo.VidaAtual / (float)Alvo.VidaTotal;
        _life = Mathf.Clamp(_life, 0, 1);
        VidaMonstro.GetComponentInChildren<Image>().fillAmount = _life;
        VidaMonstro.GetComponentInChildren<Text>().text = Alvo.Nome + "\t" + Alvo.VidaAtual + " / " + Alvo.VidaTotal;
    }
}
