using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UISkill : MonoBehaviour
{
    public RectTransform[] Paineis = new RectTransform[4];
    public Text[] CaixaDeTextos = new Text[4];
    public GameObject[] _Slots = new GameObject[4];
    public RectTransform Seletor;
    ISkill[] Slots = new ISkill[4];
    int IndiceUniversal = 0;
    PlayerMovement Personagem;
    // Use this for initialization
    void Start()
    {
        Personagem = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        for (int i = 0; i < 4; i++)
        {
            if (_Slots[i] != null)
            {
                Slots[i] = (ISkill)_Slots[i].GetComponent(typeof(ISkill));
            }
        }
        AtualizarSlotInfo();
    }

    // Update is called once per frame
    void Update()
    {
        if (Personagem.EstadoDoJogador != GameStates.CharacterState.DontMove)
        {
            Movimentar();
        }
    }
    void Movimentar()
    {
        AtualizarSlotInfo();
        //Cima
        if (Input.GetKeyDown(KeyCode.Q))
        {
            IndiceUniversal = (IndiceUniversal + 1) % Slots.Length;
            OrganizarTextos(1);
        }
        //Baixo
        if (Input.GetKeyDown(KeyCode.E))
        {
            IndiceUniversal = IndiceUniversal == 0 ? Slots.Length - 1 : IndiceUniversal - 1;
            OrganizarTextos(-1);
        }
        //Usar
        if (Input.GetButtonDown("Defense"))
        {
            if (Slots[IndiceUniversal] != null)
            {
                if (Personagem.EstadoDoJogador != GameStates.CharacterState.Defense)
                {
                    if (Slots[IndiceUniversal].Alvo == SkillTarget.Self)
                    {
                        print("Personagem");
                        Slots[IndiceUniversal].UsarSkill(Personagem);
                    }
                    else if (Slots[IndiceUniversal].Alvo == SkillTarget.Other)
                    {
                        print("oTHER");
                        Slots[IndiceUniversal].UsarSkill(Personagem.Alvo);

                    }
                }else
                {
                    if(Slots[IndiceUniversal].Nome =="Escudos")
                    {
                        if (Slots[IndiceUniversal].Alvo == SkillTarget.Self)
                        {
                            print("Personagem");
                            Slots[IndiceUniversal].UsarSkill(Personagem);
                        }
                        else if (Slots[IndiceUniversal].Alvo == SkillTarget.Other)
                        {
                            print("oTHER");
                            Slots[IndiceUniversal].UsarSkill(Personagem.Alvo);

                        }
                    }
                }
            }
        }
    }
    void OrganizarTextos(int sinal)
    {
        Paineis[IndiceUniversal].anchoredPosition = Seletor.anchoredPosition;
        for (int i = 0; i < Slots.Length; i++)
        {
            if(IndiceUniversal+1 >= Slots.Length && i == 0)
            {
                Paineis[0].anchoredPosition = new Vector2(Seletor.anchoredPosition.x, Seletor.sizeDelta.y * -1);
            }
            else if (i == IndiceUniversal + 1)
            {
                Paineis[IndiceUniversal+1].anchoredPosition = new Vector2(Seletor.anchoredPosition.x, Seletor.sizeDelta.y * -1);
            }
            else if (IndiceUniversal - 1 < 0 && i == Slots.Length - 1)
            {
                Paineis[Slots.Length - 1].anchoredPosition = new Vector2(Seletor.anchoredPosition.x, Seletor.sizeDelta.y * 1);
            }
            else if (i == IndiceUniversal - 1)
            {
                Paineis[IndiceUniversal - 1].anchoredPosition = new Vector2(Seletor.anchoredPosition.x, Seletor.sizeDelta.y * 1);
            }else if(i != IndiceUniversal)
            {
                Paineis[i].anchoredPosition = new Vector2(Seletor.anchoredPosition.x, Seletor.sizeDelta.y * 2);
            }
        }

        
       
    }
    void AtualizarSlotInfo()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i] != null)
            {
                if (Slots[i].Nome != CaixaDeTextos[i].text)
                {
                    CaixaDeTextos[i].text = Slots[i].Nome;
                }
                if (Slots[i].OnCoolDown && CaixaDeTextos[i].color != Color.red)
                {
                    CaixaDeTextos[i].color = Color.red;
                }
                else if (!Slots[i].OnCoolDown && CaixaDeTextos[i].color != Color.black && Personagem.EstadoDoJogador != GameStates.CharacterState.Defense)
                {
                    CaixaDeTextos[i].color = Color.black;
                }else if(Personagem.EstadoDoJogador == GameStates.CharacterState.Defense)
                {
                    if(Slots[i].Nome == "Escudos")
                    {
                        CaixaDeTextos[i].color = Color.blue;
                    }else if(Slots[i] == null || Slots[i].Nome != "Escudos")
                    {
                        CaixaDeTextos[i].color = Color.red;
                    }
                }
            }else
            {
                CaixaDeTextos[i].text = "Slot Vázio";
            }
        }
    }
}
