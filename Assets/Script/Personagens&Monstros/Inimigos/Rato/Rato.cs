using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Personagens e Monstros/Monstros Sripts/Monster/Rato")]
public class Rato : Monster {
    enum RatoBehavior { Idle, Tackle };
    public Rigidbody rdb;
    Vector3 Velocidade;
    public bool Horizontal = true;
    Vector3 PosIni;
    float vel = 2f;
    public float distance = 10f;
    float ramdom;
	// Use this for initialization
	void Start () {
        Level = 2;
        UpdateStatus();
        ExpEarn = 4;
        anim = GetComponentInChildren<Animator>();
        PosIni = transform.position;
        rdb = GetComponent<Rigidbody>();
        ramdom = Random.Range(0, 91);
        Horizontal = (Random.Range(0, 2) == 1 ? false : true);
        if (Horizontal)
        {
            visao = TargetVision.Left;
            anim.SetTrigger("Right");
            Velocidade.x = 1;
        }
        else
        {
            visao = TargetVision.Back;
            anim.SetTrigger("Up");
            Velocidade.z = 1;
        }
	}
	
	// Update is called once per frame
	void Update () {
        rdb.velocity = Velocidade;
        Idle();
	}
    public void Idle()
    {
        if (Horizontal)
        {
            vel = Velocidade.x;
            if (transform.position.x > (PosIni.x + distance))
            {

                visao = TargetVision.Left;
                #region ResetTrigers
                anim.ResetTrigger("Up");
                anim.ResetTrigger("Right");
                anim.ResetTrigger("Down");
                #endregion
                anim.SetTrigger("Left");
                Velocidade.x = -1;
                return;
            }
            if ((PosIni.x - distance) > transform.position.x)
            {
                visao = TargetVision.Right;
                #region ResetTrigers
                anim.ResetTrigger("Up");
                anim.ResetTrigger("Down");
                anim.ResetTrigger("Left");
                #endregion
                anim.SetTrigger("Right");
                Velocidade.x = 1;
                return;
            }
        }
        else
        {
            vel = Velocidade.z;
            if (transform.position.z > (PosIni.z + distance))
            {
                visao = TargetVision.Front;
                #region ResetTrigers
                anim.ResetTrigger("Up");
                anim.ResetTrigger("Right");
                anim.ResetTrigger("Left");
                #endregion
                anim.SetTrigger("Down");
                Velocidade.z = -1;
                return;
            }
            if ((PosIni.z - distance) > transform.position.z)
            {
                visao = TargetVision.Back;
                #region ResetTrigers
                anim.ResetTrigger("Down");
                anim.ResetTrigger("Right");
                anim.ResetTrigger("Left");
                #endregion
                anim.SetTrigger("Up");
                Velocidade.z = 1;
                return;
            }
        }
    }
}
