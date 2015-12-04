using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Nevena_NPC : MonoBehaviour
{
    [Header("Painel com as escolhas (Quest ou Pagar Divida)")]
    public GameObject Selecao_painel;
    [Space(1f)]
    [Header("Box aonde as quests serão alocadas")]
    public GameObject Quest_Box;
    public GameObject Quest_Screen;
    public Text Descricao;
    [Header("Prefab de Quest")]
    public VCQuest_Nevena Prefab;
    [Space(1f)]
    [Header("Texto da Divida")]
    public Text Divida_txt;
    [Header("Local que você escreve a quantidade de dinheiro")]
    public InputField Field;
    bool Falou = false;
    GameObject Box;
    public EventSystem evento;
    public GameObject Button;
    protected Character Personagem; //Eu pego a referencia do personagem para alterar o estado dele.
    protected List<Quest> ListaQuest = new List<Quest>(); //Lista que armazena todas as quests.
    protected List<VCQuest_Nevena> InstanceQuest = new List<VCQuest_Nevena>();//Gerencia as instacias
    // Use this for initialization
    void Start()
    {
        MessageBox.PrefabPath = "CaixaDeTexto";
        try
        {
            Personagem = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        }
        catch
        {
            Debug.LogWarning("Não achou o personagem no método Start");
        }
        Divida_txt.text = "Divida: " + Personagem.Divida + " Ar\nPratas: " + Personagem.Gold + " Ar";
        //if (Game.current != null)
        //{
        //    if (Game.current.Nevena.Count > 0)
        //        ListaQuest = Game.current.Nevena;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (Personagem == null)
        {
            Personagem = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        }
        if (Quest_Screen.activeInHierarchy)
        {
            if (ListaQuest.Count < 10)
            {
                PreencherQuestList();
            }
            Field.characterLimit = Personagem.Gold.ToString().Length;
        }
        if (Box != null)
        {
            if (!Box.activeInHierarchy && Falou)
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
                evento.SetSelectedGameObject(Button);
            }
            if (Input.GetButtonDown("Magic") && GameStates.IsAWindowOpen)
            {
                if (Selecao_painel.activeInHierarchy)
                {
                    GameStates.IsAWindowOpen = false;
                    Selecao_painel.SetActive(false);
                    Personagem.EstadoDoJogador = GameStates.CharacterState.Playing;
                }
            }
        }
    }

    void Dialogo()
    {
        MessageBox.Instance.WriteMessage("O que o trás aqui ?", "Nevena");
        Falou = true;
        Box = MessageBox.Instance.painel;
    }

    public void PreencherQuestList()
    {
        try
        {
            ListaQuest = DataBase_Quests.PreencherLista(ListaQuest);
            for (int i = InstanceQuest.Count; i < ListaQuest.Count; i++)
            {
                InstanceQuest.Add(Instantiate(Prefab));
                InstanceQuest[InstanceQuest.Count - 1].transform.SetParent(Quest_Box.transform);
                InstanceQuest[InstanceQuest.Count - 1].Indice = i;

            }
            //if (Game.current != null)
            //{
            //    Game.current.Nevena = ListaQuest;
            //}
            print("Completou");
        }
        catch
        {
            print("Falhou");
        }
    }
    public void MoverQuest()
    {

        for (int j = 0; j < InstanceQuest.Count; j++)
        {
            if (InstanceQuest[j].Selecionado)
            {
                Personagem.GetComponent<MyQuest>().AdicionarNovaQuest(ListaQuest[j]);
                break;
            }
        }
    }
    public void CompletarQuest()
    {
        Personagem.GetComponent<MyQuest>().ChecarQuestsCompletadas();
        List<Quest> _completas = Personagem.GetComponent<MyQuest>().TodasQuestCompletas();
        for (int i = 0; i < ListaQuest.Count; i++)
        {
            foreach (Quest q in _completas)
            {
                if (ListaQuest[i] == q)
                {
                    Personagem.Gold += ListaQuest[i].RecompensaEmDinheiro;
                    Personagem.GetComponent<MyQuest>().RemoverQuest(q);
                    ListaQuest.RemoveAt(i);
                    Destroy(InstanceQuest[i]);
                    InstanceQuest.RemoveAt(i);
                    print("Completou");
                }
            }
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
        if (Personagem.Gold < 0)
        {
            Personagem.Gold = 0;
        }
        Personagem.Divida -= long.Parse(Field.text);
        Divida_txt.text = "Divida: " + Personagem.Divida + " Ar\nPratas: " + Personagem.Gold + " Ar";
        Field.text = "";
    }

}
