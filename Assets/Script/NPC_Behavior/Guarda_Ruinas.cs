using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Scripts/NPC Scripts/Guarda Real - Ruinas")]
public class Guarda_Ruinas : MonoBehaviour
{

    public List<Vector3> MoveTo = new List<Vector3>();
    public int indice = 0;
    public bool IsMoving = false;
    private bool moveX;
    public Rigidbody rgb;
    public Animator anim;
    Vector3 velocidades;
    float vel = 2;
    // Use this for initialization
    void Start()
    {
        rgb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        for (int i = 0; i < MoveTo.Count; i++)
        {
            MoveTo[i] += transform.localPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Vel", rgb.velocity.z);
        anim.SetFloat("VelX", rgb.velocity.x);
        if (IsMoving)
        {
            if (indice < MoveTo.Count)
            {
                
                IsMoving = Moving(MoveTo[indice]);
            }
        }
        
    }
    
    bool Moving(Vector3 location)
    {
        bool mover = false;
        rgb.velocity = velocidades;
        
        #region X
        if (location.x > transform.localPosition.x + 0.1f)
        {

            mover = true;
            moveX = true;
            anim.SetTrigger("Right");
            velocidades.x = vel;
        }
        else if (location.x < transform.localPosition.x - 0.1f)
        {
            mover = true;
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
        if (location.z > transform.localPosition.z + 0.1f)
        {
            mover = true;
            if (!moveX)
            {
                anim.SetTrigger("Up");
            }
            velocidades.z = vel;
        }
        else if (location.z < transform.localPosition.z - 0.1f)
        {
            mover = true;
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
        if (!mover)
        {
            indice++;
            return false;
        }
        
        return true;
    }
    void OnDrawGizmos()
    {
        var scale = 1.0f;
        for (int i = 0; i < MoveTo.Count; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position + MoveTo[i], transform.position + MoveTo[i] + transform.forward * scale);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position + MoveTo[i], transform.position + MoveTo[i] + transform.right * scale);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position + MoveTo[i], transform.position + MoveTo[i] + Vector3.up * scale);

            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(MoveTo[i], 0.125f);
        }
    }
}
