using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[AddComponentMenu("Scripts/Save e Load/Save String")]
public class SaveGameState : MonoBehaviour {
    /* Protocolo para salvar e carregar. 
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
    public static string IniciarSave()
    {
        return "@";
    }
    public static string FinalizarSave()
    {
        return"/@";
    }
    public static string SalvarLista<T>(List<T> lista, string name)
    {
        string _temp = string.Empty;

        #region Salvando Lista em String
        for (int i = 0; i < lista.Count; i++)
        {
            if (lista.Count == 1)
            {
                _temp += "!<" + name + ">";
                _temp += "[" + i + "]{" + lista[i] + "}";
                _temp += "/!";

            }
            else if (i == 0)
            {
                _temp += "!<" + name + ">";
                _temp += "[" + i + "]{" + lista[i] + "}";
                _temp += "|";
            }
            else if (i == lista.Count - 1)
            {
                _temp += "[" + i + "]{" + lista[i] + "}";
                _temp += "/!";
            }
            else
            {
                _temp += "[" + i + "]{" + lista[i] + "}";
                _temp += "|";
            }
        }
        #endregion
        return _temp;
    }
    public static string SalvarLista<T>(List<T> lista, string name, bool more)
    {
        string _temp = string.Empty;

        #region Salvando Lista em String
        for (int i = 0; i < lista.Count; i++)
        {
            if (lista.Count == 1)
            {
                _temp += "!<" + name + ">";
                _temp += "[" + i + "]{" + lista[i] + "}";
                _temp += "/!";

            }
            else if (i == 0)
            {
                _temp += "!<" + name + ">";
                _temp += "[" + i + "]{" + lista[i] + "}";
                _temp += "|";
            }
            else if (i == lista.Count - 1)
            {
                _temp += "[" + i + "]{" + lista[i] + "}";
                _temp += "/!";
            }
            else
            {
                _temp += "[" + i + "]{" + lista[i] + "}";
                _temp += "|";
            }
        }
        #endregion
        if(more)
        {
            _temp += '|';
        }
        return _temp;
    }
    public static string SalvarLista<T>( List<T> lista, string name, string[] variaveis)
    {
        string _temp = string.Empty;
        
        #region Salvando Lista em String
        for (int i =0; i < lista.Count; i ++)
        {
            if (lista.Count == 1)
            {
                _temp += "!<" + name+">";
                for(int j = 0; j <variaveis.Length; j++)
                {
                    try { _temp += "[" + variaveis[j] + "]{" + typeof(T).GetProperty(variaveis[j]).GetValue(lista[i], null).ToString() + "}"; }
                    catch { _temp += "[" + variaveis[j] + "]{" + typeof(T).GetField(variaveis[j]).GetValue(lista[i]).ToString() + "}"; }
                    
                }
                _temp += "/!";
                
            }
            else if (i == 0)
            {
                _temp += "!<" + name + ">";
                for (int j = 0; j < variaveis.Length; j++)
                {
                    try { _temp += "[" + variaveis[j] + "]{" + typeof(T).GetProperty(variaveis[j]).GetValue(lista[i], null).ToString() + "}"; }
                    catch { _temp += "[" + variaveis[j] + "]{" + typeof(T).GetField(variaveis[j]).GetValue(lista[i]).ToString() + "}"; }
                }
                _temp += "|";
            }
            else if (i == lista.Count - 1)
            {
                for (int j = 0; j < variaveis.Length; j++)
                {
                    try { _temp += "[" + variaveis[j] + "]{" + typeof(T).GetProperty(variaveis[j]).GetValue(lista[i], null).ToString() + "}"; }
                    catch { _temp += "[" + variaveis[j] + "]{" + typeof(T).GetField(variaveis[j]).GetValue(lista[i]).ToString() + "}"; }
                }
                _temp += "/!";
            }
            else
            {
                for (int j = 0; j < variaveis.Length; j++)
                {
                    try { _temp += "[" + variaveis[j] + "]{" + typeof(T).GetProperty(variaveis[j]).GetValue(lista[i], null).ToString() + "}"; }
                    catch { _temp += "[" + variaveis[j] + "]{" + typeof(T).GetField(variaveis[j]).GetValue(lista[i]).ToString() + "}"; }
                }
                _temp += "|";
            }
        }
        #endregion
        return _temp;
    }
    public static string SalvarLista<T>(List<T> lista, string name, string[] variaveis, bool more)
    {
        string _temp = string.Empty;

        #region Salvando Lista em String
        for (int i = 0; i < lista.Count; i++)
        {
            if (lista.Count == 1)
            {
                _temp += "!<" + name + ">";
                for (int j = 0; j < variaveis.Length; j++)
                {
                    try { _temp += "[" + variaveis[j] + "]{" + typeof(T).GetProperty(variaveis[j]).GetValue(lista[i], null).ToString() + "}"; }
                    catch { _temp += "[" + variaveis[j] + "]{" + typeof(T).GetField(variaveis[j]).GetValue(lista[i]).ToString() + "}"; }

                }
                _temp += "/!";

            }
            else if (i == 0)
            {
                _temp += "!<" + name + ">";
                for (int j = 0; j < variaveis.Length; j++)
                {
                    try { _temp += "[" + variaveis[j] + "]{" + typeof(T).GetProperty(variaveis[j]).GetValue(lista[i], null).ToString() + "}"; }
                    catch { _temp += "[" + variaveis[j] + "]{" + typeof(T).GetField(variaveis[j]).GetValue(lista[i]).ToString() + "}"; }
                }
                _temp += "|";
            }
            else if (i == lista.Count - 1)
            {
                for (int j = 0; j < variaveis.Length; j++)
                {
                    try { _temp += "[" + variaveis[j] + "]{" + typeof(T).GetProperty(variaveis[j]).GetValue(lista[i], null).ToString() + "}"; }
                    catch { _temp += "[" + variaveis[j] + "]{" + typeof(T).GetField(variaveis[j]).GetValue(lista[i]).ToString() + "}"; }
                }
                _temp += "/!";
            }
            else
            {
                for (int j = 0; j < variaveis.Length; j++)
                {
                    try { _temp += "[" + variaveis[j] + "]{" + typeof(T).GetProperty(variaveis[j]).GetValue(lista[i], null).ToString() + "}"; }
                    catch { _temp += "[" + variaveis[j] + "]{" + typeof(T).GetField(variaveis[j]).GetValue(lista[i]).ToString() + "}"; }
                }
                _temp += "|";
            }
        }
        #endregion
        if (more)
        {
            _temp += "|";
        }
        return _temp;
    }
    public static string SalvarVector3(Vector3 vetor, string name)
    {
        string _temp = "#<"+name+">[x]{"+vetor.x+"}[y]{"+vetor.y+"}[z]{"+vetor.z+"}/#";
        return _temp;
    }
    public static string SalvarVector3(Vector3 vetor, string name, bool more)
    {
        string _temp = "#<" + name + ">[x]{" + vetor.x + "}[y]{" + vetor.y + "}[z]{" + vetor.z + "}/#";
        if (more)
        {
            _temp += "|";
        }
        return _temp;
    }
    public static string SalvarVariavel( object var, string name)
    {
        
        return "[" + name + "]{" + var + "}";
    }
    public static string SalvarVariavel( object var, string name, bool more)
    {
        string _temp = "[" + name + "]{" + var + "}";
        if(more)
        {
            _temp += "|";
        }
        return _temp;
    }
    
  
   
    
   
}
