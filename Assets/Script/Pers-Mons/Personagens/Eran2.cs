using UnityEngine;
using System.Collections;

public class Eran2 : Character
{
    //Variaveis
    public Animator anim;
    Rigidbody rgd;
    bool OnTheFloor;
    bool IsRecover = false;
    bool left = false;
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
            if (Jump())
            {
                return;
            }
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
                rgd.velocity = new Vector3(moveX * Speed, rgd.velocity.y, moveZ * Speed);
                moveX = Input.GetAxis("Horizontal");
                moveZ = Input.GetAxis("Vertical");

            }

        }
        else
        {
            anim.SetTrigger("Idle");
            rgd.velocity = Vector3.zero;
        }
    }
    public override void Movement()
    {
        //if (moveX < 0 && !left)
        //{
        //    Flip();
        //}
        //else if ((moveX > 0) && left)
        //{
        //    Flip();
        //}

        if (moveZ == 0 && moveX == 0)
        {
            anim.SetTrigger("Idle");
        }
        else
        {
            if (moveZ < 0 && Mathf.Abs(moveZ) > Mathf.Abs(moveX))
            {
                anim.SetTrigger("Down");
                visao = TargetVision.Front;
            }
            else if (moveZ > 0 && Mathf.Abs(moveZ) > Mathf.Abs(moveX))
            {
                anim.SetTrigger("Up");
                visao = TargetVision.Back;
            }

            else if ((moveX < 0 || moveX > 0) && (Mathf.Abs(moveZ) < Mathf.Abs(moveX) || Mathf.Abs(moveZ) == Mathf.Abs(moveX)))
            {

                if (moveX < 0)
                {
                    anim.SetTrigger("Left");
                    visao = TargetVision.Left;
                }
                else
                {
                    anim.SetTrigger("Right");
                    visao = TargetVision.Right;
                }
            }

        }


    }
    public override bool Jump()
    {
        Ray raio = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(raio, out hit, 2f))
        {
            Debug.DrawLine(raio.origin, hit.point);
            GameObject obj = hit.collider.GetComponent<GameObject>();
            if (notJump)
            {
                notJump = false;
            }
        }
        //!notJump &&
        //Não está funcionando o raycast no terreno importado.
        if (OnTheFloor && Input.GetButtonDown("ControlCharacter"))
        {
            rgd.velocity = new Vector3(rgd.velocity.x / 4, 1, rgd.velocity.z / 4);
            anim.SetBool("Jump 0", true);
            rgd.AddForce(Vector3.up * moveY, ForceMode.Impulse);
            //notJump = true;
            OnTheFloor = false;
            return true;

        }
        return false;
    }
    public override void Attack()
    {
        if (Input.GetButtonDown("Action") && !attack)
        {
            moveX = 0;
            moveZ = 0;
            if (visao == TargetVision.Left)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            anim.SetTrigger("Attack");
            attack = true;
            StartCoroutine(Wait());
            if (Alvo != null)
            {
                Alvo.HealOrDamage(20, 0);
                print(Alvo.Vida);
            }
        }
    }
    IEnumerator Wait()
    {
        anim.SetTrigger("Idle");
        yield return new WaitForSeconds(0.5f);
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
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "enemy")
        {
            if (attack)
            {
                Destroy(other.gameObject);
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
            anim.SetBool("Jump 0", false);
            print("Chao");
        }
        if (col.gameObject.tag == "Monsters")
        {

            Alvo = col.gameObject.GetComponent<Target>();
            print(Alvo.Nome);
        }

    }
    
}
