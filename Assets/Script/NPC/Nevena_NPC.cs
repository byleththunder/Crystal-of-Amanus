using UnityEngine;
using System.Collections;
using TeamUtility.IO;
using System.Collections.Generic;
using UnityEngine.UI;

public class Nevena_NPC : MonoBehaviour {
    [Header("Painel com as escolhas (Quest ou Pagar Divida)")]
    public GameObject Selecao_painel;
    [Space(1f)]
    [Header("Box aonde as quests serão alocadas")]
    public GameObject Quest_Box;
    [Header("Prefab de Quest")]
    public VCQuest_Nevena Prefab;
    [Space(1f)]
    [Header("Texto da Divida")]
    public Text Divida_txt;
    [Header("Local que você escreve a quantidade de dinheiro")]
    public InputField Field;

    protected Character Personagem; //Eu pego a referencia do personagem para alterar o estado dele.
    protected List<Quest> ListaQuest = new List<Quest>(); //Lista que armazena todas as quests.
    protected List<VCQuest_Nevena> InstanceQuest = new List<VCQuest_Nevena>();//Gerencia as instacias
	// Use this for initialization
	void Start () {
        MessageBox.PrefabPath = "Resource/CaixaDeTexto";
        Personagem = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(GameStates.IsAWindowOpen)
        {
            if(ListaQuest.Count < 10)
            {
                PreencherQuestList();
            }
            Field.characterLimit = Personagem.Gold.ToString().Length;
        }
	}

    void OntriggerStay(Collider col)
    {
        if (col.tag == "Player")//O jogador está dentro do collider?
        {
            if (InputManager.GetButtonDown("Action") && !GameStates.IsAWindowOpen)//O jogador apertou o botão de ação?
            {
                Dialogo();
                GameStates.IsAWindowOpen = true;
                col.GetComponent<Character>().EstadoDoJogador = GameStates.CharacterState.DontMove;
                Selecao_painel.SetActive(true);
            }
            if(Input.GetButtonDown("Magic") && GameStates.IsAWindowOpen)
            {
                if(Selecao_painel.activeInHierarchy)
                {
                    GameStates.IsAWindowOpen = false;
                    Selecao_painel.SetActive(false);
                    col.GetComponent<Character>().EstadoDoJogador = GameStates.CharacterState.DontMove;
                }
            }
        }
    }
    
    void Dialogo()
    {
        MessageBox.Instance.WriteMessage("O que o trás aqui ?","Nevena");
    }

    public void PreencherQuestList()
    {
        ListaQuest = DataBase_Quests.PreencherLista(ListaQuest);
        for(int i = InstanceQuest.Count-1; i <ListaQuest.Count ; i++)
        {
            InstanceQuest.Add(Instantiate(Prefab));
            InstanceQuest[i].transform.SetParent(Selecao_painel.transform);
            InstanceQuest[i].Indice = i;

        }
    }
    
    public Quest GetQuestInfo(int indice)
    {
        return ListaQuest[indice];
    }

    public void ReturnState()
    {
        GameStates.IsAWindowOpen = false;
        Personagem.EstadoDoJogador = GameStates.CharacterState.Playing;
    }

    //Divida
    public void Pay()
    {
        Personagem.Gold -= int.Parse(Field.text);
        if(Personagem.Gold < 0)
        {
            Personagem.Gold = 0;
        }
        Personagem.Divida -= long.Parse(Field.text);
        
    }

}
