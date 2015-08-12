using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MonsterTest : MonoBehaviour, ITarget
{


    public string Nome { get { return "Monstro"; } }
    public int Vida { get { return HP; } set { Vida = HP; } }
    public int VidaTotal { get { return 100; } }
    public int Amanus { get { return MP; } set { Amanus = MP; } }
    public int Ataque { get { return 50; } }
    public int AmanusTotal { get { return 100; } }
    public TargetVision visao { get; set; }
    public GameObject obj { get { return this.gameObject; } }
    public Item[] Equipamentos { get; set; }
    int HP, MP;
    public RectTransform Barra;
    //Attack Variables
    public bool CoolDown = false;
    public float CoolDownTime = 2f;
    FireMagic FireFactory;
    public int FireBallsQuantity = 4;
    public bool IsPlayerOnAttackArea = false;
    Vector3 PlayerLocation;
    public bool CloseAttck = false;
    public bool RangeAttack = false;
    // Use this for initialization
    void Start()
    {
        HP = VidaTotal;
        MP = AmanusTotal;
        FireFactory = GameObject.FindGameObjectWithTag("FireMagic").GetComponent<FireMagic>();
    }
    public void StatsChange(int _vida, int _Amanus)
    {
        HP -= _vida;
        MP -= _Amanus;

    }


    // Update is called once per frame
    void Update()
    {
        if (Vida <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            Barra.anchoredPosition = new Vector2(CalcularPos(HP, VidaTotal, Barra.sizeDelta.x), Barra.anchoredPosition.y);

        }
        if (!CoolDown)
        {
            AttackAction();
        }
    }

    float CalcularPos(float Stats, float StatsMax, float PosMax)
    {
        float y = (Stats * 100) / StatsMax;
        float x = ((PosMax * y) / 100) - PosMax;
        return x;
    }
    void AttackAction()
    {
        #region Manufactoring
        Fire[] FireBalls = new Fire[FireBallsQuantity];
        FireBalls = FireFactory.MonsterFireAttack(FireBallsQuantity);
        #endregion
        #region Set Settings

        if (IsPlayerOnAttackArea)
        {
            if (CloseAttck)
            {
                for (int i = 0; i < FireBalls.Length; i++)
                {


                    FireBalls[i].Tag = "Player";
                    FireBalls[i].WhoShoot = "Monsters";
                    FireBalls[i].Damage = (Ataque * 100) / 100;
                    FireBalls[i].transform.position = transform.position;
                    Vector3 _from = transform.position - PlayerLocation;
                    int direction = 1;
                    if (_from.x > 0)
                        direction = -1;
                    else
                        direction = 1;
                    float angle = Vector3.Angle(_from, transform.forward) * direction;

                    FireBalls[i].transform.localEulerAngles = new Vector3(0, angle, 0);
                    FireBalls[i].transform.position += FireBalls[i].transform.forward;


                }
                StartCoroutine(FireAttack(FireBalls, 0.15f));
            }
        }
        else
        {
            if (RangeAttack)
            {
                for (int i = 0; i < FireBalls.Length; i++)
                {
                    FireBalls[i].Tag = "Player";
                    FireBalls[i].WhoShoot = "Monsters";
                    FireBalls[i].Damage = (Ataque * 100) / 100;
                    FireBalls[i].transform.position = transform.position;
                    float angulo = 360f / (FireBalls.Length);
                    FireBalls[i].transform.localEulerAngles = new Vector3(0, angulo * (i + 1), 0);
                    FireBalls[i].transform.position += FireBalls[i].transform.forward;
                    FireBalls[i].gameObject.SetActive(true);
                }
            }
        }
        #endregion
        #region StartCoolDown
        CoolDown = true;
        Invoke("AttackCoolDown", CoolDownTime);
        #endregion

    }
    IEnumerator FireAttack(Fire[] FireBalls, float delaytime)
    {
        for (int i = 0; i < FireBalls.Length; i++)
        {
            FireBalls[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(delaytime);
        }
    }
    void AttackCoolDown()
    {
        CoolDown = false;
    }
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            IsPlayerOnAttackArea = true;
            PlayerLocation = col.transform.position;

        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            IsPlayerOnAttackArea = false;
        }
    }
}
