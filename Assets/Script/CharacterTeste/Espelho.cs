using UnityEngine;
using System.Collections;

public class Espelho : MonoBehaviour
{
    public float[] Angulos;
    int indice = 0;
    
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Rotacionar()
    {
        transform.eulerAngles = new Vector3(0, Angulos[indice], 0);
        indice = (indice + 1) % Angulos.Length;
        print(indice.ToString());
    }
    void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Player")
        {
            
            if (Input.GetButtonDown("Action"))
            {
                collisionInfo.gameObject.transform.localPosition += transform.forward;
                Rotacionar();
            }
        }
    }

}
