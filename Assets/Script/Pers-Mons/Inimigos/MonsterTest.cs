using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MonsterTest : Monster
{
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
    public MonsterTest()
    {
        Nome = "Monster Teste";
        VidaTotal = 100;
        AmanusTotal = 100;
        Ataque = 5;
        Vida = VidaTotal;
        Amanus = AmanusTotal;
    }
    void Start()
    {
        
        FireFactory = GameObject.FindGameObjectWithTag("FireMagic").GetComponent<FireMagic>();
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
            Barra.anchoredPosition = new Vector2(CalcularPos(Vida, VidaTotal, Barra.sizeDelta.x), Barra.anchoredPosition.y);

        }
        if (!CoolDown)
        {
            AI();
        }
    }

    float CalcularPos(float Stats, float StatsMax, float PosMax)
    {
        float y = (Stats * 100) / StatsMax;
        float x = ((PosMax * y) / 100) - PosMax;
        return x;
    }

    public override void AI()
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
                    Vector3 _from = transform.forward - PlayerLocation;
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
                    FireBalls[i].transform.eulerAngles = new Vector3(0, angulo * (i + 1), 0);
                    FireBalls[i].transform.position += FireBalls[i].transform.forward*2;
                    FireBalls[i].gameObject.SetActive(true);
                    FireBalls[i] = null;
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
