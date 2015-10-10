using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Scripts/Item/Inventario")]
public class Inventario : MonoBehaviour
{
    /// <summary>
    /// Mochila aonde guarda os itens.
    /// </summary>

    [Tooltip("Itens que estão no seu Inventário")]
    public static string SaveInvent;
    //Item[] _mochila = new Item[12];
    List<Item> _mochila = new List<Item>();
    List<int> Quantidades = new List<int>();


    public int indice = 0;
    public int MochilaLenght { get { return _mochila.Count; } }
    public List<Item> MochilaRef { get { return _mochila; } }
    public List<int> QuantidadesRef { get { return Quantidades; } }
    
    void Start()
    {
        try
        {
            if (!string.IsNullOrEmpty(Game.current.Invent))
            {
                Inventario.SaveInvent = Game.current.Invent;
                Load();
                print(MochilaLenght);
            }
        }
        catch
        {
        }

    }
    void LateUpdate()
    {
        ChecarMochila();
    }

    ///<summary "Visual Content Item Behavior">
    /// O VCitem vai se comunicar diretamente com o inventario do jogador.
    /// Ele vai pegar, a partir de métodos, informações do item que eles tem. Usando somente o indice.
    ///<!---->
    public Item ItemInformations(int indice)
    {
        return _mochila[indice];
    }
    public int QuantityInformations(int indice)
    {
        return Quantidades[indice];
    }
    public int QuantityInformations(Item iten)
    {
        return Quantidades[_mochila.FindIndex(x => x == iten)];
    }
    public bool UsarItem(Item _iten, Target alvo)
    {
        bool HasItem = true;
        for (int i = 0; i < _mochila.Count; i++)
        {
            if (_mochila[i] == _iten)
            {
                if (_mochila[i].MetodoItem(alvo))
                {
                    Quantidades[i]--;
                    if (Quantidades[i] <= 0)
                    {
                        _mochila.RemoveAt(i);
                        Quantidades.RemoveAt(i);
                        HasItem = false;
                        break;
                    }
                    break;
                }
            }
        }
        return HasItem;
    }
    public int ComprimentoDaMochila()
    {
        return _mochila.Count;
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
        gameObject.SendMessage("ChecarQuestItem", item);
        Save();
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

    /// <summary"Save & Load Methods">
    /// Salvo em formato de string as informações importantes dessa classe e mando para a classe Game.
    /// O Load pega a string na classe Game e converte para Members.
    /// </summary>
    public void Save()
    {
        string _temp = string.Empty;
        _temp += SaveGameState.IniciarSave();
        _temp += SaveGameState.SalvarLista<Item>(_mochila, "Itens", new string[1] { "Nome" }, true);
        _temp += SaveGameState.SalvarLista<int>(Quantidades, "Quantidades");
        _temp += SaveGameState.FinalizarSave();
        SaveInvent = _temp;
        //PlayerPrefs.SetString("Inventario" + indice, _temp);
    }
    public bool Load()
    {
        if (!string.IsNullOrEmpty(SaveInvent))
        {
            List<string> _Itens = LoadGameState.LoadStringList(SaveInvent, "Itens");
            for (int i = 0; i < _Itens.Count; i++)
            {
                _mochila.Add(ResourceFind.FindItem(_Itens[i]));
            }
            Quantidades = LoadGameState.LoadIntList(SaveInvent, "Quantidades");
            print("Mochila lenght = " + _mochila[0].Nome);
            return true;
        }
        return false;
    }

}
