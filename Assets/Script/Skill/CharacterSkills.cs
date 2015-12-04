using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Scripts/Skill Script/Character Skill")]
public class CharacterSkills : MonoBehaviour
{

    public List<Skill> SkillsAvailable = new List<Skill>();
    public Skill[] Slots = new Skill[4];


    void Start()
    {
       if(Game.current!=null)
       {
           Load(Game.current.Skills);
       }
    }
    void Save()
    {
        if (Game.current != null)
        {
            for (int i = 0; i > Slots.Length; i++)
            {
                Game.current.Skills[i] = Slots[i].Nome;
            }
        }
    }
    void Load(string[] sk)
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (sk[i] != string.Empty)
            {
                Slots[i] = SkillsAvailable.Find(x => x.Nome == sk[i]);
            }
        }
    }

    public Skill SkillInformation(int indice)
    {
        return SkillsAvailable[indice];
    }
    public int SkillTotal()
    {
        return SkillsAvailable.Count;
    }
    public void AddSkillToSlot(int indice, Skill _SK)
    {
        Slots[indice] = _SK;
        Save();
    }
    public void LearnNewSkill(Skill _SK)
    {
        SkillsAvailable.Add(_SK);
        Save();
    }
}
