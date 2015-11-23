using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
[AddComponentMenu("Scripts/Loja/Vendedor")]
public class Vendedor : MonoBehaviour
{

    public Canvas Loja;
    bool ligar = false;
    Character per;
    public EventSystem evento;
    [Header("Primeiro botão a ser selecionado")]
    public GameObject Botao;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (per)
        {
            if(Loja.enabled)
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
            if (GameInput.GetKeyDown(InputsName.Action))
            {
                if (Loja)
                {
                    if (!Loja.enabled)
                    {
                        Loja.enabled = true;
                        evento.SetSelectedGameObject(Botao);
                    }
                }
            }
        }
    }
}
