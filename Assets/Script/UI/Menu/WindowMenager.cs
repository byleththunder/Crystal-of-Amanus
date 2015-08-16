using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WindowMenager : MonoBehaviour
{
    public GameObject[] Janelas;
    public GameObject[] Botoes;
    public Animator Anim;
    MonoBehaviour[] JanelaScript;
    bool start = false;
    int Indice = 0;
    int IndiceAnterior = -1;
    int IndiceJanela = 0;
    Character pers;
    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        pers = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        Anim.enabled = false;
        if (JanelaScript == null)
        {
            JanelaScript = new MonoBehaviour[Janelas.Length];
            for (int i = 0; i < Janelas.Length; i++)
            {
                JanelaScript[i] = Janelas[i].GetComponent<MonoBehaviour>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if(!start)
            {
                pers.EstadoDoJogador = GameStates.CharacterState.DontMove;
                if(Anim.enabled)
                {
                    Anim.SetTrigger("StartMenu");
                }else
                {
                    Anim.enabled = true;
                }
                start = !start;
                if (!JanelaScript[0].enabled)
                {
                    JanelaScript[0].enabled = true;
                }
                
            }else
            {
                pers.EstadoDoJogador = GameStates.CharacterState.Playing;
                Time.timeScale = 1f;
                Anim.SetTrigger("DisableMenu");
                
                start = !start;
            }
        }
        if(start)
        {
            if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Show") && !Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                Time.timeScale = 0.00000000000001f;
            }
            if (!Janelas[IndiceJanela].activeInHierarchy)
            {
                Janelas[IndiceJanela].SetActive(true);
            }
            MovInMenu();
            
        }else
        {
            if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Gone"))
            {
               
                foreach (GameObject m in Janelas)
                {
                    
                        m.SetActive(false);
                    
                }
                IndiceJanela = 0;
            }
        }
    }
    void MovInMenu()
    {
        if (IndiceJanela == 0)
        {
            Image _Cor = Botoes[Indice].GetComponent<Image>();
            _Cor.color = Color.green;
            if (IndiceAnterior > -1 && Indice != IndiceAnterior)
            {
                Image _CorAnt = Botoes[IndiceAnterior].GetComponent<Image>();
                _CorAnt.color = Color.red;
            }
            
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                IndiceAnterior = Indice;
                if (Indice == 0)
                {
                    Indice = Botoes.Length;
                }
                Indice--;

            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                IndiceAnterior = Indice;
                Indice = (Indice + 1) % Botoes.Length;

            }
            if (Input.GetButtonDown("Action"))
            {
                Janelas[IndiceJanela].SetActive(false);
                if (Botoes[Indice].name == "Bt_Item")
                {
                    
                    IndiceJanela = 1;
                    Janelas[IndiceJanela].GetComponent<ItenWindow>().Att();

                }
                else if (Botoes[Indice].name == "Bt_Info")
                {
                    IndiceJanela = 2;
                }
            }
        }
        else
        {
            if (Input.GetButtonDown("Defense"))
            {
                Janelas[IndiceJanela].SetActive(false);
                IndiceJanela = 0;
            }
        }
    }
}
