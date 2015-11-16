using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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
    //Prefab do Canvas aonde o message box vai ficar. Estou criando outro canvas para não ter perigo de destruir ele no load.
    
    void Awake()
    {
        if (GameObject.Find("Canva (Singletone)") == null)
        {
            GameObject Canva = new GameObject();
            Canva.name = "Canva (Singletone)";
            var temp_C = Canva.AddComponent<Canvas>();
            var temp_CS = Canva.AddComponent<CanvasScaler>();
            Canva.AddComponent<GraphicRaycaster>();
            temp_C.renderMode = RenderMode.ScreenSpaceOverlay;
            temp_CS.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            temp_CS.referenceResolution = new Vector2(720, 1024);
            temp_CS.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            temp_CS.matchWidthOrHeight = 0.5f;
            Canva.transform.position = new Vector3(1, 1, -1);
            gameObject.transform.SetParent(Canva.transform, false);
            DontDestroyOnLoad(Canva);
        }else
        {
            gameObject.transform.SetParent(GameObject.Find("Canva (Singletone)").transform, false);
        }
        
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
        int NumLinhas = ((int)(Texto.GetComponent<RectTransform>().sizeDelta.y / Texto.fontSize));
        int PalaporLinha = ((int)(Texto.GetComponent<RectTransform>().sizeDelta.x / Texto.fontSize));
        int _indicetemp = 0;
        int paltotal = (int)((PalaporLinha * 1.9f) * NumLinhas);
        if (paltotal == 0)
        {
            paltotal *= 5;
        }
        mensagens.Add(text);
        Nomes.Add(string.Empty);
        GameStates.IsAWindowOpen = true;
        Show();
    }
    public void WriteMessage(string text, string name)
    {
        try { pers = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>(); }
        catch { Debug.LogWarning("Não tem nenhum player nessa cena."); 
        }

        FinishiAll = false;
        if (mensagens.Count == 0) { Finish = false; indice = 0; }
        if (!painel)
        {
            painel = gameObject;
            Texto = painel.transform.FindChild("Texto").GetComponent<Text>();
            Nome = painel.transform.FindChild("Nome").GetComponent<Text>();
           
            Nome.text = string.Empty;
            Texto.text = string.Empty;
        }
        float NumLinhas = (((int)Texto.GetComponent<RectTransform>().sizeDelta.y / Texto.fontSize));
        float PalaporLinha = (((int)Texto.GetComponent<RectTransform>().sizeDelta.x / Texto.fontSize));
        int _indicetemp = 0;
        int paltotal = (int)((PalaporLinha*1.9f)*NumLinhas);
        if (paltotal == 0)
        {
            paltotal *= 5;
        }
        mensagens.Add(text);
        Nomes.Add(name);

        GameStates.IsAWindowOpen = true;


        Show();
    }
    void Update()
    {
        if (Finish)
        {
            if (mensagens.Count > 0)
            {
                if (GameInput.GetKeyDown(InputsName.Action))
                {
                    indice = 0;
                    Texto.text = string.Empty;
                    mensagens.RemoveAt(0);
                    Nomes.RemoveAt(0);
                    Finish = false;
                    Timer = 0;
                    Invoke("Show", 0.1f);
                }
            }
            else
            {
                //GameStates.IsAWindowOpen = false;
                //if (pers)
                //   pers.EstadoDoJogador = GameStates.CharacterState.Playing; 
                //painel.SetActive(false);
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
            

        }
        if (FinishiAll && painel.activeInHierarchy)
        {
            
            GameStates.IsAWindowOpen = false;
            if (pers)
            {
                pers.EstadoDoJogador = GameStates.CharacterState.Playing;
                print("Saiu");
            }
            painel.SetActive(false);
            if(IsInvoking("Show"))
            {
                CancelInvoke("Show");
            }
           
        }
       

    }
    //Escreve a mensagem na tela.
    void Show()
    {
        if (pers)
        {
            if (pers.EstadoDoJogador == GameStates.CharacterState.Playing) { pers.EstadoDoJogador = GameStates.CharacterState.DontMove; }
            print("Entrou");
        }

        if (mensagens.Count > 0)
        {
            if (indice < mensagens[0].Length)
            {
                Timer += (Time.deltaTime*2);

                if (GameInput.GetKeyDown(InputsName.Action) || Input.GetMouseButtonDown(1) && !Finish && indice > 0)
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
