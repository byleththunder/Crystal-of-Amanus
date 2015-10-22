using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlertMessage : Singleton<AlertMessage>
{

    protected AlertMessage() { }
    //Imagem do Item
    public Image Img;
    //Nome do item
    public Text texto;
    //painel da mensagem
    GameObject Painel;
    //Autorização para parar de mostrar a messagem.
    bool autorizacao = false;
    //contador de tempo
    float cont = 0;
    void Awake()
    {
        GameObject canvas = GameObject.Find("Canvas");
        gameObject.transform.SetParent(canvas.transform, false);
    }
    public void ShowAlert(Item iten, int quantidade)
    {
        if (Painel == null)
        {
            Painel = gameObject;
            Img = Painel.transform.FindChild("Image").GetComponent<Image>();
            texto = Painel.transform.FindChild("Text").GetComponent<Text>();
        }
        Img.sprite = iten.Img;
        texto.text = iten.name + " x" + quantidade;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().EstadoDoJogador = GameStates.CharacterState.DontMove;
        Time.timeScale = 0;
    }
    public void ShowAlert(int Ag)
    {
        if (Painel == null)
        {
            Painel = gameObject;
            Img = Painel.transform.FindChild("Image").GetComponent<Image>();
            texto = Painel.transform.FindChild("Text").GetComponent<Text>();
        }
        texto.text = "Adquiriu " + Ag + " Ag";
        //Img.sprite = null; //Adicionar imagem de moedas de prata.
        GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().EstadoDoJogador = GameStates.CharacterState.DontMove;
        Time.timeScale = 0;
        
    }

    void Update()
    {


        if (Painel != null)
        {
            if (Painel.activeInHierarchy && autorizacao)
            {
                if (Input.anyKeyDown)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().EstadoDoJogador = GameStates.CharacterState.Playing;
                    Time.timeScale = 1;
                    autorizacao = false;
                    Painel.SetActive(false);
                }
            }else
            {
                if(cont < 1)
                {
                    cont += 0.1f;
                }else
                {
                    autorizacao = true;
                    cont = 0;
                }
            }
        }
    }

    
}
