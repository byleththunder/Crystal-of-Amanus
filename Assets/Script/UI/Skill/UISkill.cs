using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UISkill : MonoBehaviour
{
    public RectTransform[] Paineis = new RectTransform[4];
    public Text[] CaixaDeTextos = new Text[4];
    public GameObject[] _Slots = new GameObject[4];
    public RectTransform Seletor;
    ISkill[] SlotSkill = new ISkill[4];
    Item[] SlotsItem = new Item[4];
    int IndiceUniversal = 0;
    Character Personagem;
    Color Disablecolor;
    // Use this for initialization
    void Start()
    {
        Personagem = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        Disablecolor = CaixaDeTextos[0].color;
        for (int i = 0; i < 4; i++)
        {
            if (_Slots[i] != null)
            {
                SlotSkill[i] = (ISkill)_Slots[i].GetComponent(typeof(ISkill));
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
            if (!IsInvoking("AddItensSlots"))
            {
                Invoke("AddItensSlots", 0.5f);
            }
        }
    }
    void Movimentar()
    {
        AtualizarSlotInfo();
        //Cima
        if (Input.GetKeyDown(KeyCode.Q))
        {
            IndiceUniversal = (IndiceUniversal + 1) % SlotSkill.Length;
            OrganizarTextos(1);
        }
        //Baixo
        if (Input.GetKeyDown(KeyCode.E))
        {
            IndiceUniversal = IndiceUniversal == 0 ? SlotSkill.Length - 1 : IndiceUniversal - 1;
            OrganizarTextos(-1);
        }
        //Usar
        if (Input.GetButtonDown("Defense"))
        {
            if (SlotSkill[IndiceUniversal] != null)
            {
                if (Personagem.EstadoDoJogador != GameStates.CharacterState.Defense)
                {
                    if (SlotSkill[IndiceUniversal].Alvo == SkillTarget.Self)
                    {
                        print("Personagem");
                        SlotSkill[IndiceUniversal].UsarSkill(Personagem);
                    }
                    else if (SlotSkill[IndiceUniversal].Alvo == SkillTarget.Other)
                    {
                        print("oTHER");
                        SlotSkill[IndiceUniversal].UsarSkill(Personagem.Alvo);

                    }
                }else
                {
                    if(SlotSkill[IndiceUniversal].Nome =="Escudos")
                    {
                        if (SlotSkill[IndiceUniversal].Alvo == SkillTarget.Self)
                        {
                            print("Personagem");
                            SlotSkill[IndiceUniversal].UsarSkill(Personagem);
                        }
                        else if (SlotSkill[IndiceUniversal].Alvo == SkillTarget.Other)
                        {
                            print("oTHER");
                           SlotSkill[IndiceUniversal].UsarSkill(Personagem.Alvo);

                        }
                    }
                }
            }else
            {
                //print("Item");
                if(SlotsItem[IndiceUniversal] != null)
                {
                    //print("Tem Item: "+ SlotsItem[IndiceUniversal].Nome);
                    Inventario _inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventario>();
                    _inv.UsarItem(SlotsItem[IndiceUniversal], Personagem);
                    CheckItens();
                    return;
                }
            }
        }
    }
    void OrganizarTextos(int sinal)
    {
        Paineis[IndiceUniversal].anchoredPosition = Seletor.anchoredPosition;
        for (int i = 0; i < SlotSkill.Length; i++)
        {
            if(IndiceUniversal+1 >= SlotSkill.Length && i == 0)
            {
                Paineis[0].anchoredPosition = new Vector2(Seletor.anchoredPosition.x, Seletor.sizeDelta.y * -1);
            }
            else if (i == IndiceUniversal + 1)
            {
                Paineis[IndiceUniversal+1].anchoredPosition = new Vector2(Seletor.anchoredPosition.x, Seletor.sizeDelta.y * -1);
            }
            else if (IndiceUniversal - 1 < 0 && i == SlotSkill.Length - 1)
            {
                Paineis[SlotSkill.Length - 1].anchoredPosition = new Vector2(Seletor.anchoredPosition.x, Seletor.sizeDelta.y * 1);
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
        int indice = 0;
        for (int i = 0; i < SlotSkill.Length; i++)
        {
            if (SlotSkill[i] != null)
            {
                indice = i;
                if (SlotSkill[i].Nome != CaixaDeTextos[i].text)
                {
                    CaixaDeTextos[i].text = SlotSkill[i].Nome;
                }
                if (SlotSkill[i].OnCoolDown && CaixaDeTextos[i].color != Color.red)
                {
                    CaixaDeTextos[i].color = Color.red;
                }
                else if (!SlotSkill[i].OnCoolDown && CaixaDeTextos[i].color != Color.black && Personagem.EstadoDoJogador != GameStates.CharacterState.Defense)
                {
                    CaixaDeTextos[i].color = Color.black;
                }else if(Personagem.EstadoDoJogador == GameStates.CharacterState.Defense)
                {
                    if(SlotSkill[i].Nome == "Escudos")
                    {
                        CaixaDeTextos[i].color = Color.blue;
                    }else if(SlotSkill[i] == null || SlotSkill[i].Nome != "Escudos")
                    {
                        CaixaDeTextos[i].color = Color.red;
                    }
                }
            }else
            {
                break;
            }
        }
        for (int i = 0; i < SlotsItem.Length; i++)
        {
            if(SlotsItem[i] != null)
            {
                indice++;
                if (SlotsItem[i].Nome != CaixaDeTextos[i].text)
                {
                    CaixaDeTextos[i].text = SlotsItem[i].Nome;
                }
                if (Personagem.EstadoDoJogador != GameStates.CharacterState.Defense)
                {
                    CaixaDeTextos[i].color = Color.black;
                }
                
            }else
            {
                //break;
            }
        }
    }
    void AddItensSlots()
    {
        int espacosLivres = 0;
        foreach (ISkill skill in SlotSkill)
        {
            if (skill == null)
            {
                espacosLivres++;
            }
        }
        //print(string.Format("Espaços Livres: {0}",espacosLivres));
        Inventario _inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventario>();
        if (_inv.MochilaLenght < espacosLivres)
        {
            for (int i = 0; i < _inv.MochilaLenght; i++)
            {
                var indicetemp = (SlotsItem.Length - espacosLivres) + i;
                if (_inv.MochilaRef[i].Tipo == Item.TipoDeItem.Potion)
                {
                    
                   // print(string.Format("Indice Temporário: {0}", indicetemp));
                    SlotsItem[indicetemp] = _inv.MochilaRef[i];
                    print(SlotsItem[indicetemp].name);
                }else if(_inv.MochilaRef[i] != SlotsItem[indicetemp])
                {
                    SlotsItem[indicetemp] = null;
                }
            }
            return;
        }
        else
        {
            for (int i = 0; i < espacosLivres; i++)
            {
                var indicetemp = (SlotsItem.Length - espacosLivres) + i;
                if (_inv.MochilaRef[i] != null)
                {
                    if (_inv.MochilaRef[i].Tipo == Item.TipoDeItem.Potion)
                    {
                        
                        print(string.Format("Indice Temporário: {0}",indicetemp));
                        SlotsItem[indicetemp] = _inv.MochilaRef[i];
                        print(SlotsItem[indicetemp].name);
                    }
                }else
                {
                    SlotsItem[indicetemp] = null;
                }
            }
            return;
        }

    }
    void CheckItens()
    {
        Inventario _inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventario>();
        for(int i =0; i <SlotsItem.Length; i ++)
        {
            if (SlotsItem[i] != null)
            {
                bool HasItem = false;
                for (int j = 0; j < _inv.MochilaRef.Count; j++)
                {
                    if (SlotsItem[i] == _inv.MochilaRef[j])
                    {
                        HasItem = true;
                        break;
                    }
                }
                if (!HasItem)
                {
                    
                        SlotsItem[i] = null;
                        CaixaDeTextos[i].text = "Slot Vázio";
                        CaixaDeTextos[i].color = Disablecolor;
                    
                }
            }
        }
    }
}
