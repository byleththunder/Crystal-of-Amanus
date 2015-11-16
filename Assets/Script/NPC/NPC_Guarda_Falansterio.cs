using UnityEngine;
using System.Collections;

public class NPC_Guarda_Falansterio : MonoBehaviour
{

    [SerializeField]
    private TargetVision Visao = TargetVision.Left;
    public Animator anim;

    bool Falar = false;
    Transform Player;
    GameObject Box;
    // Use this for initialization
    void Start()
    {
        MessageBox.PrefabPath = "CaixaDeTexto";
        anim.SetFloat("AnimSpeed", 0.2f);
        switch (Visao)
        {
            case TargetVision.Back:
                anim.SetTrigger("Up");
                break;
            case TargetVision.Front:
                anim.SetTrigger("Down");
                break;
            case TargetVision.Left:
                anim.SetTrigger("Left");
                break;
            case TargetVision.Right:
                anim.SetTrigger("Right");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Falar)
        {
            if(!Box.activeInHierarchy)
            {
                Player.position += -transform.forward * 2;
                Falar = false;
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            Player = col.gameObject.transform;
            MessageBox.PrefabPath = "CaixaDeTexto";
            MessageBox.Instance.WriteMessage("Nem mais um passo adiante!!", "Guarda");
            Box = MessageBox.Instance.painel;
            Falar = true;
        }
    }
}
