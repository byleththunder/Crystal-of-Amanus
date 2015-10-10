using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TeamUtility.IO;

[AddComponentMenu("Scripts/MessageBox/MessageScript")]
public class MessageBox : Singleton<MessageBox>
{
    protected MessageBox() { }
    //Texto
    public Text Texto;
    public Text Nome;
    //Painel aonde o texto vai ficar
    public GameObject painel;
    //Variaveis e propriedades
    public bool IsFinishAll { get { return FinishiAll; } }
    //Armazena todas as mensagens requisitadas
    List<string> mensagens = new List<string>();
    List<string> Nomes = new List<string>();
    //Finish diz que acabou
    bool Finish = false;
    //FinishAll diz se todos os textos já foram impressos
    bool FinishiAll = false;
    //O indice representa a letra que está sendo impressa.
    int indice = 0;
    //O timer faz com que as letras sejam impressas em um determinado tempo.
    float Timer = 0;
    //Awake - Vai encontrar o canvas e fazer a Messagebox ser filha dele.
    Character pers;
    void Awake()
    {
        GameObject canvas = GameObject.Find("Canvas");
        gameObject.transform.SetParent(canvas.transform, false);
    }
    //Armazena a mensagem
    public void WriteMessage(string text)
    {
        try { pers = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>(); }
        catch { Debug.LogWarning("Não tem nenhum player nessa cena."); }
        FinishiAll = false;
        if (mensagens.Count == 0) { Finish = false; indice = -1; }
        if (!painel)
        {
            painel = gameObject;
            var temp = GetComponentsInChildren<Text>();
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i].name == "Texto")
                {
                    Texto = temp[i];
                }
                if (temp[i].name == "Nome")
                {
                    Nome = temp[i];
                }
            }
            Nome.text = string.Empty;
            Texto.text = string.Empty;
        }
        int NumLinhas = ((int)(Texto.GetComponent<RectTransform>().sizeDelta.y / Texto.fontSize)) - 1;
        int PalaporLinha = ((int)(Texto.GetComponent<RectTransform>().sizeDelta.x / Texto.fontSize));
        int _indicetemp = 0;
        mensagens.Add(string.Empty);
        Nomes.Add(string.Empty);
        for (int i = 0; i < text.Length; i++)
        {
            if (i > (NumLinhas * PalaporLinha) * (_indicetemp + 1) && text[i] == ' ')
            {
                _indicetemp++;
                mensagens.Add(string.Empty);
                Nomes.Add(string.Empty);
                i++;
            }
            mensagens[mensagens.Count - 1] += text[i];
        }




        Show();
    }
    public void WriteMessage(string text, string name)
    {
        try { pers = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>(); }
        catch { Debug.LogWarning("Não tem nenhum player nessa cena."); }

        FinishiAll = false;
        if (mensagens.Count == 0) { Finish = false; indice = 0; }
        if (!painel)
        {
            painel = gameObject;
            var temp = GetComponentsInChildren<Text>();
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i].name == "Texto")
                {
                    Texto = temp[i];
                }
                if (temp[i].name == "Nome")
                {
                    Nome = temp[i];
                }
            }
            Nome.text = string.Empty;
            Texto.text = string.Empty;
        }
        float NumLinhas = ((int)(Texto.GetComponent<RectTransform>().sizeDelta.y / Texto.fontSize));
        float PalaporLinha = ((int)(Texto.GetComponent<RectTransform>().sizeDelta.x / Texto.fontSize));
        int _indicetemp = 0;
        int paltotal = (int)(NumLinhas * PalaporLinha);
        if (paltotal == 0)
        {
            paltotal *= 5;
        }
        print(paltotal);
        mensagens.Add(string.Empty);
        Nomes.Add(name);
        for (int i = 0; i < text.Length; i++)
        {
            if (i > paltotal * (_indicetemp + 1) && text[i] == ' ')
            {
                _indicetemp++;
                mensagens.Add(string.Empty);
                Nomes.Add(name);
                i++;
            }
            mensagens[mensagens.Count - 1] += text[i];
        }



        Show();
    }
    void Update()
    {
        if (Finish)
        {
            if (mensagens.Count > 0)
            {
                if (InputManager.GetButtonDown("Action"))
                {
                    indice = 0;
                    Texto.text = string.Empty;
                    mensagens.RemoveAt(0);
                    Nomes.RemoveAt(0);
                    Finish = false;
                    Timer = 0;
                    Show();
                }
            }
            else
            {
                painel.SetActive(false);
            }
        }
        else
        {
            if (mensagens.Count > 0)
            {
                Show();
            }
        }
        if (mensagens.Count == 0)
        {
            FinishiAll = true;
            if (pers)
                if (pers.EstadoDoJogador == GameStates.CharacterState.DontMove) { pers.EstadoDoJogador = GameStates.CharacterState.Playing; }

        }
        if (FinishiAll && painel.activeInHierarchy)
        {
            painel.SetActive(false);
        }

    }
    //Escreve a mensagem na tela.
    void Show()
    {
        if (pers)
            if (pers.EstadoDoJogador == GameStates.CharacterState.Playing) { pers.EstadoDoJogador = GameStates.CharacterState.DontMove; }

        if (mensagens.Count > 0)
        {
            if (indice < mensagens[0].Length)
            {

                Timer += (Time.deltaTime*2);
                if (InputManager.GetButtonDown("Action") && !Finish && indice > 0)
                {
                    if (Nomes[0] == string.Empty)
                    {
                        Nome.text = string.Empty;
                        Texto.text = mensagens[0];
                    }
                    else
                    {
                        Nome.text = Nomes[0];
                        Texto.text = mensagens[0];
                    }
                    Finish = true;
                    Timer = 0;
                    return;
                }
                if (Timer > 0.05f)
                {
                    if (indice == 0)
                    {
                        if (Nomes[0] == string.Empty)
                        {
                            Nome.text = string.Empty;
                            Texto.text = string.Empty;
                        }
                        else
                        {
                            Nome.text = Nomes[0];
                            Texto.text = string.Empty;
                        }
                    }
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
