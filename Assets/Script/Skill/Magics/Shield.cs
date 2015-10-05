using UnityEngine;
using System.Collections;

public class Shield : Skill {
    //Propriedades
   
    //Variaveis
    float Timer = 0;
    public GameObject Escudos;
    bool On = false;
    public Character Jogador;
   
    //----------
	// Use this for initialization
	void Start () {
        Nome = "Escudos";
        Descricao = "Invoca um escudos";
        Alvo = SkillTarget.Other;
        
        CoolDown = 5f;
        Jogador = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        Escudos = GameObject.FindGameObjectWithTag("Reflect");
        if(Escudos != null)
        {
            Escudos.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (Escudos != null)
        {
            if (!Escudos.activeInHierarchy && On)
            {
                Invoke("ResetCoolDown", CoolDown);
                On = false;
                OnCoolDown = true;
            }
            if (OnCoolDown)
            {
                Timer += Time.deltaTime;
            }
            else
            {
                SetSheildPosition();
            }
        }
	}
    public override void UsarSkill(Target target)
    {
        if (!OnCoolDown)
        {
            if(!Escudos.activeInHierarchy)
            {
                Escudos.SetActive(true);
                On = true;
                Jogador.EstadoDoJogador = GameStates.CharacterState.Defense;
            }else
            {
                Escudos.SetActive(false);
                Jogador.EstadoDoJogador = GameStates.CharacterState.Playing;
                OnCoolDown = true;
            }
            if (!Escudos.activeInHierarchy && On )
            {
                Invoke("ResetCoolDown", CoolDown);
            }
        }
        else
        {
            print("Cooldown: " + (int)Timer);
        }
    }
    void SetSheildPosition()
    {
        switch(Jogador.visao)
        {
            case TargetVision.Back:
                Escudos.transform.localPosition = new Vector3(0, 0, 1);
                Escudos.transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            case TargetVision.Front:
                Escudos.transform.localPosition = new Vector3(0, 0, -1);
                Escudos.transform.localEulerAngles = new Vector3(0, 180, 0);
                break;
            case TargetVision.Left:
                Escudos.transform.localPosition = new Vector3(-1, 0, 0);
                Escudos.transform.localEulerAngles = new Vector3(0, -90, 0);
                break;
            case TargetVision.Right:
                Escudos.transform.localPosition = new Vector3(1, 0, 0);
                Escudos.transform.localEulerAngles = new Vector3(0, 90, 0);
                break;
        }
    }
    public override void ResetCoolDown()
    {
        OnCoolDown = false;
        Timer = 0;
        On = false;
    }
}
