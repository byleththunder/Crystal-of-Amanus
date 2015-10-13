using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Personagens e Monstros/Monstros Sripts/Monster/Morcego")]
public class Morcego : Monster
{
    enum MorcegoBehavior { Idle, Rasante, Cuspe };
    public Rigidbody rdb;
    float ramdom;
    bool Horizontal = true;
    Vector3 PosIni;
    public float distance = 10f;
    MorcegoBehavior Behavior = MorcegoBehavior.Idle;
    public Vector3 AfterAtk, TargetPos;
    bool cooldown = false;
    Ray raio;
    Vector3 Velocidade;
    //Razante
    bool atk = false;
    bool Check = false;
    bool CheckY = false;
    bool XZ = false;
    float vel = 0;
    //Cuspe
    public ParticleSystem poison;
    // Use this for initialization
    void Start()
    {
        Level = 2;
        UpdateStatus();
        ExpEarn = 4;
        poison = GetComponentInChildren<ParticleSystem>();
        anim = GetComponentInChildren<Animator>();
        PosIni = transform.position;
        rdb = GetComponent<Rigidbody>();
        ramdom = Random.Range(0, 91);
        Horizontal = (Random.Range(0, 2) == 1 ? false : true);
        if (Horizontal)
        {
            visao = TargetVision.Left;
            anim.SetTrigger("Left");
            Velocidade.x = 1;
        }
        else
        {
            visao = TargetVision.Back;
            anim.SetTrigger("Up");
            Velocidade.z = 1;
        }
    }

    void Update()
    {
        DamageCheck();
        rdb.velocity = Velocidade;
        raio = new Ray(transform.position, ConvertVisao(visao) +  new Vector3(0,-0.5f,0));
        RaycastHit hit;
        if (Physics.Raycast(raio, out hit, 2))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                if (!cooldown)
                {
                    Debug.Log("Detectou");
                    var aleatorio = Random.Range(1, 3);
                    switch (aleatorio)
                    {
                        case 1:
                            Behavior = MorcegoBehavior.Cuspe;
                            TargetPos = hit.transform.position;
                            break;
                        case 2:
                            Behavior = MorcegoBehavior.Rasante;
                            AfterAtk = transform.position;
                            TargetPos = hit.transform.position;
                            atk = true;
                            break;
                    }
                    
                    Invoke("Cooldown", 10f);
                    cooldown = true;
                }
            }
            Debug.DrawLine(raio.origin, hit.point);
        }

        if (Behavior == MorcegoBehavior.Idle)
        {
            Idle();
            HitTeste = false;
        }
        else if (Behavior == MorcegoBehavior.Rasante)
        {
            Razante();
        }
        else if (Behavior == MorcegoBehavior.Cuspe)
        {
            Cuspe();
        }
    }
    

    public void Razante()
    {
        //Debug.Log("Sem 2 ação...");
        if (atk)
        {
            #region X
            if (TargetPos.x > transform.position.x + 0.1f)
            {
                XZ = false;
                Velocidade.x = 2;
            }
            else if (TargetPos.x < transform.position.x - 0.1f)
            {
                XZ = false;
                Velocidade.x = -2;
            }
            else
            {
                if(!XZ)
                {
                    Check = false;
                }
                Velocidade.x = 0;
            }
            #endregion
            #region Z
            if (TargetPos.z > transform.position.z + 0.1f)
            {
                XZ = true;
                Velocidade.z = 2;
            }
            else if (TargetPos.z < transform.position.z - 0.1f)
            {
                XZ = true;
                Velocidade.z = -2;
            }
            else
            {
                if(XZ)
                {
                    Check = true;
                }
                Velocidade.z = 0;
            }
            #endregion
            #region Y
            if (TargetPos.y > transform.position.y + 0.1f)
            {
                Velocidade.y = 1;
            }
            else if (TargetPos.y < transform.position.y - 0.1f)
            {
                Velocidade.y = -1;
            }
            else
            {
                CheckY = true;
                Velocidade.y = 0;
            }
            #endregion
            if(Check && CheckY || HitTeste)
            {
                Check = false;
                CheckY = false;
                atk = false;
            }
        }
        else
        {
            TargetPos = Vector3.zero;
            #region X
            if (AfterAtk.x > transform.position.x + 0.1f)
            {
                XZ = false;
                Velocidade.x = 2;
            }
            else if (AfterAtk.x < transform.position.x - 0.1f)
            {
                XZ = false;
                Velocidade.x = -2;
            }
            else
            {
                if (!XZ)
                {
                    Check = true;
                }
                Velocidade.x = 0;
            }
            #endregion
            #region Z
            if (AfterAtk.z > transform.position.z + 0.1f)
            {
                XZ = true;
                Velocidade.z = 2;
            }
            else if (AfterAtk.z < transform.position.z - 0.1f)
            {
                XZ = true;
                Velocidade.z = -2;
            }
            else
            {
                if(XZ)
                {
                    Check = true;
                }
                Velocidade.z = 0;
            }
            #endregion
            #region Y
            if (AfterAtk.y > transform.position.y + 0.1f)
            {
                Velocidade.y = 2;
            }
            else if (AfterAtk.y < transform.position.y - 0.1f)
            {
                Velocidade.y = -2;
            }
            else
            {
                CheckY = true;
                Velocidade.y = 0;
            }
            #endregion
            if (Check && CheckY)
            {
                Debug.Log("voltou");
                Check = false;
                CheckY = false;
                
                Behavior = MorcegoBehavior.Idle;
                if(Horizontal)
                {
                    Velocidade.x = vel;
                }else
                {
                    Velocidade.z = vel;
                }
            }
            
        }
    }
    public void Cuspe()
    {
        
        float angulo = Mathf.Atan2(transform.position.y,TargetPos.y) * Mathf.Rad2Deg;
        float angulx = Vector3.Angle(transform.position, TargetPos);
        poison.transform.localRotation = Quaternion.Euler(ConvertVisaoToAngle(visao)+ new Vector3(angulo,angulx,0));
        poison.transform.localPosition = ConvertVisao(visao)/2;
        poison.Emit(1);
        Behavior = MorcegoBehavior.Idle;
    }
    public void Idle()
    {
        float sin = Mathf.Sin((Time.time + ramdom));
        rdb.AddRelativeForce(Vector3.up * sin);
        Velocidade.y = rdb.velocity.y;
        if (Horizontal)
        {
            vel = Velocidade.x;
            if (transform.position.x > (PosIni.x + distance))
            {
                
                visao = TargetVision.Left;
                #region ResetTrigers
                anim.ResetTrigger("Up");
                anim.ResetTrigger("Right");
                anim.ResetTrigger("Down");
                #endregion
                anim.SetTrigger("Left");
                Velocidade.x = -1;
                return;
            }
            if ((PosIni.x - distance) > transform.position.x)
            {
                visao = TargetVision.Right;
                #region ResetTrigers
                anim.ResetTrigger("Up");
                anim.ResetTrigger("Down");
                anim.ResetTrigger("Left");
                #endregion
                anim.SetTrigger("Right");
                Velocidade.x = 1;
                return;
            }
        }
        else
        {
            vel = Velocidade.z;
            if (transform.position.z > (PosIni.z + distance))
            {
                visao = TargetVision.Front;
                #region ResetTrigers
                anim.ResetTrigger("Up");
                anim.ResetTrigger("Right");
                anim.ResetTrigger("Left");
                #endregion
                anim.SetTrigger("Down");
                Velocidade.z = -1;
                return;
            }
            if ((PosIni.z - distance) > transform.position.z)
            {
                visao = TargetVision.Back;
                #region ResetTrigers
                anim.ResetTrigger("Down");
                anim.ResetTrigger("Right");
                anim.ResetTrigger("Left");
                #endregion
                anim.SetTrigger("Up");
                Velocidade.z = 1;
                return;
            }
        }
    }
    void Cooldown()
    {
        
        cooldown = false;
    }
    Vector3 ConvertVisao(TargetVision v)
    {
        Vector3 _temp = Vector3.zero;
        switch (v)
        {
            case TargetVision.Back:
                _temp = Vector3.forward;
                break;
            case TargetVision.Front:
                _temp = Vector3.back;
                break;
            case TargetVision.Left:
                _temp = Vector3.left;
                break;
            case TargetVision.Right:
                _temp = Vector3.right;
                break;
            default:
                Debug.LogError("Valor Invalidao: Morcego - ConvertVisao");
                break;
        }

        return _temp;
    }
    Vector3 ConvertVisaoToAngle(TargetVision v)
    {
        Vector3 _temp = Vector3.zero;
        switch (v)
        {
            case TargetVision.Back:
                _temp = new Vector3(0, 0, 0);
                break;
            case TargetVision.Front:
                _temp = new Vector3(0, 180, 0);
                break;
            case TargetVision.Left:
                _temp = new Vector3(0, 270, 0);
                break;
            case TargetVision.Right:
                _temp = new Vector3(0, 90, 0);
                break;
            default:
                Debug.LogError("Valor Invalidao: Morcego - ConvertVisao");
                break;
        }

        return _temp;
    }
    void OnCollisionEnter(Collision col)
    {
        print(col.transform.name);
        if(col.transform.tag == "Reflect")
        {
            HitTeste = true;
            print("Escudo");
            return; 
        }
        if(col.gameObject.tag == "Player")
        {
            if(atk)
            {
                col.gameObject.GetComponent<Target>().HealOrDamage(Ataque, 0);
                atk = false;
            }
        }
    }
}
