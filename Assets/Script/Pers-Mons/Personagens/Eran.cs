using UnityEngine;
using System.Collections;

public class Eran : Character {
    //Variaveis
    public Animator anim;
    Rigidbody rgd;
    bool OnTheFloor;
    bool IsRecover = false;
    bool left = false;
    //
    public Eran()
    {
        Nome = "Eran Airikina";
        Ataque = 10;
        VidaTotal = 100;
        Vida = VidaTotal;
        AmanusTotal = 100;
        Amanus = AmanusTotal;
        EstadoDoJogador = GameStates.CharacterState.Playing;
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        obj = this.gameObject;
    }
	void Start () {
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
            Destroy(gameObject);
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
    }
    public override void Movement()
    {
        if (moveX < 0 && !left)
        {
            Flip();
        }
        else if ((moveX > 0) && left)
        {
            Flip();
        }

        if (moveZ == 0 && moveX == 0)
            anim.SetTrigger("Idle");

        if (moveZ < 0 && Mathf.Abs(moveZ) > Mathf.Abs(moveX))
        {
            anim.SetTrigger("Down");
            visao = TargetVision.Front;
        }
        if ((moveX < 0 || moveX > 0) && (Mathf.Abs(moveZ) < Mathf.Abs(moveX) || Mathf.Abs(moveZ) == Mathf.Abs(moveX)))
        {
            anim.SetTrigger("Right");
            if (moveX < 0)
            {
                visao = TargetVision.Left;
            }
            else
            {
                visao = TargetVision.Right;
            }
        }
        if (moveZ > 0 && Mathf.Abs(moveZ) > Mathf.Abs(moveX))
        {
            anim.SetTrigger("Up");
            visao = TargetVision.Back;
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
            rgd.AddForce(Vector3.up * moveY, ForceMode.Impulse);
            notJump = true;
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
            anim.SetTrigger("Attack");
            attack = true;
            StartCoroutine(Wait());
            if(Alvo != null)
            {
                Alvo.HealOrDamage(20,0);
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
        }
        if (col.gameObject.tag == "Monsters")
        {

            Alvo = col.gameObject.GetComponent<Target>();

        }

    }
}
