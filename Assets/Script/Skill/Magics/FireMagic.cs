using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Scripts/Skill Script/Skills/Amanus Fire")]
public class FireMagic : Skill
{
    ///<summary "Como Funciona?">
    ///Quando eu clico para usar a habilidade, eu verifico para onde o personagem está olhando e emito uma particula na direção certa.
    ///</summary>
	
	//Variaveis
	public GameObject Pers;
    public ParticleSystem Particle;
	public Target Personagem;
	//Métodos
    void Start()
    {
        Nome = "AmanusFire";
        Descricao = "Uma bola de fogo que segue pelo mapa";
        Alvo = SkillTarget.Other;
        Pers = GameObject.FindGameObjectWithTag("Player");
        Personagem = ((Target)Pers.GetComponent(typeof(Target)));
        Dano = (Dano * 100) / Personagem.AtaqueAtual;
    }
    public override void UsarSkill(Target target)
	{
        
		if (!OnCoolDown) {
			if (Personagem.AmanusAtual >= 10) {
                Particle.transform.eulerAngles = ConvertVisionToEuler(Personagem.visao);
                Particle.Emit(1);
				if (Personagem != null) {
				
					Personagem.HealOrDamage (0, 10);
				}
				OnCoolDown = true;
				Invoke ("ResetCoolDown", CoolDown);
			}
			} 

	}
	Vector3 ConvertVisionToEuler(TargetVision v)
	{
		Vector3 _temp = Vector3.zero;
		switch (v) {
		case TargetVision.Back:
			_temp = new Vector3(0,0,0);
			break;
		case TargetVision.Front:
			_temp = new Vector3(0,180,0);
			break;
		case TargetVision.Left:
			_temp = new Vector3(0,270,0);
			break;
		case TargetVision.Right:
			_temp = new Vector3(0,90,0);
			break;
		}
		return _temp;
	}
    public override void ResetCoolDown()
	{
		OnCoolDown = false;
	}
    
}
