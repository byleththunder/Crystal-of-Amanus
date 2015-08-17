using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Inventario : MonoBehaviour
{

    //Item[] _mochila = new Item[12];
    List<Item> _mochila = new List<Item>();
    List<int> Quantidades = new List<int>();
    public static Inventario Mochila;

    public int indice = 0;
    public int MochilaLenght { get { return _mochila.Count; } }
    public List<Item> MochilaRef { get { return _mochila; } }
    public List<int> QuantidadesRef { get { return Quantidades; } }
    // Use this for initialization
    void Awake()
    {
        Mochila = this;
    }
    void Start()
    {

    }
    void FixedUpdate()
    {
        if (!IsInvoking())
        {
            Invoke("ChecarMochila", 2f);
        }
    }
    /*-------------------------------------------------------------------------------
     * Nesse método eu verifico, de duas formas, se é possivel pegar o item. Primeiro eu verifico se o mesmo já existe no inventário, caso exista, eu só adiciono mais um na quantidade.
     * Se não existir o item, eu vejo se tem algum espaço vázio disponivél no inventário, caso não exista, o método retorna falso.
     --------------------------------------------------------------------------------*/
    public void PickItem(Item item, int Quantidade)
    {
        for (int i = 0; i < MochilaLenght; i++)
        {
            if (_mochila[i] == item)
            {
                Quantidades[i] += Quantidade;
                return;
            }

        }
        _mochila.Add(item);
        Quantidades.Add(Quantidade);
        
    }
    /*-------------------------------------------------------------------------------
    * Nesse método eu verifico se é possivel descartar um item. Primeiro eu vejo se eu tenho o item, se eu tiver, 
    * eu vejo se eu tenho mais de um, pois ai eu só diminuo a quantidade, caso eu só tenha 1 (um), eu retiro o item da mochila.
    --------------------------------------------------------------------------------*/
    public bool DiscartItem(Item item)
    {
        for (int i = 0; i < MochilaLenght; i++)
        {
            if (_mochila[i] == item)
            {
                if (Quantidades[i] > 1)
                {
                    Quantidades[i]--;
                }
                else
                {
                    _mochila.RemoveAt(i);
                    Quantidades.RemoveAt(i);
                }
                return true;
            }
        }
        return false;
    }
    public string[] ItensNames()
    {
        string[] temp = new string[MochilaLenght];
        for (int i = 0; i < temp.Length; i++)
        {
            if (_mochila[i] != null)
            {
                temp[i] = _mochila[i].Nome;
            }
            else
            {
                temp[i] = "Slot Vázio";
            }
        }
        return temp;
    }
    void ChecarMochila()
    {
        for (int i = 0; i < MochilaLenght; i++)
        {
            if (Quantidades[i] <= 0)
            {
                _mochila.RemoveAt(i);
                Quantidades.RemoveAt(i);
            }
        }
    }
    public void UsarItem(Item _iten, Target alvo)
    {
        for(int i = 0; i <_mochila.Count; i++)
        {
            if(_mochila[i] == _iten)
            {
                if(_mochila[i].MetodoItem(alvo))
                {
                    Quantidades[i]--;
                    if(Quantidades[i]<=0)
                    {
                        _mochila.RemoveAt(i);
                        Quantidades.RemoveAt(i);
                        break;
                    }
                }
            }
        }
    }
}
