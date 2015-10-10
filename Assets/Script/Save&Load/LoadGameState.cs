using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

[AddComponentMenu("Scripts/Save e Load/Load String")]
public class LoadGameState : MonoBehaviour
{

    /*
     * Salvando e Carregando itens no inventário. 
     *Protocolo de Save
     * @ - Começa
     * /@ - Termina
     * [Vareiavel]
     * {Valor}
     * | - Separa
     * !<Nome> - Array Inicio
     * /! - ArrayFim
     * #<Nome> - Vector Inicio
     * /# - Vector Fim
     * @[Nome]{Lucas}[Idade]{20}|[Nome]{Fulano}[Idade]{19}/@
     */

    public static List<int> LoadIntList(string Load, string MyList)
    {
        //Bolean
        bool ListName = false;
        bool Variavel = false;
        bool Valor = false;
        bool Iniciar = false;
        //Strings
        string _valor = string.Empty;
        string _variavel = string.Empty;
        string _listName = string.Empty;
        //Lista Genérica
        List<int> _tempList = new List<int>();

        //Int Indice da lista
        int indice = -1;
        //Leitura de protocolo
        for (int i = 0; i < Load.Length - 1; i++)
        {
            #region Nome da Lista
            if (Load[i] == '!' && Load[i + 1] == '<')//Verifico se a lista começou
            {
                ListName = true;
                i += 2;
            }
            if (ListName)//Escrevo o nome da lista
            {
                _listName += Load[i];
            }
            if (Load[i + 1] == '>' && ListName)//Verifico se o nome da lista já acabou.
            {
                if (_listName == MyList)
                {

                    Iniciar = true;
                }
                ListName = false;
                i += 2;
            }
            #endregion
            #region Nome da Variavel
            if (Load[i] == '[')
            {
                if (Iniciar)
                {
                    _tempList.Add(0);
                    indice++;
                }
                Variavel = true;
                i++;
            }
            if (Variavel)
            {
                _variavel += Load[i];
            }
            if (Load[i + 1] == ']')
            {
                Variavel = false;
                i++;
            }
            #endregion
            #region Nome do Valor
            if (Load[i] == '{')
            {
                Valor = true;
                i++;
            }
            if (Valor)
            {
                _valor += Load[i];
            }
            if (Load[i + 1] == '}')
            {
                if (Iniciar)
                {
                    //(_valor);
                    #region Adicionando varivel na Lista

                    var _v = int.Parse(_valor);
                    //(typeof(T).Name);
                    _tempList[indice] = _v;
                    #endregion
                }
                _valor = string.Empty;
                _variavel = string.Empty;
                Valor = false;
                i++;
            }
            #endregion
            #region Fim da Lista
            if (Load[i] == '/' && Load[i + 1] == '!')
            {
                _listName = string.Empty;
                if (Iniciar)
                {
                    return _tempList;
                }
            }
            #endregion

        }
        //envio da Lista Genérica preenchida.
        return null;
    }
    public static List<float> LoadFloatList(string Load, string MyList)
    {
        //Bolean
        bool ListName = false;
        bool Variavel = false;
        bool Valor = false;
        bool Iniciar = false;
        //Strings
        string _valor = string.Empty;
        string _variavel = string.Empty;
        string _listName = string.Empty;
        //Lista Genérica
        List<float> _tempList = new List<float>();

        //Int Indice da lista
        int indice = -1;
        //Leitura de protocolo
        for (int i = 0; i < Load.Length - 1; i++)
        {
            #region Nome da Lista
            if (Load[i] == '!' && Load[i + 1] == '<')//Verifico se a lista começou
            {
                ListName = true;
                i += 2;
            }
            if (ListName)//Escrevo o nome da lista
            {
                _listName += Load[i];
            }
            if (Load[i + 1] == '>' && ListName)//Verifico se o nome da lista já acabou.
            {

                if (_listName == MyList)
                {

                    Iniciar = true;
                }
                ListName = false;
                i += 2;
            }
            #endregion
            #region Nome da Variavel
            if (Load[i] == '[')
            {
                if (Iniciar)
                {
                    _tempList.Add(0);
                    indice++;
                }
                Variavel = true;
                i++;
            }
            if (Variavel)
            {
                _variavel += Load[i];
            }
            if (Load[i + 1] == ']')
            {
                Variavel = false;
                i++;
            }
            #endregion
            #region Nome do Valor
            if (Load[i] == '{')
            {
                Valor = true;
                i++;
            }
            if (Valor)
            {
                _valor += Load[i];
            }
            if (Load[i + 1] == '}')
            {
                if (Iniciar)
                {
                    //(_valor);
                    #region Adicionando varivel na Lista

                    var _v = float.Parse(_valor);
                    _tempList[indice] = _v;
                    #endregion
                }
                _valor = string.Empty;
                _variavel = string.Empty;
                Valor = false;
                i++;
            }
            #endregion
            #region Fim da Lista
            if (Load[i] == '/' && Load[i + 1] == '!')
            {
                _listName = string.Empty;
                if (Iniciar)
                {
                    return _tempList;
                }
            }
            #endregion

        }
        //envio da Lista Genérica preenchida.
        return null;
    }
    public static List<string> LoadStringList(string Load, string MyList)
    {
        //Bolean
        bool ListName = false;
        bool Variavel = false;
        bool Valor = false;
        bool Iniciar = false;
        //Strings
        string _valor = string.Empty;
        string _variavel = string.Empty;
        string _listName = string.Empty;
        //Lista Genérica
        List<string> _tempList = new List<string>();

        //Int Indice da lista
        int indice = -1;
        //Leitura de protocolo
        for (int i = 0; i < Load.Length - 1; i++)
        {
            #region Nome da Lista
            if (Load[i] == '!' && Load[i + 1] == '<')//Verifico se a lista começou
            {
                ListName = true;
                i += 2;
            }
            if (ListName)//Escrevo o nome da lista
            {
                _listName += Load[i];
            }
            if (Load[i + 1] == '>' && ListName)//Verifico se o nome da lista já acabou.
            {
                if (_listName == MyList)
                {

                    Iniciar = true;
                }
                ListName = false;
                i += 2;
            }
            #endregion
            #region Nome da Variavel
            if (Load[i] == '[')
            {
                if (Iniciar)
                {
                    _tempList.Add(string.Empty);
                    indice++;
                }
                Variavel = true;
                i++;
            }
            if (Variavel)
            {
                _variavel += Load[i];
            }
            if (Load[i + 1] == ']')
            {
                Variavel = false;
                i++;
            }
            #endregion
            #region Nome do Valor
            if (Load[i] == '{')
            {
                Valor = true;
                i++;
            }
            if (Valor)
            {
                _valor += Load[i];
            }
            if (Load[i + 1] == '}')
            {
                if (Iniciar)
                {
                    //(_valor);
                    #region Adicionando varivel na Lista

                    _tempList[indice] = _valor;
                    #endregion
                }
                _valor = string.Empty;
                _variavel = string.Empty;
                Valor = false;
                i++;
            }
            #endregion
            #region Fim da Lista
            if (Load[i] == '/' && Load[i + 1] == '!')
            {
                _listName = string.Empty;
                if (Iniciar)
                {
                    return _tempList;
                }
            }
            #endregion

        }
        //envio da Lista Genérica preenchida.
        return null;
    }
    public static List<bool> LoadBooleanList(string Load, string MyList)
    {
        //Bolean
        bool ListName = false;
        bool Variavel = false;
        bool Valor = false;
        bool Iniciar = false;
        //Strings
        string _valor = string.Empty;
        string _variavel = string.Empty;
        string _listName = string.Empty;
        //Lista Genérica
        List<bool> _tempList = new List<bool>();

        //Int Indice da lista
        int indice = -1;
        //Leitura de protocolo
        for (int i = 0; i < Load.Length - 1; i++)
        {
            #region Nome da Lista
            if (Load[i] == '!' && Load[i + 1] == '<')//Verifico se a lista começou
            {
                ListName = true;
                i += 2;
            }
            if (ListName)//Escrevo o nome da lista
            {
                _listName += Load[i];
            }
            if (Load[i + 1] == '>' && ListName)//Verifico se o nome da lista já acabou.
            {
                if (_listName == MyList)
                {

                    Iniciar = true;
                }
                ListName = false;
                i += 2;
            }
            #endregion
            #region Nome da Variavel
            if (Load[i] == '[')
            {
                if (Iniciar)
                {
                    _tempList.Add(false);
                    indice++;
                }
                Variavel = true;
                i++;
            }
            if (Variavel)
            {
                _variavel += Load[i];
            }
            if (Load[i + 1] == ']')
            {
                Variavel = false;
                i++;
            }
            #endregion
            #region Nome do Valor
            if (Load[i] == '{')
            {
                Valor = true;
                i++;
            }
            if (Valor)
            {
                _valor += Load[i];
            }
            if (Load[i + 1] == '}')
            {
                if (Iniciar)
                {
                    //(_valor);
                    #region Adicionando varivel na Lista

                    var _v = System.Convert.ToBoolean(_valor);
                    _tempList[indice] = _v;
                    #endregion
                }
                _valor = string.Empty;
                _variavel = string.Empty;
                Valor = false;
                i++;
            }
            #endregion
            #region Fim da Lista
            if (Load[i] == '/' && Load[i + 1] == '!')
            {
                _listName = string.Empty;
                if (Iniciar)
                {
                    return _tempList;
                }
            }
            #endregion

        }
        //envio da Lista Genérica preenchida.
        return null;
    }
    public static List<T> LoadList<T>(string Load, string MyList)
    {
        //Bolean
        bool ListName = false;
        bool Variavel = false;
        bool Valor = false;
        bool Iniciar = false;
        //Strings
        string _valor = string.Empty;
        string _variavel = string.Empty;
        string _listName = string.Empty;
        //Lista Genérica
        List<T> _tempList = new List<T>();
        System.Type genericclass = System.Type.GetType(typeof(T).ToString());
        TypeConverter c = new TypeConverter();
        //Int Indice da lista
        int indice = -1;
        //Leitura de protocolo
        for (int i = 0; i < Load.Length - 1; i++)
        {
            #region Nome da Lista
            if (Load[i] == '!' && Load[i + 1] == '<')//Verifico se a lista começou
            {
                ListName = true;
                i += 2;
            }
            if (ListName)//Escrevo o nome da lista
            {
                _listName += Load[i];
            }
            if (Load[i + 1] == '>' && ListName)//Verifico se o nome da lista já acabou.
            {
                if (_listName == MyList)
                {

                    Iniciar = true;
                }
                ListName = false;
                i += 2;
            }
            #endregion
            #region Nome da Variavel
            if (Load[i] == '[')
            {
                if (Iniciar)
                {
                    var obj = new object();
                    obj = c.ConvertTo(obj, genericclass);
                    _tempList.Add((T)obj);


                    indice++;
                }
                Variavel = true;
                i++;
            }
            if (Variavel)
            {
                _variavel += Load[i];
            }
            if (Load[i + 1] == ']')
            {
                Variavel = false;
                i++;
            }
            #endregion
            #region Nome do Valor
            if (Load[i] == '{')
            {
                Valor = true;
                i++;
            }
            if (Valor)
            {
                _valor += Load[i];
            }
            if (Load[i + 1] == '}')
            {
                if (Iniciar)
                {
                    //(_valor);
                    #region Adicionando varivel na Lista

                    var _v = c.ConvertTo(_valor, genericclass);
                    //(typeof(T).Name);
                    _tempList[indice] = ((T)(_v));
                    #endregion
                }
                _valor = string.Empty;
                _variavel = string.Empty;
                Valor = false;
                i++;
            }
            #endregion
            #region Fim da Lista
            if (Load[i] == '/' && Load[i + 1] == '!')
            {
                _listName = string.Empty;
                if (Iniciar)
                {
                    return _tempList;
                }
            }
            #endregion

        }
        //envio da Lista Genérica preenchida.
        return null;
    }
    public static List<T> LoadList<T>(string Load, string MyList, string[] variaveis)
    {
        //Bolean
        bool ListName = false;
        bool Variavel = false;
        bool Valor = false;
        bool Iniciar = false;
        //Strings
        string _valor = string.Empty;
        string _variavel = string.Empty;
        string _listName = string.Empty;
        //Lista Genérica
        System.Type genericclass = System.Type.GetType(typeof(T).ToString());

        List<T> _tempList = new List<T>();
        //Int Indice da lista
        int indice = -1;
        //Leitura de protocolo
        for (int i = 0; i < Load.Length - 1; i++)
        {
            #region Nome da Lista
            if (Load[i] == '!' && Load[i + 1] == '<')//Verifico se a lista começou
            {
                ListName = true;
                i += 2;
            }
            if (ListName)//Escrevo o nome da lista
            {
                _listName += Load[i];
            }
            if (Load[i + 1] == '>' && ListName)//Verifico se o nome da lista já acabou.
            {
                if (_listName == MyList)
                {
                    Iniciar = true;
                }
                ListName = false;
                i += 2;
            }
            #endregion
            #region Nome da Variavel
            if (Load[i] == '[')
            {
                if (Iniciar)
                {
                    var obj = System.Activator.CreateInstance(genericclass, false);
                    _tempList.Add((T)obj);
                    // _tempList.Add(obj2); 
                    indice++;

                }
                Variavel = true;
                i++;
            }
            if (Variavel)
            {
                _variavel += Load[i];
            }
            if (Load[i + 1] == ']')
            {
                Variavel = false;
                i++;
            }
            #endregion
            #region Nome do Valor
            if (Load[i] == '{')
            {
                Valor = true;
                i++;
            }
            if (Valor)
            {
                _valor += Load[i];
            }
            if (Load[i + 1] == '}')
            {
                if (Iniciar)
                {
                    #region Adicionando varivel na Lista
                    foreach (string variable in variaveis)
                    {
                        if (_variavel == variable)
                        {
                            try
                            {
                                typeof(T).GetProperty(_variavel).SetValue(_tempList[indice], _valor, null);
                            }
                            catch
                            {
                                typeof(T).GetField(_variavel).SetValue(_tempList[indice], _valor);
                            }

                            break;
                        }
                    }
                }
                    #endregion
                _variavel = string.Empty;
                _valor = string.Empty;
                Valor = false;
                i++;
            }
            #endregion
            #region Fim da Lista
            if (Load[i] == '/' && Load[i + 1] == '!')
            {
                _listName = string.Empty;
                if (Iniciar)
                {
                    return _tempList;
                }
            }
            #endregion
        }
        //envio da Lista Genérica preenchida.
        return null;
    }
}
