using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour,ISkill {
    //Propriedades
    public string Nome { get { return "Escudos"; } }
    public string Descricao { get { return "Invoca quatro escudos"; } }
    public SkillTarget Alvo { get { return SkillTarget.Other; } }
    public float Dano { get { return _dano; } }
    public float CoolDown { get { return 5f; } }
    public bool OnCoolDown { get { return IsOnCoolDown; } }
    //Variaveis
    bool IsOnCoolDown = false;
    float Timer = 0;
    float _dano = 0;
    public GameObject Escudos;
    bool On = false;
    public Character Jogador;
   
    //----------
	// Use this for initialization
	void Start () {
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
                IsOnCoolDown = true;
            }
            if (IsOnCoolDown)
            {
                Timer += Time.deltaTime;
            }
            else
            {
                SetSheildPosition();
            }
        }
	}
    public void UsarSkill(Target target)
    {
        if (!IsOnCoolDown)
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
                IsOnCoolDown = true;
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
                Escudos.transform.localPosition = new Vector3(0, 0, 2);
                Escudos.transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            case TargetVision.Front:
                Escudos.transform.localPosition = new Vector3(0, 0, -2);
                Escudos.transform.localEulerAngles = new Vector3(0, 180, 0);
                break;
            case TargetVision.Left:
                Escudos.transform.localPosition = new Vector3(-2, 0, 0);
                Escudos.transform.localEulerAngles = new Vector3(0, -90, 0);
                break;
            case TargetVision.Right:
                Escudos.transform.localPosition = new Vector3(2, 0, 0);
                Escudos.transform.localEulerAngles = new Vector3(0, 90, 0);
                break;
        }
    }
    public void ResetCoolDown()
    {
        IsOnCoolDown = false;
        Timer = 0;
        On = false;
    }
}
