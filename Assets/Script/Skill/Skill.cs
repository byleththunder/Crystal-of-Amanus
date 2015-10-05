using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum SkillTarget { Self, Other };
public class Skill : MonoBehaviour {
    //Propriedades
    public string Nome;
    public string Descricao;
    public Sprite Img;
    public Sprite NameImg;
    public SkillTarget Alvo;
    public float Dano;
    public float CoolDown;
    public bool OnCoolDown;
    //Métodos
    public virtual void UsarSkill(Target target)
    {

    }
    public virtual void ResetCoolDown()
    {

    }
    
}
