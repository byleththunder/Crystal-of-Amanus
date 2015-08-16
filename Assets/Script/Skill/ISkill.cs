using UnityEngine;
using System.Collections;

public enum SkillTarget { Self, Other };
public interface ISkill {
    //Propriedades
    string Nome { get;}
    string Descricao { get;}
    SkillTarget Alvo { get;}
    float Dano { get; }
    float CoolDown { get; }
    bool OnCoolDown { get; }
    //Métodos
    void UsarSkill(Target target);
    void ResetCoolDown();
    
}
