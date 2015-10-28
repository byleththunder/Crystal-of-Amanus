using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Skill Script/Skills/Escudo")]
public class Shield : Skill
{
    public GameObject Personagem;
    public GameObject Escudo;
    bool Ativado = false;
    // Inicializo as informações da habilidade
    void Start()
    {
        Nome = "Escudo";
        Descricao = "Uma bola de energia cobre o personagem";
        Alvo = SkillTarget.Other;
        Escudo = GameObject.FindGameObjectWithTag("Reflect");
        Personagem = GameObject.FindGameObjectWithTag("Player").transform.FindChild("Eran2Sprites").gameObject;
        Escudo.SetActive(false);
        CoolDown = 5f;
    }
    /// <summary "Como funciona?">
    /// Verifico se eu consegui achar o escudo (escudo != null).
    /// No UsarSkill, eu verifico se posso usar a habilidade, se sim, eu verifico se o escudo já está ativado, se estiver ativado eu desativo e coloco a habilidade em cooldown,
    /// se o escudo não estiver ativado, eu ativo e começo a habilidade.
    /// </summary>
   
    public override void UsarSkill(Target target)
    {
        if (!OnCoolDown)
        {
            Personagem.GetComponent<Animator>().SetTrigger("Magic");
            if (Escudo.activeInHierarchy == false)
            {
                Escudo.SetActive(true);
                Ativado = true;
            }
            else
            {
                Escudo.SetActive(false);
                Ativado = false;
            }
            if(Escudo.activeInHierarchy == false && Ativado)
            {
                Invoke("ResetCoolDown", CoolDown);
                OnCoolDown = true;
            }
        }
    }
    public override void ResetCoolDown()
    {
        OnCoolDown = false;
        Ativado = false;
    }
}
