using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu("Scripts/Transportes/Teletransporte")]
public class Teleport : MonoBehaviour
{


    public Vector3 NextPosition;
    public GameObject Fade;
    bool FadeBegin = false;
    GameObject Player;
    float Timer = 0;
    Color FadeColor;
    bool Change = false;
    void Start()
    {
        NextPosition += transform.position;
        FadeColor = Fade.GetComponent<Image>().color;
    }
    void Update()
    {
        if(Fade)
        {
            if (FadeBegin)
            {
                Fade.GetComponent<Image>().color = FadeColor;
                Timer += Time.deltaTime;
                if (Timer > 0.05f)
                {
                    Timer = 0;
                    if (!Change)
                    {
                        FadeColor.a += 0.1f;
                        if (FadeColor.a > 0.9f)
                        {
                            Change = true;
                            if (Player)
                            {
                                Player.transform.position = NextPosition;
                                
                            }
                        }
                    }
                    else
                    {
                        FadeColor.a -= 0.1f;
                        if (FadeColor.a < 0.1f)
                        {
                            Player.GetComponent<Character>().EstadoDoJogador = GameStates.CharacterState.Playing;
                            Player = null;
                            FadeBegin = false;
                            Change = false;
                        }
                    }
                }
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + NextPosition, 0.2f);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position + NextPosition, transform.position + NextPosition + new Vector3(0, 0, 2));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + NextPosition, transform.position + NextPosition + new Vector3(2, 0, 0));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + NextPosition, transform.position + NextPosition + new Vector3(0, 2, 0));

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!FadeBegin)
            {
                Player = other.gameObject;
                Player.GetComponent<Character>().EstadoDoJogador = GameStates.CharacterState.DontMove;
                FadeBegin = true;
            }
        }
    }
}
