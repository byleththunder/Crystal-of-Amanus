using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class VHabilidades : MonoBehaviour {

    public Transform Area_My;
    public Button Prefab;
    List<Selectable> Selecionaveis = new List<Selectable>();
    public Image[] Slots = new Image[4];
    Character Personagem;
    CharacterSkills SK;
	// Use this for initialization
	void Start () {
        Personagem = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        SK = Personagem.GetComponent<CharacterSkills>();
        
	}
	
	// Update is called once per frame
	void LateUpdate () {
        AlocarTexto();
	    for(int i = 0; i < Slots.Length; i ++)
        {
            if(SK.Slots[i] == null)
            {
                Slots[i].color = new Color(1, 1, 1, 0);
            }else
            {
                Slots[i].color = new Color(1, 1, 1, 1);
            }
        }
	}
    void AlocarTexto()
    {
        var quantidade = SK.SkillTotal();
        for (int i = 0; i < quantidade; i++)
        {
            if (i + 1 > Selecionaveis.Count)
            {
                Selecionaveis.Add(Instantiate(Prefab));
                Selecionaveis[i].transform.SetParent(Area_My, false);
                Selecionaveis[i].GetComponentInChildren<Text>().text = SK.SkillInformation(i).name;
            }
            if (Selecionaveis[i] == null)
            {
                Selecionaveis.RemoveAt(i);
            }
        }
    }
    public void MoverSkill(int i)
    {
        
        for(int j = 0; j<Selecionaveis.Count;j++)
        {
            if (Selecionaveis[j].GetComponent<VCHabilidade>().IsSeletec)
            {
                Slots[i].sprite = SK.SkillInformation(j).NameImg;
                SK.AddSkillToSlot(i, SK.SkillInformation(j));
                Selecionaveis[j].GetComponent<VCHabilidade>().IsSeletec = false;
                print("Moveu = "+ j);
                break;
            }
        }
    }
    
}
