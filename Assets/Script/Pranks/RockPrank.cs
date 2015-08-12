using UnityEngine;
using System.Collections;

public class RockPrank : MonoBehaviour
{
    public GameObject Rocha, Prancha, Barreira;
    bool Ativado = false;
    int rotacionar = 1;
    bool iniciado = false;
    // Use this for initialization
    void Start()
    {
        Barreira.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Ativado == true)
        {
            if (iniciado == false)
            {
                if (rotacionar < 45)
                {
                    Prancha.transform.eulerAngles += new Vector3(-45, 0, 0);
                    rotacionar=45;
                    Mathf.Clamp(rotacionar, 0, 45);
                }
                else
                {
                    Rocha.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -15), ForceMode.VelocityChange);
                    iniciado = true;
                }
            }
            else
            {
                if(Rocha.transform.position.z < Barreira.transform.position.z - 5)
                {
                    Barreira.SetActive(true);
                    this.enabled = false;
                    
                }
            }

        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Ativado = true;
        }
    }
}
