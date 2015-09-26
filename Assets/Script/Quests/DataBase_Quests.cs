using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class DataBase_Quests : MonoBehaviour
{

    public static List<Quest> Lista = new List<Quest>();
    // Use this for initialization
    public DataBase_Quests()
    {
        if(Game.current != null)
        {
            if(Game.current.Quests.Count > 0)
            {
                Lista = Game.current.Quests;
            }
        }
    }
    static void SetQuest()
    {

    }
    public static List<Quest> OrganizarLista()
    {
        for (int i = 0; i < 3; i++)
        {
            Lista.Add(new Quest());
            Lista[i].GerarQuestAleatória();
        }
        //Não mudar
        return Lista;
    }
    public Quest ChamarQuest(string nome)
    {
        List<Quest> Lista = OrganizarLista();
        return Lista.Find(X => X.Nome == nome);
    }
    public static List<Quest> GetList()
    {
        return Lista;
    }
    /* Protocolo de Save
     * @ - Começa
     * [Vareiavel]
     * {Valor}
     * | - Separa
     * !<Nome> - Array Inicio
     * /! - ArrayFim
     * /@ - Termina
     * @[Nome](Lucas)[Idade](20)||[Nome](Fulano)[Idade](19)/@
     */
    public static void Save(int indice)
    {
        string _temp = string.Empty;
        for (int i = 0; i < Lista.Count; i++)
        {
            if (i == 0)
            {
                _temp += "@[Nome]{" + Lista[i].Nome + "}[Descricao]{" + Lista[i].Descricao + "}[Objetivo]{" + Lista[i].Objetivo + "}[Repete]{" + Lista[i].Repete + "}";
                _temp += SaveArray(i);
                _temp += "|";
            }
            else if (i == Lista.Count - 1)
            {
                _temp += "[Nome]{" + Lista[i].Nome + "}[Descricao]{" + Lista[i].Descricao + "}[Objetivo]{" + Lista[i].Objetivo + "}[Repete]{" + Lista[i].Repete + "}";
                _temp += SaveArray(i);
                _temp += "/@";
            }
            else
            {
                _temp += "[Nome]{" + Lista[i].Nome + "}[Descricao]{" + Lista[i].Descricao + "}[Objetivo]{" + Lista[i].Objetivo + "}[Repete]{" + Lista[i].Repete + "}";
                _temp += SaveArray(i);
                _temp += "|";
            }

        }
        print(_temp);
        PlayerPrefs.SetString("Quests" + indice, _temp);
        print("SaveQuests OK");
    }
    static string SaveArray(int i)
    {
        string _temp = string.Empty;
        #region Objetivos
        if (Lista[i].Objetivo == ObjetivesType.Coleta)
        {
            for (int j = 0; j < Lista[i].ItensColetaveis.Length; j++)
            {
                if (Lista[i].ItensColetaveis.Length == 1)
                {
                    _temp += ("!<Itens>[" + j + "]{" + Lista[i].ItensColetaveis[j] + "}/!");
                }
                else if (j == 0)
                {
                    _temp += ("!<Itens>[" + j + "]{" + Lista[i].ItensColetaveis[j] + "}");
                }
                else if (j == Lista[i].ItensColetaveis.Length - 1)
                {
                    _temp += ("[" + j + "]{" + Lista[i].ItensColetaveis[j] + "}/!");
                }
                else
                {
                    _temp += ("[" + j + "]{" + Lista[i].ItensColetaveis[j] + "}");
                }
            }
        }
        else
        {
            for (int j = 0; j < Lista[i].Monstros.Length; j++)
            {
                if (Lista[i].Monstros.Length == 1)
                {
                    _temp += ("!<Monstros>[" + j + "]{" + Lista[i].Monstros[j] + "}/!");
                }
                else if (j == 0)
                {
                    _temp += ("!<Monstros>[" + j + "]{" + Lista[i].Monstros[j] + "}");
                }
                else if (j == Lista[i].Monstros.Length - 1)
                {
                    _temp += ("[" + j + "]{" + Lista[i].Monstros[j] + "}/!");
                }
                else
                {
                    _temp += ("[" + j + "]{" + Lista[i].Monstros[j] + "}");
                }
            }
        }
        for (int j = 0; j < Lista[i].Quantidades.Length; j++)
        {
            if (Lista[i].Quantidades.Length == 1)
            {
                _temp += ("!<Quantidades>[" + j + "]{" + Lista[i].Quantidades[j] + "}/!");
            }
            else if (j == 0)
            {
                _temp += ("!<Quantidades>[" + j + "]{" + Lista[i].Quantidades[j] + "}");
            }
            else if (j == Lista[i].Quantidades.Length - 1)
            {
                _temp += ("[" + j + "]{" + Lista[i].Quantidades[j] + "}/!");
            }
            else
            {
                _temp += ("[" + j + "]{" + Lista[i].Quantidades[j] + "}");
            }
        }
        #endregion
        return _temp;
    }
    public static bool Load(int indice)
    {
        if (PlayerPrefs.HasKey("Quests" + indice))
        {
            List<Quest> _Lista = new List<Quest>();
            string _temp = PlayerPrefs.GetString("Quests" + indice);
            string variavel = string.Empty;
            string valor = string.Empty;
            string array = string.Empty;
            bool bool_variavel = false;
            bool bool_valor = false;
            bool bool_array = false;
            bool bool_array_nome = false;
            int ListaIndice = -1;
            int arrayindice = -1;
            //------------------------
            #region Load
            for (int i = 0; i < _temp.Length; i++)
            {
                if (_temp[i] == '@' || _temp[i] == '|')
                {
                    _Lista.Add(new Quest());
                    ListaIndice++;
                }

                if (_temp[i] == ']')
                {
                    bool_variavel = false;
                }

                if (_temp[i] == '}')
                {
                    bool_valor = false;
                    #region Registrando
                    if (!bool_array)
                    {
                        if (variavel == "Nome") { _Lista[ListaIndice].Nome = valor; }
                        if (variavel == "Descricao") { _Lista[ListaIndice].Descricao = valor; }
                        if (variavel == "Objetivo") { _Lista[ListaIndice].Objetivo = (ObjetivesType)System.Enum.Parse(typeof(ObjetivesType), valor); }
                        if (variavel == "Repete") { _Lista[ListaIndice].Repete = System.Convert.ToBoolean(valor); }
                    }
                    else
                    {
                        if (array == "Itens") { arrayindice++; _Lista[ListaIndice].ItensColetaveis[arrayindice] = ResourceFind.FindItem(valor); }
                        if (array == "Monstros") { arrayindice++; _Lista[ListaIndice].Monstros[arrayindice] = ResourceFind.FindMonster(valor); }
                        if (array == "Quantidades") { arrayindice++; _Lista[ListaIndice].Quantidades[arrayindice] = System.Convert.ToInt32(valor); }
                    }
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
                    arrayindice = -1;
                    array = string.Empty;
                }
                //Detecta o termino do nome do array
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
               
                //Detecta o termino de um array
                if (_temp[i] == '/' && _temp[i + 1] == '!')
                {
                    bool_array = false;
                    arrayindice = -1;
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
            Lista = _Lista;
            print("LoadQuests OK");
            return true;
        }
        print("LoadQuests Failed");
        return false;
    }
    public static void Delete(int indice)
    {
        if (PlayerPrefs.HasKey("Quests" + indice))
        {
            PlayerPrefs.DeleteKey("Quests" + indice);
            print("DeletouQuest" + indice);
            return;
        }
        print("Não existe Quest" + indice);

    }

}
