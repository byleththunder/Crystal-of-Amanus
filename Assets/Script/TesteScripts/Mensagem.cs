using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System;
using UnityEngine.UI;

public class Mensagem : MonoBehaviour
{
    bool mostrarMsg = false;
    
    public GUISkin skin;
    public bool EmPartes = true;
    string MensagemExibida = string.Empty;
    public float Timer = 0;
    int lenght = 0;
    bool vez = false;
    bool vez2 = false;
    bool avancar = false;
    Rect PosicaoDaMensagem, PosicaoString;
    
    int IndiceMensagem = 0;
    public int PalavrasPorMensagem;
    bool OneTime = false;
    bool JustOnce = false;
    public float TamanhoH, TamanhoW;
    //new
    private XmlTextReader leitor;
    private string fileLocation;
    List<Message> MenssagemRecebida,Menssagem;
    public string FileName;
    //Visual
    public GameObject MessagePrefab;
    public Text TxtNome;
    Text Texto;
    GameObject prefab;
    // Use this for initialization
    void Start()
    {
        TamanhoW = Screen.width / 2;
        TamanhoH = (float)((int)(Screen.height / 9));
        PosicaoDaMensagem = new Rect(Screen.width / 5, (Screen.height - TamanhoH), TamanhoW, TamanhoH);
        float f = (Screen.width / 5) + (Screen.width / 4);
        PosicaoString = new Rect(f, (Screen.height - (Screen.height / 20)), TamanhoW/2, TamanhoH);
        ReadFile(FileName);
        MenssagemRecebida = LoadXml();
        Visual();




    }

    // Update is called once per frame
    void Update()
    {
        if (this.enabled)
        {
            if (JustOnce)
            {
                Visual();
                mostrarMsg = true;
                JustOnce = false;

            }
            if (mostrarMsg)
            {
                //Impressão da Mensagem
                Texto.text = MensagemExibida;
                TxtNome.text = Menssagem[IndiceMensagem].Nome;
               
                //Controle da Mensagem
                if (Input.GetButtonDown("Action"))
                {
                    #region Controle das Mensagens
                    if (avancar || (IndiceMensagem == Menssagem.Count - 1 && MensagemExibida.Length == Menssagem[Menssagem.Count - 1].Mensagem.Length))
                    {
                        mostrarMsg = false;
                        Destroy(prefab);
                        IndiceMensagem = 0;
                        lenght = 0;
                        JustOnce = true;
                        this.enabled = false;

                    }
                    if (!avancar && Menssagem.Count == IndiceMensagem && Menssagem[IndiceMensagem].Mensagem.Length == lenght)
                    {
                        avancar = true;

                        vez2 = true;
                    }
                    else if (!avancar && Menssagem[IndiceMensagem].Mensagem.Length == lenght)
                    {
                        EmPartes = true;
                        lenght = 0;
                        IndiceMensagem++;
                        MensagemExibida = string.Empty;

                        vez2 = false;


                    }
                    else if (!avancar && Menssagem[IndiceMensagem].Mensagem.Length > lenght)
                    {
                        EmPartes = false;
                        MensagemExibida = Menssagem[IndiceMensagem].Mensagem;
                        lenght = Menssagem[IndiceMensagem].Mensagem.Length;
                    }



                }
                    #endregion
                if (!avancar)
                {

                    Timer += (2 * Time.deltaTime);
                    if (Timer > 0.1f)
                    {
                        Timer = 0;
                        if (EmPartes)
                        {
                            try
                            {
                                if (IndiceMensagem < Menssagem.Count)
                                {
                                    if (lenght < Menssagem[IndiceMensagem].Mensagem.Length)
                                    {
                                        MensagemExibida += Menssagem[IndiceMensagem].Mensagem[lenght];
                                        lenght++;

                                    }
                                    else
                                    {
                                        if (!vez2)
                                        {

                                            vez2 = true;

                                        }
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                Debug.Log(e.Message.ToString());
                            }
                        }
                    }
                }
                else
                {
                    Timer = 0;
                    MensagemExibida = Menssagem[IndiceMensagem].Mensagem;
                }
            }

        }
        else
        {
            JustOnce = true;
        }
        
    }

    void OnTriggerStay(Collider other)
    {
        if(this.enabled){
            if (other.tag == "Player")
            {
                if (!vez)
                {

                    //gameObject.audio.Play();
                    //gameObject.audio.loop = true;
                    vez = true;
                    mostrarMsg = true;

                }

            }

        }
    }

    
    void Visual()
    {
        
        //MessagePrefab.SetActive(true);
        prefab = Instantiate(MessagePrefab) as GameObject;
        prefab.transform.SetParent(GameObject.Find("Canvas").transform, false);
        Texto = prefab.GetComponentInChildren<Text>();
        float PalavrasPorLargura = Texto.rectTransform.sizeDelta.x / Texto.fontSize;
        float PalavrasPorAltura = (Texto.rectTransform.sizeDelta.y - Texto.fontSize) / Texto.fontSize;
        PalavrasPorMensagem = (int)(PalavrasPorLargura * PalavrasPorAltura);

        Menssagem = ConvertMsgToString(MenssagemRecebida);
        
    }
   
    List<string> ArrangeMessageInAList(string mensagemCompleta, int NumberOfWordsPerMessage)
    {
        List<string> Mensagem = new List<string>();

        int j = 0;
        Mensagem.Add(string.Empty);
        for (int i = 0; i < mensagemCompleta.Length - 1; i++)
        {
            Mensagem[j] += mensagemCompleta[i];
            if (i >= NumberOfWordsPerMessage * (j + 1) && mensagemCompleta[i] == ' ')
            {
                Mensagem.Add(string.Empty);
                j++;

            }
            //else if (i >= (NumberOfWordsPerMessage * (j + 1)) + 5)
            //{
            //    Mensagem.Add(string.Empty);
            //    j++;
            //}



        }
        return Mensagem;
    }
    //New
    public void ReadFile(string name)
    {
        TextAsset TxtAsset = (TextAsset)Resources.Load(name, typeof(TextAsset));
        leitor = new XmlTextReader(new StringReader(TxtAsset.text));
    }
    public List<Message> LoadXml()
    {
        
        List<Message> m = new List<Message>();
        int percorrer = 0;
        int i = -1;
        while (leitor.Read())
        {
            //verificacao do tipo de nó lido 
            switch (leitor.NodeType)
            {
                case XmlNodeType.Element:
                    if (leitor.Name == "Fala")
                    {
                        m.Add(new Message());
                        i++;
                    }
                    break;
                case XmlNodeType.Text:
                    if (percorrer == 0)
                    {
                        m[i].Nome = leitor.Value;
                        percorrer++;
                        break;
                    }
                    if (percorrer == 1)
                    {
                        m[i].Mensagem = leitor.Value;
                        percorrer = 0;
                        break;
                    }
                    break;
                case XmlNodeType.EndElement:

                    //Display the end of the element. 
                    break;
            }
        }
        //fechamento do arquivo XML 
        leitor.Close();
        
        return m;
    }
    public List<Message> ConvertMsgToString(List<Message> msg1)
    {
        List<Message> m = new List<Message>();
        bool finish = false;
        int i = 0;
        
        while (!finish)
        {

           
            if (msg1[i].Mensagem.Length <= PalavrasPorMensagem)
            {
                m.Add(msg1[i]);
                i++;
                
            }
            else
            {
                
                List<string> temp = ArrangeMessageInAList(msg1[i].Mensagem, PalavrasPorMensagem);
                for (int f = 0; f < temp.Count; f++)
                {
                    m.Add(new Message() { Mensagem = temp[f], Nome = msg1[i].Nome });
                }
                
                i++;
                
            }
            if (i == msg1.Count)
            {
                finish = true;
            }
        }
        
        return m;
    }
    public string msg { get; set; }
}
