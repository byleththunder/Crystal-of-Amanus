using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/MapaMundi/Pivot")]
public class CharacterPivot : MonoBehaviour
{
    public Animator anim;
    public Vector3 Destino;
    Vector3 velocidades;
    float vel = 3.5f;
    bool moveX = false;
    bool moving = false;
    [SerializeField]
    private NavMeshAgent Navegador;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Navegador.transform.position;
        anim.SetFloat("Speed", Mathf.Abs(velocidades.z));
        anim.SetFloat("SpeedX", Mathf.Abs(velocidades.x));
        if (Navegador.velocity != Vector3.zero)
        {
            moving = true;
            Move();
        }
        if (Navegador.velocity == Vector3.zero)
        {
            moving = false;
            anim.SetTrigger("Down");
        }
    }
    void Move()
    {
        
        #region X
        if (Navegador.transform.eulerAngles.y > 0 && Navegador.transform.eulerAngles.y<=90)
        {
            anim.ResetTrigger("Up");
            anim.ResetTrigger("Down");
            anim.ResetTrigger("Left");
            anim.SetTrigger("Right");
            velocidades.x = vel;
        }
        else if (Navegador.transform.eulerAngles.y > 180 && Navegador.transform.eulerAngles.y <= 270)
        {
            moveX = true;
            anim.ResetTrigger("Up");
            anim.ResetTrigger("Down");
            anim.ResetTrigger("Right");
            anim.SetTrigger("Left");
            velocidades.x = -vel;
        }
        else
        {
            moveX = false;
            velocidades.x = 0;
        }
        #endregion
        #region Z
        if ( (Navegador.transform.eulerAngles.y > 270 && Navegador.transform.eulerAngles.y <= 360) || Navegador.transform.eulerAngles.y ==0)
        {
            if (!moveX)
            {
                anim.ResetTrigger("Right");
                anim.ResetTrigger("Down");
                anim.ResetTrigger("Left");
                anim.SetTrigger("Up");
            }
            velocidades.z = vel;
        }
        else if (Navegador.transform.eulerAngles.y > 90 && Navegador.transform.eulerAngles.y <= 180)
        {
            if (!moveX)
            {
                anim.ResetTrigger("Up");
                anim.ResetTrigger("Right");
                anim.ResetTrigger("Left");
                anim.SetTrigger("Down");
            }
            velocidades.z = -vel;
        }
        else
        {
            velocidades.z = 0;
        }
        #endregion
    }
    public bool IsMoving()
    {
        return moving;
    }
    public void ChangeVel(float indice)
    {
        vel = indice;
    }
}
