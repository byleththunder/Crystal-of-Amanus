using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ItemContent : MonoBehaviour
{
    public Image sprite;
    public Text texto;
    public Item iten;
    public Image Painel;
    public bool Usado = false;
    public int quantidade;
    public int x, y;
    bool change = true;
    Color cor;

    // Use this for initialization
    void Start()
    {

        Painel = this.gameObject.GetComponent<Image>();
        cor = Painel.color;


    }

    public void ChangeColor(bool interruptor)
    {
        if (interruptor)
        {
            Painel.color = Color.blue;
        }
        else
        {
            Painel.color = cor;
        }
    }
    public bool UsarItem(ITarget alvo)
    {
        //Usado = false;
        print("Teste: " + (Usado == null));
        Usado = iten.MetodoItem(alvo);
        if (quantidade == 1 && Usado)
        {
            quantidade--;
        }
        return Usado;
    }
    // Update is called once per frame
    void Update()
    {
        if (quantidade == 0)
        {
            ResetInfo();
        }
    }
    void ResetInfo()
    {
        iten = null;
        sprite.sprite = null;
        texto.text = "Slot Empty";
    }
    public void AdicionarItem(Item item, int _quantidade)
    {
        if (iten == item)
        {
            quantidade = _quantidade;
            texto.text = iten.Nome + " - x" + quantidade;
        }
        else
        {
           
            iten = item;
            quantidade = _quantidade;
            texto.text = iten.Nome + " - x" + quantidade;
            sprite.sprite = iten.Img;
           
        }
    }
}
