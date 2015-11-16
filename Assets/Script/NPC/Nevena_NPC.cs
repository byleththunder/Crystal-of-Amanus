using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Nevena_NPC : MonoBehaviour {
    [Header("Painel com as escolhas (Quest ou Pagar Divida)")]
    public GameObject Selecao_painel;
    [Space(1f)]
    [Header("Box aonde as quests serão alocadas")]
    public GameObject Quest_Box;
    public GameObject Quest_Screen;
    [Header("Prefab de Quest")]
    public VCQuest_Nevena Prefab;
    [Space(1f)]
    [Header("Texto da Divida")]
    public Text Divida_txt;
    [Header("Local que você escreve a quantidade de dinheiro")]
    public InputField Field;
    bool Falou = false;
    GameObject Box;
    protected Character Personagem; //Eu pego a referencia do personagem para alterar o estado dele.
    protected List<Quest> ListaQuest = new List<Quest>(); //Lista que armazena todas as quests.
    protected List<VCQuest_Nevena> InstanceQuest = new List<VCQuest_Nevena>();//Gerencia as instacias
	// Use this for initialization
	void Start () {
        MessageBox.PrefabPath = "CaixaDeTexto";
        try
        {
            Personagem = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        }catch
        {
            Debug.LogWarning("Não achou o personagem no método Start");
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(Personagem == null)
        {
            Personagem = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        }
	    if(Quest_Screen.activeInHierarchy)
        {
            if(ListaQuest.Count < 10)
            {
                PreencherQuestList();
            }
            Field.characterLimit = Personagem.Gold.ToString().Length;
        }
        if(Box != null)
        {
            if(!Box.activeInHierarchy && Falou)
            {
                GameStates.IsAWindowOpen = true;
                Personagem.EstadoDoJogador = GameStates.CharacterState.DontMove;
                Selecao_painel.SetActive(true);
                Falou = false;
            }
        }
	}

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")//O jogador está dentro do collider?
        {
            if (GameInput.GetKeyDown(InputsName.Action) && !GameStates.IsAWindowOpen)//O jogador apertou o botão de ação?
            {
                Dialogo();
                
            }
            if(Input.GetButtonDown("Magic") && GameStates.IsAWindowOpen)
            {
                if(Selecao_painel.activeInHierarchy)
                {
                    GameStates.IsAWindowOpen = false;
                    Selecao_painel.SetActive(false);
                    Personagem.EstadoDoJogador = GameStates.CharacterState.DontMove;
                }
            }
        }
    }
    
    void Dialogo()
    {
        MessageBox.Instance.WriteMessage("O que o trás aqui ?","Nevena");
        Falou = true;
        Box = MessageBox.Instance.painel;
    }

    public void PreencherQuestList()
    {
        ListaQuest = DataBase_Quests.PreencherLista(ListaQuest);
        for(int i = InstanceQuest.Count-1; i <ListaQuest.Count ; i++)
        {
            InstanceQuest.Add(Instantiate(Prefab));
            InstanceQuest[InstanceQuest.Count - 1].transform.SetParent(Quest_Box.transform);
            InstanceQuest[InstanceQuest.Count - 1].Indice = i;

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
