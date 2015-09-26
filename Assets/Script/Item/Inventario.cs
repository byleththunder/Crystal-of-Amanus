using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventario : MonoBehaviour
{

    public static Inventario Mochila;
    public static string SaveInvent;
    //Item[] _mochila = new Item[12];
    List<Item> _mochila = new List<Item>();
    List<int> Quantidades = new List<int>();
    

    public int indice = 0;
    public int MochilaLenght { get { return _mochila.Count; } }
    public List<Item> MochilaRef { get { return _mochila; } }
    public List<int> QuantidadesRef { get { return Quantidades; } }
    // Use this for initialization
    void Awake()
    {
        Mochila = this;
       // SaveLoad.Load();
    }
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
            //Debug.LogError("invent é null");
        }
        
    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            //Save(1);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
           // Load(1);
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            Delete(1);
        }
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
    public void UsarItem(Item _iten, Target alvo)
    {
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
                        break;
                    }
                }
            }
        }
    }
    /*
     *Salvando e Carregando itens no inventário. 
     *Protocolo de Save
     * @ - Começa
     * [Vareiavel]
     * {Valor}
     * | - Separa
     * !<Nome> - Array Inicio
     * /! - ArrayFim
     * /@ - Termina
     * @[Nome]{Lucas}[Idade]{20}|[Nome](Fulano)[Idade](19)/@
     */
    public void Save()
    {
        string _temp = string.Empty;
        _temp += SaveGameState.IniciarSave();
        _temp += SaveGameState.SalvarLista<Item>(_mochila, "Itens", new string[1] { "Nome" }, true);
        _temp += SaveGameState.SalvarLista<int>(Quantidades, "Quantidades");
        _temp += SaveGameState.FinalizarSave();
        SaveInvent = _temp;
        //PlayerPrefs.SetString("Inventario" + indice, _temp);
        print("SaveQuests OK");
    }
    public bool Load()
    {
        if(!string.IsNullOrEmpty(SaveInvent))
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
        print("LoadQuests Failed");
        return false;
    }
    public void Delete(int indice)
    {
        if (PlayerPrefs.HasKey("Inventario" + indice))
        {
            PlayerPrefs.DeleteKey("Inventario" + indice);
            print("DeletouQuest" + indice);
            return;
        }
        print("Não existe Quest" + indice);
    }
}
