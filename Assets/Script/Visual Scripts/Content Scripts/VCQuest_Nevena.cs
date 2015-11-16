using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class VCQuest_Nevena : Selectable,IDeselectHandler,ISelectHandler {

   
    public Text Nome;//Nome da Quest
    public Image Ico;//Imagem do icone
    public int Indice = -1; // Indice da Quest.
    public bool Selecionado = false; //Para saber se está selecionado ou não.
    Nevena_NPC npc; // Referencia do NPC
    [Header("Icones de Quests")]
    public Sprite[] icones = new Sprite[2];//Sprites de quest.

	protected override void Start()
    {
        base.Start();
        //Preenchendo as informações
        if (!Application.isPlaying) return;
        try
        {
            npc = GameObject.Find("Nevena").GetComponent<Nevena_NPC>();
            Nome.text = npc.GetQuestInfo(Indice).Nome;
            if (npc.GetQuestInfo(Indice).Tipo == TiposDeQuests.Principal)
            {
                Ico.sprite = icones[0];
            }
            else if (npc.GetQuestInfo(Indice).Tipo == TiposDeQuests.Repetitiva)
            {
                Ico.sprite = icones[1];
            }
        }catch
        {
            Debug.LogError("Não foi possivél inicializar o Quest Content da NPC Nevena");
        }

    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        Selecionado = true; //Eu selecionei a quest
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        Invoke("Deselecionar", 2f);//Para que não deselecione no momento que eu clicar em outra coisa, eu espero 2 segundos para deselecionar.
    }
    void Deselecionar()
    {
        Selecionado = false; //Aqui eu deseleciono. 
    }
}
