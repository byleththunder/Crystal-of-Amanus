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
        if(Input.GetKeyDown(KeyCode.F2))
        {
            Save(1);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            Load(1);
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
     * @[Nome](Lucas)[Idade](20)||[Nome](Fulano)[Idade](19)/@
     */
    public void Save(int indice)
    {
        string _temp = string.Empty;
        for (int i = 0; i < _mochila.Count; i++)
        {
            if (_mochila.Count == 1)
            {
                _temp += "@!<Itens>[Nome]{" + _mochila[i].name + "}/!|";
            }
            else if (i == 0)
            {
                _temp += "@!<Itens>[Nome]{" + _mochila[i].name + "}";
                _temp += "|";
            }
            else if (i == _mochila.Count - 1)
            {
                _temp += "[Nome]{" + _mochila[i].name + "}";
                _temp += "/!";
            }
            else
            {
                _temp += "[Nome]{" + _mochila[i].name + "}";
                _temp += "|";
            }
        }
        for (int i = 0; i < Quantidades.Count; i++)
        {
            if (_mochila.Count == 1)
            {
                _temp += "!<Quantidades>[" + i + "]{" + Quantidades[i] + "}/!/@";
            }
            else if (i == 0)
            {
                _temp += "!<Quantidades>[" + i + "]{" + Quantidades[i] + "}";
                _temp += "|";
            }
            else if (i == _mochila.Count - 1)
            {
                _temp += "[" + i + "]{" + Quantidades[i] + "}";
                _temp += "/!/@";
            }
            else
            {
                _temp += "[" + i + "]{" + Quantidades[i] + "}";
                _temp += "|";
            }
        }
        print(_temp);
        PlayerPrefs.SetString("Inventario" + indice, _temp);
        print("SaveQuests OK");
    }
    public bool Load(int indice)
    {
        if (PlayerPrefs.HasKey("Inventario" + indice))
        {
            List<Item> _Itens = new List<Item>();
            List<int> _Quantidades = new List<int>();
            string _temp = PlayerPrefs.GetString("Inventario" + indice);
            print(_temp);
            string variavel = string.Empty;
            string valor = string.Empty;
            string array = string.Empty;
            bool bool_variavel = false;
            bool bool_valor = false;
            bool bool_array = false;
            bool bool_array_nome = false;
            int ListaIndice = -1;
            //------------------------
            #region Load
            for (int i = 0; i < _temp.Length; i++)
            {
                
                if (_temp[i] == ']')
                {
                    bool_variavel = false;
                }

                if (_temp[i] == '}')
                {
                    bool_valor = false;
                    #region Registrando
                    print(array);
                    if (array == "Itens") { _Itens.Add(ResourceFind.FindItem(valor)); print(valor); }
                    if (array == "Quantidades") { _Quantidades.Add(System.Convert.ToInt32(valor)); print(valor); }

                    #endregion
                    variavel = string.Empty;
                    valor = string.Empty;
                }
                //Escreve o nome da variavel
                if (bool_variavel)
                {
                    variavel += _temp[i];
                }
                //Detectou uma variavel no proximo elemento
                if (_temp[i] == '[')
                {
                    bool_variavel = true;
                }
                //Escreve o valor da variavel
                if (bool_valor)
                {
                    valor += _temp[i];
                }
                //Detecta um valor no proximo elemento
                if (_temp[i] == '{')
                {
                    bool_valor = true;
                }
                //Detecta o inicio de um array
                if (_temp[i] == '!')
                {
                    bool_array = true;
                    array = string.Empty;
                }
                if (_temp[i] == '>')
                {
                    bool_array_nome = false;
                }
                //Escreve o nome do array
                if (bool_array_nome)
                {
                    array += _temp[i];
                }
                //Detecta um array no proximo elemento
                if (_temp[i] == '<')
                {
                    bool_array_nome = true;
                }
                //Detecta o termino do nome do array
                
                //Detecta o termino de um array
                if (_temp[i] == '/' && _temp[i + 1] == '!')
                {
                    bool_array = false;
                    array = string.Empty;
                    i++;
                }


                if (_temp[i] == '/' && _temp[i + 1] == '@')
                {
                    break;
                }
            }
            #endregion
            //-------------------------
            _mochila = _Itens;
            Quantidades = _Quantidades;
            print("Mochila lenght = " + _mochila.Count);
            print("LoadQuests OK");
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
