using UnityEngine;
using System.Collections;

public class RuinasTestes : MonoBehaviour
{
    Animator anim;
    bool firstEvent = true;
    bool secondEvent = false;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
       MessageBox.PrefabPath = "CaixaDeTexto";
        
    }

    // Update is called once per frame
    void Update()
    {
        if (firstEvent)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("RuinasShowOff"))
            {
                anim.enabled = false;
                GetComponent<CameraRendering>().enabled = true;
                GetComponent<EndGame>().enabled = true;
                MessageBox.Instance.WriteMessage("Olá. Aperte A para passar o texto.");
                MessageBox.Instance.WriteMessage("Essa cena das ruinas ainda não está completa, mas você é capaz de explorar o mapa.");
                GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().EstadoDoJogador = GameStates.CharacterState.Playing;
                firstEvent = false;
            }else
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().EstadoDoJogador = GameStates.CharacterState.DontMove;
                if(Input.GetKeyDown(KeyCode.A))
                {
                    anim.enabled = false;
                    GetComponent<CameraRendering>().enabled = true;
                    GetComponent<EndGame>().enabled = true;
                    MessageBox.Instance.WriteMessage("Olá. Aperte A para passar o texto.");
                    MessageBox.Instance.WriteMessage("Essa cena das ruinas ainda não está completa, mas você é capaz de explorar o mapa.");
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().EstadoDoJogador = GameStates.CharacterState.Playing;
                    firstEvent = false;
                }
            }
        }
        if (!secondEvent  && !firstEvent)
        {
            if (MessageBox.Instance.IsFinishAll)
            {
                Invoke("SegundoEvento", 2f);
                secondEvent = true;
            }
        }
    }
    void SegundoEvento()
    {
        MessageBox.Instance.WriteMessage("Ah...desculpe, já ia esquecendo de te avisar, mas se você apertar o F1, você consegue abrir uma janela que mostra os comandos desse alpha.");
    }
    
}
