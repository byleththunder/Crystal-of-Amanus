using UnityEngine;
using System.Collections;

public class RollingRock : MonoBehaviour
{
    public enum Eixo { MaisX, MenosX, MaisZ, MenosZ };
    public bool DestroyAtEnd = false;
    public Rigidbody rgd;
    public Transform Rocha;
    public Vector3[] Distancias;
    public Eixo[] Sentidos;
    public float MaxSpeed = 10;
    Vector3 speed = Vector3.zero;
     int indice = 0;
     Vector3 Destiny;
     float multiply = 0;
     public bool Lento = false;
    // Use this for initialization
    void Start()
    {
        Destiny = Rocha.position + Distancias[0];
        
    }

    // Update is called once per frame
    void Update() 
    {
        rgd.velocity = new Vector3(speed.x, rgd.velocity.y, speed.z);
        if(Lento)
        {
            multiply = Mathf.Lerp(multiply, 2, Time.deltaTime / 50);
        }else
        {
            multiply = Mathf.Lerp(multiply, 2, Time.deltaTime / 10);
        }
        
        if(Sentidos[indice].Equals(Eixo.MaisX))
        {
            if (Rocha.position.x < Destiny.x)
            {
                speed.x = Mathf.Lerp(speed.x, MaxSpeed, Time.deltaTime * multiply);
            }
            else
            {
                speed.x = 0;
                multiply = multiply/2;
                rgd.velocity = new Vector3(speed.x, rgd.velocity.y, speed.z);
                if(indice +1 < Distancias.Length)
                {
                    indice++;
                    Destiny += Distancias[indice];
                }
                else
                {
                    if(DestroyAtEnd)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }else if(Sentidos[indice].Equals(Eixo.MenosX))
        {
            if (Rocha.position.x > Destiny.x)
            {
                speed.x = Mathf.Lerp(speed.x, -MaxSpeed, Time.deltaTime * multiply);
            }
            else
            {
                speed.x = 0;
                multiply = multiply/2;
                rgd.velocity = new Vector3(speed.x, rgd.velocity.y, speed.z);
                if (indice + 1 < Distancias.Length)
                {
                    indice++;
                    Destiny += Distancias[indice];
                }
                else
                {
                    if (DestroyAtEnd)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }else if(Sentidos[indice].Equals(Eixo.MaisZ))
        {
            if (Rocha.position.z < Destiny.z)
            {
                speed.z = Mathf.Lerp(speed.z, MaxSpeed, Time.deltaTime * multiply);
            }
            else
            {
                speed.z = 0;
                multiply = multiply / 2;
                rgd.velocity = new Vector3(speed.x, rgd.velocity.y, speed.z);
                if (indice + 1 < Distancias.Length)
                {
                    indice++;
                    Destiny += Distancias[indice];
                }
                else
                {
                    if (DestroyAtEnd)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }else // menos z
        {
            if (Rocha.position.z > Destiny.z)
            {
                speed.z = Mathf.Lerp(speed.z, -MaxSpeed, Time.deltaTime * multiply);
            }
            else
            {
                speed.z = 0;
                multiply = multiply / 2;
                rgd.velocity = new Vector3(speed.x, rgd.velocity.y, speed.z);
                if (indice + 1 < Distancias.Length)
                {
                    indice++;
                    Destiny += Distancias[indice];
                }
                else
                {
                    if (DestroyAtEnd)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
        
	}

    void OnDrawGizmos()
    {
        if(Distancias != null)
        {
            if (Distancias.Length > 0)
            {
                Vector3 _Destiny = Vector3.zero;
                for (int i = 0; i < Distancias.Length; i++)
                {
                    if (i.Equals(0))
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawLine(transform.position, transform.position + new Vector3(Distancias[i].x, 0, 0));
                        Gizmos.color = Color.green;
                        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, Distancias[i].y, 0));
                        Gizmos.color = Color.blue;
                        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, 0, Distancias[i].z));
                        Gizmos.color = Color.yellow;
                        Gizmos.DrawSphere(transform.position, 0.5f);
                        _Destiny = transform.position + Distancias[i];
                    }
                    else
                    {


                        Gizmos.color = Color.red;
                        Gizmos.DrawLine(_Destiny, _Destiny + new Vector3(Distancias[i].x, 0, 0));
                        Gizmos.color = Color.green;
                        Gizmos.DrawLine(_Destiny, _Destiny + new Vector3(0, Distancias[i].y, 0));
                        Gizmos.color = Color.blue;
                        Gizmos.DrawLine(_Destiny, _Destiny + new Vector3(0, 0, Distancias[i].z));
                        Gizmos.color = Color.yellow;
                        Gizmos.DrawSphere(_Destiny, 0.5f);
                        _Destiny += Distancias[i];
                    }

                }
            }
        }
        
    }

    
}
