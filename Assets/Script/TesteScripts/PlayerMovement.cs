using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour, ITarget
{
	//Da Interface
	//Propriedades
    public string Nome { get { return _Nome; } }
    public int Vida { get { return HP; } set { Vida = HP; } }
    public int VidaTotal { get { return VidaMax+LifeArmor; } }
    public int Amanus { get { return MP; } set { Amanus = MP; } }
    public int Ataque { get { return _Ataque+AtkArmor; } }
    public int AmanusTotal { get { return AmanusMax+AmanusArmor; } }
    public enum CameraDirection { Front, Back };
    public TargetVision visao { get; set; }
    public GameObject obj { get { return this.gameObject; } }
    public Item[] Equipamentos { get { return _Equipamentos; } set { Equipamentos = _Equipamentos; } }
	//Variaveis
	public int _Ataque, VidaMax,AmanusMax;
    public string _Nome;
    int AtkArmor, LifeArmor, AmanusArmor;
    int HP, MP;
	public ITarget Alvo;
    public TargetVision Direcao;
    public Item[] _Equipamentos = new Item[2];
    bool IsRecover = false;
	//Fim
	public float Speed = 500f;
	public float moveX = 0f;
	public float moveZ = 0f;
	public float jump = 0f;

	public bool notJump;
    public bool OnTheFloor;
	public bool left;
	public bool attack;
	public Animator anim;
    Rigidbody rgd;
    //Estados do Jogador
    public GameStates.CharacterState EstadoDoJogador = GameStates.CharacterState.Playing;
    //Posição de restart
    public Vector3 Ini;
    //Provisorio
    public GameObject Escudo;
	void Awake()
	{
		DontDestroyOnLoad (gameObject);
        HP = VidaTotal;
        MP = AmanusTotal;
        
	}
	void Start ()
	{
        
		rgd = GetComponent<Rigidbody>();
        rgd.freezeRotation = true;
       
        //Inicializando propriedades;
        HP = VidaTotal;
        MP = AmanusTotal;
		visao = TargetVision.Front;
        //Final
	}

	public Vector3 Flip ()
	{
		left = !left;
		Vector3 leftScale = transform.localScale;
		leftScale.x *= -1;
		transform.localScale = leftScale;
		return leftScale;
	}
	//Metodos da Interface
	public void StatsChange(int _vida, int _Amanus)
	{
		HP -= _vida;
		MP -= _Amanus;
		
	}
	//Fim
	void FixedUpdate ()
	{
        if(IsRecover)
        {
            if(Amanus < AmanusTotal)
            {
                MP++;
            }else
            {
                IsRecover = false;
            }
        }
        if (Application.loadedLevel == 0)
        {
            Destroy(gameObject);
        }
        if (Application.loadedLevelName == "GameOver")
        {
            if(!Escudo.activeInHierarchy)
            {
                Escudo.SetActive(true);
            }
        }
        
        Direcao = visao;
        if (EstadoDoJogador != GameStates.CharacterState.DontMove)
        {
            if(Jump())
            {
                return;
            }
            if (!IsInvoking("CheckRecoverAmanusAuto"))
            {
                Invoke("CheckRecoverAmanusAuto", 10f);
            }
            if (EstadoDoJogador != GameStates.CharacterState.Defense)
            {
                Attack();
            }
            
            if (!attack)
            {

                Movement();
                rgd.velocity = new Vector3(moveX * Speed, rgd.velocity.y, moveZ * Speed);
                moveX = Input.GetAxis("Horizontal");
                moveZ = Input.GetAxis("Vertical");

            }
            
        }
	}

	public void Movement ()
	{
		if (moveX < 0 && !left) {
			Flip ();
		}
		else if ((moveX > 0) && left) {
			Flip ();
		}
		
		if (moveZ == 0 && moveX == 0)
			anim.SetTrigger ("Idle");
		
		if (moveZ < 0 && Mathf.Abs (moveZ) > Mathf.Abs (moveX)) {
			anim.SetTrigger ("Down");
			visao = TargetVision.Front;
		}
		if ((moveX < 0 || moveX > 0) && (Mathf.Abs (moveZ) < Mathf.Abs (moveX) || Mathf.Abs (moveZ) == Mathf.Abs (moveX))) {
			anim.SetTrigger ("Right");
			if(moveX < 0)
            {
                visao = TargetVision.Left;
            }else
            {
                visao = TargetVision.Right;
            }
		}
		if (moveZ > 0 && Mathf.Abs (moveZ) > Mathf.Abs (moveX)) {
			anim.SetTrigger ("Up");
			visao = TargetVision.Back;
		}
	}

	public bool Jump ()
	{
		
        Ray raio = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if(Physics.Raycast(raio, out hit, 2f))
        {
            Debug.DrawLine(raio.origin, hit.point);
            GameObject obj = hit.collider.GetComponent<GameObject>();
            if(notJump)
            {
                notJump = false;
            }
        }
        //!notJump &&
        //Não está funcionando o raycast no terreno importado.
        if ( OnTheFloor && Input.GetButtonDown("ControlCharacter"))
        {
            rgd.AddForce(Vector3.up*5,ForceMode.Impulse);
            notJump = true;
            OnTheFloor = false;
            return true;
            
        }
        return false;


	}
    void CheckRecoverAmanusAuto()
    {
        if(Amanus < AmanusTotal)
        {
            IsRecover = true;
        }
    }
	public void Attack ()
	{
		if (Input.GetButtonDown ("Action") && !attack) {
			moveX = 0;
			moveZ = 0;
			anim.SetTrigger ("Attack");
			attack = true;
			StartCoroutine (Wait ());
			if(Alvo != null)
			{
				Alvo.StatsChange(20,0);
			}
		}
	}

	IEnumerator Wait ()
	{
		anim.SetTrigger ("Idle");
		yield return new WaitForSeconds (0.5f);
		attack = false;
	}

	void OnTriggerStay (Collider other)
	{
		if (other.tag == "enemy") {

			if (attack) {
				Destroy (other.gameObject);
			}
		}
	}
	void OnCollisionEnter(Collision col)
	{
        if(col.gameObject.tag == "Restart")
        {
            transform.position = Ini;
        }
        if(!OnTheFloor)
        {
            OnTheFloor = true;
        }
		if (col.gameObject.tag == "Monsters") {

			Alvo = (ITarget)col.gameObject.GetComponent(typeof(ITarget));

		}
			
	}
    /*
     * Equipamentos
     * 0 - Arma
     * 1 - Armadura
     */
    public Item Equipar(Item _equipamento, int tipo)
    {
        if(_Equipamentos[tipo] != null)
        {
            
            #region Desequipar
            Item _temp = _Equipamentos[tipo];
            _Equipamentos[tipo].IsEquip = false;
            AtkArmor -= _Equipamentos[tipo].Ataque;
            LifeArmor -= _Equipamentos[tipo].Vida;
            AmanusArmor -= _Equipamentos[tipo].Amanus;
            #endregion
            _Equipamentos[tipo] = _equipamento;
            _Equipamentos[tipo].IsEquip = false;
            CheckStatusFromEquiment();
            return _temp;
        }else
        {
            _Equipamentos[tipo] = _equipamento;
            _Equipamentos[tipo].IsEquip = false;
            CheckStatusFromEquiment();
        }
        return null;

    }
    void CheckStatusFromEquiment()
    {
        for(int i = 0; i < _Equipamentos.Length;i++)
        {
            if(_Equipamentos[i]!= null)
            {
                if(!_Equipamentos[i].IsEquip)
                {
                    AtkArmor += _Equipamentos[i].Ataque;
                    LifeArmor += _Equipamentos[i].Vida;
                    AmanusArmor += _Equipamentos[i].Amanus;
                    _Equipamentos[i].IsEquip = true;
                    print("ATk: " + Ataque + " - Vida: " + VidaTotal + " - Amanus: " + AmanusTotal);
                }
            }
        }
    }
}	