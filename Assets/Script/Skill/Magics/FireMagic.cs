using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireMagic : Skill
{

	
	//Variaveis
	public GameObject Pers;
    public ParticleSystem Particle;
    List<Fire> FirePool = new List<Fire>();
	float Timer = 0;
	public Target Personagem;
    int ID = 0;
	// Use this for initialization
	void Awake ()
	{
	}
    void Start()
    {
        Nome = "AmanusFire";
        Descricao = "Uma bola de fogo que segue pelo mapa";
        Alvo = SkillTarget.Other;
        Pers = GameObject.FindGameObjectWithTag("Player");
        Personagem = ((Target)Pers.GetComponent(typeof(Target)));
        Dano = (Dano * 100) / Personagem.Ataque;
    }
	// Update is called once per frame
	void Update ()
	{
		if (OnCoolDown) {
			Timer += Time.deltaTime;
		}
	}
	//Métodos
    public override void UsarSkill(Target target)
	{
        
		if (!OnCoolDown) {
			if (Personagem.Amanus >= 10) {
                Particle.transform.eulerAngles = ConvertVisionToEuler(Personagem.visao);
                Particle.Emit(1);
				if (Personagem != null) {
				
					Personagem.HealOrDamage (0, 10);
				}
				OnCoolDown = true;
				Invoke ("ResetCoolDown", CoolDown);
			}
			} else {
				print ("Cooldown: " + (int)Timer);
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
		Timer = 0;
	}
    
}
