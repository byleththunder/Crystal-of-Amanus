using UnityEngine;
using System.Collections;

public class CharacterPivot : MonoBehaviour
{
    public Animator anim;
    public Vector3 Destino;
    public Rigidbody rgb;
    Vector3 velocidades;
    float vel = 2;
    bool moveX = false;
    bool moving = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(rgb.velocity.z));
        anim.SetFloat("SpeedX",  Mathf.Abs(rgb.velocity.x));
        if (Destino != transform.position && Destino != Vector3.zero)
        {
            moving = true;
            Move();
        }
        if(rgb.velocity == Vector3.zero)
        {
            moving = false;
            anim.SetTrigger("Down");
        }
    }
    void Move()
    {
        rgb.velocity = velocidades;
        #region X
        if (Destino.x > transform.position.x+0.1f)
        {
            moveX = true;
            anim.SetTrigger("Right");
            velocidades.x = vel;
        }
        else if (Destino.x < transform.position.x-0.1f)
        {
            moveX = true;
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
        if (Destino.z > transform.position.z+0.1f)
        {
            if (!moveX)
            {
                anim.SetTrigger("Up");
            }
            velocidades.z = vel;
        }
        else if (Destino.z < transform.position.z-0.1f)
        {
            if (!moveX)
            {
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
