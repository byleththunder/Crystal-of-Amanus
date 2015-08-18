using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuestContent : MonoBehaviour {
	public Quest missao;
	public Image Painel,Imagem;
	public Text Txt_Nome,Txt_Tipo;
	// Use this for initialization
	void Start () {
		if (missao != null) {
			Txt_Nome.text = missao.Nome;
			Txt_Tipo.text = missao.Tipo.ToString();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void ChangeColor(bool select)
	{
        if (!missao.GetQuest)
        {
            if (select)
            {
                Painel.color = new Color(Color.red.r, Color.red.g, Color.red.b, 100.0f);
            }
            else
            {
                Painel.color = new Color(Color.red.r, Color.red.g, Color.red.b, 0f);
            }
        }
        else
        {
            if (select)
            {
                Painel.color = new Color(Color.green.r, Color.green.g, Color.green.b, 100.0f);
            }
            else
            {
                Painel.color = new Color(Color.blue.r, Color.blue.g, Color.blue.b, 100.0f);
            }
        }
	}
}
