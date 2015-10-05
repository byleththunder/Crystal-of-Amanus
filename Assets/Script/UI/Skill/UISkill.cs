using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TeamUtility.IO;

public class UISkill : MonoBehaviour
{
    public RectTransform[] Paineis = new RectTransform[4];
    public Image[] Imagens = new Image[4];
    public Image SkillImg;
    public GameObject[] _Slots = new GameObject[4];
    public RectTransform Seletor;
    Skill[] SlotSkill = new Skill[4];
    Item[] SlotsItem = new Item[4];
    int IndiceUniversal = 0;
    public GameObject GameCharacter;
    Character Personagem;
    Color Disablecolor;
    bool press = false;
    // Use this for initialization
    void Start()
    {
        Personagem = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        Disablecolor = Imagens[0].color;
        SlotSkill = Personagem.GetComponent<CharacterSkills>().Slots;
        AtualizarSlotInfo();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Personagem.EstadoDoJogador != GameStates.CharacterState.DontMove)
        {
            Movimentar();
            AddItensSlots();
            
        }
        if (InputManager.GetAxisRaw("ScrollSkill") == 0)
        {
            press = false;
        }
    }
    void Movimentar()
    {
        AtualizarSlotInfo();
        OrganizarTextos();
        if (SlotSkill[IndiceUniversal] != null)
        {
            SkillImg.color = Color.white;
            SkillImg.sprite = SlotSkill[IndiceUniversal].Img;
        }
        else if (SlotsItem[IndiceUniversal] != null)
        {
            SkillImg.color = Color.white;
            SkillImg.sprite = SlotsItem[IndiceUniversal].Img;
        }
        else
        {
            SkillImg.color = new Color(1, 1, 1, 0);
            SkillImg.sprite = null;
        }
        //Cima
        if (InputManager.GetAxisRaw("ScrollSkill") == 1 && !press)
        {
            IndiceUniversal = (IndiceUniversal + 1) % SlotSkill.Length;
            OrganizarTextos();
            press = true;
        }
        //Baixo
        if (InputManager.GetAxisRaw("ScrollSkill") == -1 && !press)
        {
            IndiceUniversal = IndiceUniversal == 0 ? SlotSkill.Length - 1 : IndiceUniversal - 1;
            OrganizarTextos();
            press = true;
        }
        //Usar
        if (InputManager.GetButtonDown("Magic"))
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
                }
                else
                {
                    if (SlotSkill[IndiceUniversal].Nome == "Escudos")
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
            }
            else
            {
                //print("Item");
                if (SlotsItem[IndiceUniversal] != null)
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
    void OrganizarTextos()
    {
        Paineis[IndiceUniversal].anchoredPosition = Seletor.anchoredPosition;
        if (SlotSkill[IndiceUniversal] != null || SlotsItem[IndiceUniversal] != null)
            Imagens[IndiceUniversal].color = new Color(Imagens[IndiceUniversal].color.r, Imagens[IndiceUniversal].color.g, Imagens[IndiceUniversal].color.b, 1);
        for (int i = 0; i < SlotSkill.Length; i++)
        {
            if (IndiceUniversal + 1 >= SlotSkill.Length && i == 0)
            {
                Paineis[0].anchoredPosition = new Vector2(Seletor.anchoredPosition.x, Seletor.sizeDelta.y * -1);
                if (SlotSkill[0] != null || SlotsItem[0] != null)
                    Imagens[0].color = new Color(Imagens[0].color.r, Imagens[0].color.g, Imagens[0].color.b, 0.3f);
            }
            else if (i == IndiceUniversal + 1)
            {
                Paineis[IndiceUniversal + 1].anchoredPosition = new Vector2(Seletor.anchoredPosition.x, Seletor.sizeDelta.y * -1);
                if (SlotSkill[IndiceUniversal + 1] != null || SlotsItem[IndiceUniversal + 1] != null)
                    Imagens[IndiceUniversal + 1].color = new Color(Imagens[IndiceUniversal + 1].color.r, Imagens[IndiceUniversal + 1].color.g, Imagens[IndiceUniversal + 1].color.b, 0.3f);
            }
            else if (IndiceUniversal - 1 < 0 && i == SlotSkill.Length - 1)
            {
                Paineis[SlotSkill.Length - 1].anchoredPosition = new Vector2(Seletor.anchoredPosition.x, Seletor.sizeDelta.y * 1);
                if (SlotSkill[SlotSkill.Length - 1] != null || SlotsItem[SlotSkill.Length - 1] != null)
                    Imagens[SlotSkill.Length - 1].color = new Color(Imagens[SlotSkill.Length - 1].color.r, Imagens[SlotSkill.Length - 1].color.g, Imagens[SlotSkill.Length - 1].color.b, 0.3f);
            }
            else if (i == IndiceUniversal - 1)
            {
                Paineis[IndiceUniversal - 1].anchoredPosition = new Vector2(Seletor.anchoredPosition.x, Seletor.sizeDelta.y * 1);
                if (SlotSkill[IndiceUniversal - 1] != null || SlotsItem[IndiceUniversal - 1] != null)
                    Imagens[IndiceUniversal - 1].color = new Color(Imagens[IndiceUniversal - 1].color.r, Imagens[IndiceUniversal - 1].color.g, Imagens[IndiceUniversal - 1].color.b, 0.3f);
            }
            else if (i != IndiceUniversal)
            {
                Paineis[i].anchoredPosition = new Vector2(Seletor.anchoredPosition.x, Seletor.sizeDelta.y * 2);
                if (SlotSkill[i] != null || SlotsItem[i] != null)
                    Imagens[i].color = new Color(Imagens[i].color.r, Imagens[i].color.g, Imagens[i].color.b, 0.3f);
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
                if (SlotSkill[i].NameImg != Imagens[i].sprite)
                {
                    Imagens[i].sprite = SlotSkill[i].NameImg;
                }
                if (SlotSkill[i].OnCoolDown && Imagens[i].color != (new Color(Color.red.r, Color.red.g, Color.red.b, Imagens[i].color.a)))
                {
                    Imagens[i].color = new Color(Color.red.r, Color.red.g, Color.red.b, Imagens[i].color.a);
                }
                else if (!SlotSkill[i].OnCoolDown && Imagens[i].color != (new Color(Color.white.r, Color.white.g, Color.white.b, Imagens[i].color.a)) && Personagem.EstadoDoJogador != GameStates.CharacterState.Defense)
                {
                    Imagens[i].color = new Color(Color.white.r, Color.white.g, Color.white.b, Imagens[i].color.a);
                }
                else if (Personagem.EstadoDoJogador == GameStates.CharacterState.Defense)
                {
                    if (SlotSkill[i].Nome == "Escudos")
                    {
                        Imagens[i].color = new Color(Color.blue.r, Color.blue.g, Color.blue.b, Imagens[i].color.a);
                    }
                    else if (SlotSkill[i] == null || SlotSkill[i].Nome != "Escudos")
                    {
                        Imagens[i].color = new Color(Color.red.r, Color.red.g, Color.red.b, Imagens[i].color.a);
                    }
                }
            }
            else
            {
                break;
            }
        }
        for (int i = 0; i < SlotsItem.Length; i++)
        {
            if (SlotsItem[i] != null)
            {
                indice++;
                if (SlotsItem[i].NameImg != Imagens[i].sprite)
                {
                    Imagens[i].sprite = SlotsItem[i].NameImg;
                }
                if (Personagem.EstadoDoJogador != GameStates.CharacterState.Defense)
                {
                    Imagens[i].color = new Color(Color.white.r, Color.white.g, Color.white.b, Imagens[i].color.a);
                }

            }
            else if(SlotsItem[i] == null && SlotSkill[i] == null)
            {
                Imagens[i].color = new Color(Color.white.r, Color.white.g, Color.white.b, 0);
            }
        }
    }
    void AddItensSlots()
    {
        int espacosLivres = 0;
        foreach (Skill skill in SlotSkill)
        {
            if (skill == null)
            {
                espacosLivres++;
            }
        }
        Inventario _inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventario>();
        if (_inv.MochilaLenght < espacosLivres)
        {
            for (int i = 0; i < _inv.MochilaLenght; i++)
            {
                var indicetemp = (SlotsItem.Length - espacosLivres) + i;
                if (_inv.MochilaRef[i].Tipo == Item.TipoDeItem.Potion)
                {
                    SlotsItem[indicetemp] = _inv.MochilaRef[i];
                }
                else if (_inv.MochilaRef[i] != SlotsItem[indicetemp])
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

                        print(string.Format("Indice Temporário: {0}", indicetemp));
                        SlotsItem[indicetemp] = _inv.MochilaRef[i];
                        print(SlotsItem[indicetemp].name);
                    }
                }
                else
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
        for (int i = 0; i < SlotsItem.Length; i++)
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
                    Imagens[i].sprite = null;
                    Imagens[i].color = new Color(1, 1, 1, 0);

                }
            }
        }
    }
}
