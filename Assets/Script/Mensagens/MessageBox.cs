using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MessageBox : Singleton<MessageBox>
{
    protected MessageBox() { }
    //Texto
    public Text Texto;
    //Painel aonde o texto vai ficar
    public GameObject painel;
    //Variaveis e propriedades
    public bool IsFinishAll { get { return FinishiAll; } }
    //Armazena todas as mensagens requisitadas
    List<string> mensagens = new List<string>();
    //Finish diz que acabou
    bool Finish = false;
    //FinishAll diz se todos os textos já foram impressos
    bool FinishiAll = false;
    //O indice representa a letra que está sendo impressa.
    int indice = 0;
    //O timer faz com que as letras sejam impressas em um determinado tempo.
    float Timer = 0;
    //Awake - Vai encontrar o canvas e fazer a Messagebox ser filha dele.
    Camera cam;
    Character pers;
    void Awake()
    {
        GameObject canvas = GameObject.Find("Canvas");
        gameObject.transform.SetParent(canvas.transform, false);
    }
    //Armazena a mensagem
    public void WriteMessage(string text)
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        pers = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        FinishiAll = false;
        if (mensagens.Count == 0) { Finish = false; }
        mensagens.Add(text);
        if (!painel)
        {
            painel = gameObject;
            Texto = painel.GetComponentInChildren<Text>();
            Texto.text = string.Empty;
        }
        Show();
    }
    void Update()
    {
        if (Finish)
        {
            if (mensagens.Count > 0)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    print("oi");
                    indice = 0;
                    Texto.text = string.Empty;
                    mensagens.RemoveAt(0);
                    Finish = false;
                    Timer = 0;
                    Show();
                }
            }
            else
            {
                painel.SetActive(false);
            }
        }else
        {
            if(mensagens.Count >0)
            {
                Show();
            }
        }
        if(mensagens.Count == 0)
        {
            FinishiAll = true;
            if (pers.EstadoDoJogador == GameStates.CharacterState.DontMove) { pers.EstadoDoJogador = GameStates.CharacterState.Playing; }
        }

    }
    //Escreve a mensagem na tela.
    void Show()
    {
        if (pers.EstadoDoJogador == GameStates.CharacterState.Playing) { pers.EstadoDoJogador = GameStates.CharacterState.DontMove; }
        int NumLinhas = ((int)(painel.GetComponent<RectTransform>().sizeDelta.y / Texto.fontSize)) - 1;
        if (mensagens.Count > 0)
        {
            if (indice < mensagens[0].Length)
            {

                Timer += (Time.deltaTime);
                if (Input.GetKeyDown(KeyCode.A) && !Finish && indice > 0)
                {
                    Texto.text = mensagens[0];
                    Finish = true;
                    Timer = 0;
                    return;
                }
                if (Timer > 0.05f)
                {
                    Texto.text += mensagens[0][indice];
                    indice++;
                    Timer = 0;
                    if (indice >= mensagens[0].Length)
                    {
                        Timer = 0;
                        Finish = true;
                        return;
                    }
                }

            }

        }
        else
        {
            Finish = true;
        }

    }
}
