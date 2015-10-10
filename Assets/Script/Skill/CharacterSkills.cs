using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Scripts/Skill Script/Character Skill")]
public class CharacterSkills : MonoBehaviour {

    public List<Skill> SkillsAvailable = new List<Skill>();
    public Skill[] Slots = new Skill[4];
	
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
    }
    public void LearnNewSkill(Skill _SK)
    {
        SkillsAvailable.Add(_SK);
    }
}
