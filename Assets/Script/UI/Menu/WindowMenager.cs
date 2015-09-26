using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TeamUtility.IO;

public class WindowMenager : MonoBehaviour
{
    public GameObject[] Janelas;
    public GameObject[] Botoes;
    MonoBehaviour[] JanelaScript;
    bool start = false;
    int Indice = 0;
    int IndiceAnterior = -1;
    int IndiceJanela = 0;
    Character pers;
    public GameObject JanelaPrincipal;
    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        pers = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
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
            if (!start)
            {
                pers.EstadoDoJogador = GameStates.CharacterState.DontMove;
                JanelaPrincipal.SetActive(true);
                start = !start;
                if (!JanelaScript[0].enabled)
                {
                    JanelaScript[0].enabled = true;
                }

            }
            else
            {
                pers.EstadoDoJogador = GameStates.CharacterState.Playing;
                Time.timeScale = 1f;
                JanelaPrincipal.SetActive(false);
                start = !start;
            }
        }
        if (start)
        {
            Time.timeScale = 0.0f;

            if (!Janelas[IndiceJanela].activeInHierarchy)
            {
                Janelas[IndiceJanela].SetActive(true);
            }
            MovInMenu();

        }
        else
        {
            foreach (GameObject m in Janelas)
            {
                m.SetActive(false);
            }
            IndiceJanela = 0;
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

            if (InputManager.GetButtonDown("Vertical") && InputManager.GetAxisRaw("Vertical") < 0)
            {
                IndiceAnterior = Indice;
                if (Indice == 0)
                {
                    Indice = Botoes.Length;
                }
                Indice--;

            }
            if (InputManager.GetButtonDown("Vertical") && InputManager.GetAxisRaw("Vertical") > 0)
            {
                IndiceAnterior = Indice;
                Indice = (Indice + 1) % Botoes.Length;

            }
            if (InputManager.GetButtonDown("Action"))
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
            if (InputManager.GetButtonDown("Magic"))
            {
                Janelas[IndiceJanela].SetActive(false);
                IndiceJanela = 0;
            }
        }
    }
}
