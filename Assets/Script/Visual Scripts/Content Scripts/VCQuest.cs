using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[AddComponentMenu("Scripts/VisualScripts/Visual Contents/Quests")]
public class VCQuest : Selectable
{

    Text Descricao;
    public Text Nome;
    public Image Icone;
    public Sprite[] icones = new Sprite[2];
    public int Indice = -1;
    MyQuest Q;
    string texto;
    // Use this for initialization
    protected override void Start()
    {
        if (!Application.isPlaying) return;
        Q = GameObject.FindGameObjectWithTag("Player").GetComponent<MyQuest>();
        //Content > Area com mascara >Box > Area_Inventario
        Descricao = gameObject.transform.parent.transform.parent.transform.parent.Find("Descricao").GetComponent<Text>();
        texto = "Objetivo: \n" + Q.QuestInformation(Indice).Descricao + "\n\t\tRecompensa: " + Q.QuestInformation(Indice).Recompensa();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying) return;
        if (Indice != -1)
        {
            if (Q != null)
            {
                if (Q.QuestInformation(Indice).Tipo == TiposDeQuests.Principal)
                {
                    Icone.sprite = icones[0];
                }
                if (Q.QuestInformation(Indice).Tipo == TiposDeQuests.Repetitiva)
                {
                    Icone.sprite = icones[1];
                }
                Nome.text = Q.QuestInformation(Indice).Nome + " (" + (Q.QuestInformation(Indice).IsComplete ? "OK)" : " )");
                texto = "Objetivo: \n" + Q.QuestInformation(Indice).Descricao + "\n\t\tRecompensa: " + Q.QuestInformation(Indice).Recompensa();
            }
            else
            {
                Debug.LogError("Inventário é nulo, você esqueceu de atribuir valor");
            }
        }
        else
        {
            if (Descricao.text == texto)
            {
                Descricao.text = "Objetivo:\nRecomepensa:";
            }
            Destroy(gameObject);
        }
    }
    public void MouseDown()
    {
        Descricao.text = "Objetivo: \n" + Q.QuestInformation(Indice).Descricao + "\n\t\tRecompensa: " + Q.QuestInformation(Indice).Recompensa();
    }


}
