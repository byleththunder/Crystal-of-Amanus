using UnityEngine;
using System.Collections;

public class Vendedor : MonoBehaviour
{

    public GameObject Loja;
    bool ligar = false;
    Character per;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (per)
        {
            if(Loja.activeInHierarchy)
            {
                per.EstadoDoJogador = GameStates.CharacterState.DontMove;
            }else
            {
                per.EstadoDoJogador = GameStates.CharacterState.Playing;
            }
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!per)
            {
                per = col.gameObject.GetComponent<Character>();
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (Loja)
                {
                    print("Abrir");
                    ligar = !ligar;
                    Loja.SetActive(ligar);
                }
            }
        }
    }
}
