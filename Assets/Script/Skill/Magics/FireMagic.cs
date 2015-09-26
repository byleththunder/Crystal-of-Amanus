using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireMagic : MonoBehaviour,ISkill
{

	//Propriedades
	public string Nome { get { return "AmanusFire"; } }
	public string Descricao { get { return "Uma bola de fogo que segue pelo mapa"; } }
	public SkillTarget Alvo { get { return SkillTarget.Other; } }
	public float Dano { get { return _dano; } }
	public float CoolDown { get { return 1; } }
	public bool OnCoolDown { get { return IsOnCoolDown; } }
	//Variaveis
	public GameObject Pers;
    public Fire FirePrefab;
    List<Fire> FirePool = new List<Fire>();
	bool IsOnCoolDown = false;
	float Timer = 0;
	float _dano = 0;
	public Target Personagem;
    int ID = 0;
	// Use this for initialization
	void Awake ()
	{
		DontDestroyOnLoad (gameObject);
		
       
	}
    void Start()
    {
        Pers = GameObject.FindGameObjectWithTag("Player");
        Personagem = ((Target)Pers.GetComponent(typeof(Target)));
    }
	// Update is called once per frame
	void Update ()
	{
		if (IsOnCoolDown) {
			Timer += Time.deltaTime;
		}
	}
	//Métodos
	public void UsarSkill (Target target)
	{

		if (!IsOnCoolDown) {
			if (Personagem.Amanus >= 10) {
                Fire municao = Pool();
                municao.Tag = "Monsters";
                municao.WhoShoot = "Player";
				municao.transform.eulerAngles = ConvertVisionToEuler(Personagem.visao);
                municao.transform.position = Pers.transform.position  + municao.transform.forward*2 ;
                float atk = ((Target)Pers.GetComponent(typeof(Target))).Ataque;
                municao.Damage = (int)(atk * 100) / 100;
                Pers.GetComponent<Eran2>().anim.SetTrigger("Magic");
                municao.gameObject.SetActive(true);
                
				if (Personagem != null) {
				
					Personagem.HealOrDamage (0, 10);
				}
				IsOnCoolDown = true;
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
	public void ResetCoolDown ()
	{
		IsOnCoolDown = false;
		Timer = 0;
	}
    public Fire[] MonsterFireAttack(int Quantity)
    {
        Fire[] FireBalls = new Fire[Quantity];
        for (int i = 0; i < FireBalls.Length; i++)
        {
            FireBalls[i] = Pool(FireBalls);
        }
        return FireBalls;
    }
    Fire Pool()
    {
        foreach(Fire fogo in FirePool)
        {
            if(!fogo.gameObject.activeInHierarchy)
            {
                return fogo;
            }
        }
        return ConjurarFogo();
    }
    Fire Pool(Fire[] comparativo)
    {
        foreach (Fire fogo in FirePool)
        {
            if (!fogo.gameObject.activeInHierarchy)
            {
                bool igualdade = false;
                foreach (Fire f in comparativo)
                {
                    if(f==fogo)
                    {
                        igualdade = true;
                    }
                }
                if(!igualdade)
                {
                    return fogo;
                }
            }
        }
        return ConjurarFogo();
    }
    Fire ConjurarFogo()
    {
        Fire temp = Instantiate(FirePrefab);
        temp.transform.SetParent(this.transform, false);
        temp.gameObject.SetActive(false);
        temp.name = "Fire " + ID;
        FirePool.Add(temp);
        ID++;
        return temp;
    }
}
