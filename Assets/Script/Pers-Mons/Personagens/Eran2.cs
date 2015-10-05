using UnityEngine;
using System.Collections;
using TeamUtility.IO;
using System.Collections.Generic;
using System.IO;


public class Eran2 : Character
{
    public static string Player;
    //Variaveis
    public Animator anim;
    Rigidbody rgd;
    bool OnTheFloor = false;
    bool IsRecover = false;
    bool left = false;
    //Velocidade da Fisica
    public Vector3 velocity;
    public float YOrigin;
    //
    public float Reducao;
    bool wait = false;
    bool teste = false;
    bool NaDar = false;
    //
    bool ShowDamage = false;
    List<Vector3> posDamage = new List<Vector3>();
    //
    public Eran2()
    {

        Nome = "Eran Airikina";
        Ataque = 10;
        VidaTotal = 100;
        Vida = VidaTotal;
        AmanusTotal = 100;
        Amanus = AmanusTotal;
        EstadoDoJogador = GameStates.CharacterState.Playing;
        Gold = 10000;


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
    void FixedUpdate()
    {


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
        if (Application.loadedLevel == 0)
        {
            //Destroy(gameObject);
        }
        if (Application.loadedLevelName == "GameOver")
        {
            if (!Escudo.activeInHierarchy)
            {
                Escudo.SetActive(true);
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
                Jump();
                if (OnTheFloor) { velocity = new Vector3(moveX * Speed, rgd.velocity.y, moveZ * Speed); }
                else if (!wait) { velocity = new Vector3(moveX * Speed / Reducao, velocity.y, moveZ * Speed / Reducao); }


                moveX = InputManager.GetAxis("Horizontal");
                moveZ = InputManager.GetAxis("Vertical");

                anim.SetFloat("Speed", Mathf.Abs(moveZ));
                anim.SetFloat("SpeedX", Mathf.Abs(moveX));

            }
            velocity.y = Mathf.Lerp(velocity.y, rgd.velocity.y, Time.deltaTime*20);
            rgd.velocity = velocity;

        }
        else
        {
            anim.SetTrigger("Idle");
            rgd.velocity = Vector3.zero;
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
        if (OnTheFloor && InputManager.GetButtonDown("Jump"))
        {

            float Forca = rgd.mass * moveY;
            anim.SetBool("Jump", true);
            //rgd.AddForce(transform.up * moveY);
            velocity.y = moveY;
            wait = true;
            StartCoroutine(JumpWait());
            teste = true;
            //notJump = true;
            OnTheFloor = false;
            return true;

        }
        return false;
    }
    IEnumerator JumpWait()
    {
        velocity = new Vector3(0, velocity.y, 0);
        yield return new WaitForSeconds(0.2f);
        velocity = new Vector3(moveX * Speed / Reducao, velocity.y, moveZ * Speed / Reducao);
        wait = false;
    }
    public override void Attack()
    {
        if (InputManager.GetButtonDown("Action") && !attack)
        {
            moveX = 0;
            moveZ = 0;
            anim.SetTrigger("Attack");
            attack = true;
            StartCoroutine(Wait());
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
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

}
