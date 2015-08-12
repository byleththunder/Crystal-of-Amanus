using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class QuestList : MonoBehaviour, ISelectable
{
    public Text Txt_Nome, Txt_Tipo, Txt_Objetivo, Txt_Recompensa, Txt_Descricao;
    public QuestContent QuestPrefab;
    List<QuestContent> Quests;
    List<Quest> ListasDeQuests;
    int indice = 0;
    int indicePassado = -1;
    public GameObject Op;
    bool selectDois = false;
    public GameObject BackScreen, QuestScreen;
    public ScrollRect scrollV;
    float valor = 0;
    public CharacterWorld personagem;
    bool TentarCompletar = false;
    int CompleteQuestIndice = -1;
    // Use this for initialization
    void Start()
    {
        ListasDeQuests = DataBase_Quests.OrganizarLista();
        SetList();
        scrollV.verticalScrollbar.value = 1;
       

    }

    // Update is called once per frame
    void Update()
    {
        if (!Op.active)
        {

            NavegarLista();
        }

    }

    void SetList()
    {
       
        Quests = new List<QuestContent>();
        RectTransform rct = gameObject.GetComponent<RectTransform>();
        rct.sizeDelta = new Vector2(rct.sizeDelta.x, ListasDeQuests.Count * 50);
        for (int i = 0; i < ListasDeQuests.Count; i++)
        {
            QuestContent cont = Instantiate(QuestPrefab) as QuestContent;

            if (cont != null)
            {
                Quests.Add(cont);
                cont.transform.SetParent(gameObject.transform, false);
                cont.name = ListasDeQuests[i].Nome;
                RectTransform rctt = QuestPrefab.GetComponent<RectTransform>();

                cont.transform.localPosition = new Vector3(0, (rctt.position.y + 48) - (48 * i), 0);
                cont.transform.localScale = new Vector3(1, 1, 1);
                cont.missao = ListasDeQuests[i];

            }
        }
        valor = ((float)(ListasDeQuests.Count + 1) / 10f) / 2f;


    }
    void ReOrganizarLista()
    {
        
        RectTransform rct = gameObject.GetComponent<RectTransform>();
        rct.sizeDelta = new Vector2(rct.sizeDelta.x, ListasDeQuests.Count * 50);
        for (int i = 0; i < Quests.Count; i++)
        {
            RectTransform rctt = QuestPrefab.GetComponent<RectTransform>();
            Quests[i].transform.localPosition = new Vector3(0, (rctt.position.y + 48) - (48 * i), 0);
            Quests[i].transform.localScale =  new Vector3(1, 1, 1);
        }
    }
    void NavegarLista()
    {
        if (indice > -1 && indice < Quests.Count)
        {
            if (Quests[indice].Painel != null)
            {
                Quests[indice].ChangeColor(true);
            }


        }
        if (indicePassado > -1 && indicePassado < Quests.Count)
        {
            Quests[indicePassado].ChangeColor(false);
        }

        if (Input.GetButtonDown("Action"))
        {
            if (!selectDois)
            {
                if (Quests[indice].missao.GetQuest)
                {
                    Txt_Nome.text = "Nome: " + Quests[indice].missao.Nome + " (GET)";
                }
                else if (Quests[indice].missao.Complete)
                {
                    Txt_Nome.text = "Nome: " + Quests[indice].missao.Nome + " (OK)";
                }
                else
                {
                    Txt_Nome.text = "Nome: " + Quests[indice].missao.Nome;
                }
                Txt_Tipo.text = "Tipo: " + Quests[indice].missao.Tipo.ToString();
                Txt_Descricao.text = "Descriçao: \n" + Quests[indice].missao.Texto;
                Txt_Objetivo.text = "Objetivo: \n" + Quests[indice].missao.c;
                Txt_Recompensa.text = "Recompensa: \n" + Quests[indice].missao.Recompensa();
                selectDois = true;
            }
            else
            //filtrando ação
            {
                Input.GetButtonDown("Action");
                Op.SetActive(true);
                selectDois = false;
               // personagem.ChecarQuestColeta();

            }

        }
        if (Input.GetButtonDown("Defense"))
        {
            BackScreen.SetActive(true);
            QuestScreen.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && indice < Quests.Count - 1)
        {
            //print(scrollV.verticalScrollbar.value.ToString());
            scrollV.verticalScrollbar.value -= valor;
            Mathf.Clamp(scrollV.verticalScrollbar.value, 0, 1);
            Mathf.Clamp(scrollV.verticalScrollbar.value, 0, 1.2f);
            if (indicePassado != null)
            {
                indicePassado = indice;
            }
            indice++;
            selectDois = false;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && indice - 1 > -1)
        {
            //print(scrollV.verticalScrollbar.value.ToString());

            scrollV.verticalScrollbar.value += valor;
            Mathf.Clamp(scrollV.verticalScrollbar.value, 0, 1);
            if (indicePassado != null)
            {
                indicePassado = indice;
            }
            indice--;
            selectDois = false;
        }
    }
    void PegarQuest()
    {
        for (int i = 0; i < personagem.MinhasMissoes.Count; i++)
        {
            if (personagem.MinhasMissoes[i] == Quests[indice].missao)
            {
                CompleteQuestIndice = i;
                TentarCompletar = true;
                return;
            }
        }
        personagem.MinhasMissoes.Add(Quests[indice].missao);
        Quests[indice].missao.GetQuest = true;
        Txt_Nome.text = "Nome: " + Quests[indice].missao.Nome + " (GET)";
    }
    public void ConfirmButton(string buttonName)
    {

        if (buttonName == "Pegar Quest")
        {
            if (personagem.MinhasMissoes.Count > 0)
            {
                PegarQuest();

            }
            else
            {
                personagem.MinhasMissoes.Add(Quests[indice].missao);
                Quests[indice].missao.GetQuest = true;
                Txt_Nome.text = "Nome: " + Quests[indice].missao.Nome + " (GET)";
            }
            return;
        }
        if (buttonName == "Concluir Quest")
        {
            for (int i = 0; i < personagem.MinhasMissoes.Count; i++)
            {
                if (personagem.MinhasMissoes[i] == Quests[indice].missao)
                {
                    CompleteQuestIndice = i;
                    break;
                }
            }
            if (CompleteQuestIndice >= 0)
            {
                if (personagem.MinhasMissoes[CompleteQuestIndice].Complete)
                {
                    print("Completou");
                    Quests[indice].missao.Complete = true;
                    personagem.MinhasMissoes.RemoveAt(CompleteQuestIndice);
                    Destroy(Quests[indice].gameObject);
                    Quests.RemoveAt(indice);
                    ReOrganizarLista();

                }
                else
                {
                    print("Não Completou");
                }
            }
            return;
        }
    }
    
}
