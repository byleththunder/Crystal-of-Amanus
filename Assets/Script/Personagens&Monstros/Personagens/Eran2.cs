using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;


[AddComponentMenu("Scripts/Personagens e Monstros/Personagens Sripts/Personagens/Eran")]
public class Eran2 : Character
{
    [Space(.5f)]
    [Header("Eran")]
    [Space(.5f)]

    [HideInInspector]
    public static string Player;
    //Variaveis
    [Header("Animator Component para acessa-la do Script")]
    public Animator anim;
    Rigidbody rgd;
    bool OnTheFloor = false;
    bool IsRecover = false;
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
        Nome = "Eran Airikina";
        AtaquePadrao = 10;
        //if (!LoadInfo())
        //{
            
        //}
        VidaTotal = 100;
        AmanusTotal = 100;
        Gold = 0;
        UpdateStatus();
        Vida = VidaTotal;
        Amanus = AmanusTotal;
        EstadoDoJogador = GameStates.CharacterState.Playing;


    }
    public bool LoadInfo()
    {
        if (Game.current != null)
        {
            //Vou verificar se esse é um novo jogo.
            if (Game.current.Player_Pos != null)
            {
                //Se não for um novo jogo, eu vou carregar as configurações anteriores.
                transform.position = Game.current.Player_Pos;
                Level = Game.current.Player_Level;
                VidaTotal = Game.current.Player_Vida;
                AmanusTotal = Game.current.Player_Amanus;
                Ataque = Game.current.Player_Ataque;
                Exp = Game.current.Player_Exp;
                Gold = Game.current.Player_Argento;
                Equipamentos[0] = ResourceFind.FindItem(Game.current.Player_Equipamentos[0]);
                Equipamentos[1] = ResourceFind.FindItem(Game.current.Player_Equipamentos[1]);
                return true;
            }
        }
        return false;
    }
    public void SaveInfo()
    {
        if (Game.current != null)
        {

            //Se não for um novo jogo, eu vou carregar as configurações anteriores.
            Game.current.Player_Pos = transform.position;
            Game.current.Player_Level = Level;
            Game.current.Player_Vida = VidaTotal;
            Game.current.Player_Amanus = AmanusTotal;
            Game.current.Player_Ataque = Ataque;
            Game.current.Player_Exp = Exp;
            Game.current.Player_Argento = Gold;
            if (Equipamentos[0] != null)
                Game.current.Player_Equipamentos[0] = Equipamentos[0].name;
            if (Equipamentos[1] != null)
                Game.current.Player_Equipamentos[1] = Equipamentos[1].name;
        }
    }
    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        try
        {
            CheckPointPosition = GameObject.Find("InitialPosition").transform.position;
        }
        catch
        {

        }
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
        }
        else
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


                moveX = GameInput.GetAxis(InputsName.Horizontal);//InputManager.GetAxis("Horizontal");
                moveZ = GameInput.GetAxis(InputsName.Vertical);//InputManager.GetAxis("Vertical");

                anim.SetFloat("Speed", Mathf.Abs(moveZ));
                anim.SetFloat("SpeedX", Mathf.Abs(moveX));

            }

            rgd.velocity = velocity;

        }
        else
        {
            rgd.velocity = Vector3.zero;
            anim.SetFloat("Speed", 0);
            anim.SetFloat("SpeedX", 0);
        }
    }
    void FixedUpdate()
    {
        velocity.y = Mathf.Lerp(velocity.y, -5f, Time.deltaTime * 2f);
        if (IsJump && OnTheFloor)
        {
            anim.SetBool("Jump", true);
            velocity.y = moveY;
            OnTheFloor = false;
            IsJump = false;
        }
        SaveInfo();
    }
    public override void Movement()
    {

        if (GameInput.GetAxisRaw(InputsName.Vertical) == -1)
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
        else if (GameInput.GetAxisRaw(InputsName.Vertical) == 1)
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
        else if (GameInput.GetAxisRaw(InputsName.Horizontal) == -1)
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
        else if (GameInput.GetAxisRaw(InputsName.Horizontal) == 1)
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
        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
        //{
        //    if (InputManager.GetButtonDown("Jump"))
        //    {
        //        IsJump = true;
        //        return true;
        //    }
        //}

        if (OnTheFloor && GameInput.GetKeyDown(InputsName.Jump) && !IsJump)
        {
            IsJump = true;
            return true;
        }
        return false;
    }
   
    public override void Attack()
    {
        if (GameInput.GetKeyDown(InputsName.Action) && !attack)
        {
           // SoundEffects[0].Play();
            switch (visao)
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
            DamageAttk = true;
            StartCoroutine(Wait());
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.4f);
        DamageAttk = false;
        attack = false;
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

        if (col.gameObject.tag == "Destruivel")
        {
            if (DamageAttk)
            {
                col.gameObject.SendMessage("ExplosionObject");
                DamageAttk = false;
            }
        }
        if (col.gameObject.tag == "Monsters")
        {
            if (Alvo == null)
            {
                Alvo = col.gameObject.GetComponent<Target>();
            }
            if (DamageAttk)
            {
                col.gameObject.GetComponent<Target>().HealOrDamage(Ataque, 0);
                try
                {
                    col.gameObject.GetComponent<Monster>().anim.SetTrigger("Dano");
                }
                catch
                {

                }
                DamageAttk = false;
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
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Monsters")
        {
            Alvo = null;
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "CheckPoint")
        {
            CheckPointPosition = col.transform.position;
            col.gameObject.SetActive(false);
        }
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
                GUI.skin.label.fontSize = 16;
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
    public override void HealOrDamage(int _vida, int _amanus)
    {
        base.HealOrDamage(_vida, _amanus);
        if (_vida > 0)
        {
            anim.SetTrigger("Dano");
        }
    }
    void ShowMonsterLife()
    {
        float _life = (float)Alvo.VidaAtual / (float)Alvo.VidaTotal;
        _life = Mathf.Clamp(_life, 0, 1);
        VidaMonstro.transform.FindChild("barra").GetComponent<Image>().fillAmount = _life;
        VidaMonstro.GetComponentInChildren<Text>().text = Alvo.Nome + "\t" + Alvo.VidaAtual + " / " + Alvo.VidaTotal;
    }
}
