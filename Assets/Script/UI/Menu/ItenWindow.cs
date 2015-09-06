using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ItenWindow : MonoBehaviour
{
    Target Persoangem;
    List<ItemContent> Slots;
    public ItemContent Prefab;
    RectTransform PrefabValues;
    Vector2 PrefabSize { get { return PrefabValues.sizeDelta; } }
    int Indice = 0;
    int IndiceAnterior = -1;
    bool atualizado = false;
    Inventario Inv;
    int X = 0;
    int Y = 0;
    public ScrollRect Scroll;
    public GameObject Screen;
    //Awake
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

    }
    // Use this for initialization
    void AtualizarLista()
    {
        ReSizeScroll(Scroll);
        for (int i = 0; i < Slots.Count; i++)
        {
            if(Slots[i].iten == null)
            {
                Slots[i].gameObject.SetActive(false);
            }
        }
        Pool(Inv.MochilaLenght);
        for (int i = 0; i < Inv.MochilaLenght; i++)
        {
            Slots[i].AdicionarItem(Inv.MochilaRef[i], Inv.QuantidadesRef[i]);
        }
        Scroll.verticalScrollbar.value = 1;
        print("Scroll Value: " + Scroll.verticalScrollbar.value.ToString());
        
    }
    void Start()
    {
        GameObject _pers = GameObject.FindGameObjectWithTag("Player");
        if (_pers != null)
        {
            Inv = _pers.GetComponent<Inventario>();
            Persoangem = (Target)_pers.GetComponent(typeof(Target));
        }
        if (Slots == null)
        {
            Slots = new List<ItemContent>();
        }
        if (PrefabValues == null)
        {
            PrefabValues = Prefab.GetComponent<RectTransform>();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
        if (Slots.Count > 0)
        {
            
            Slots[Indice].ChangeColor(true);
            if (Slots.Count > 1)
            {
                if (IndiceAnterior > -1 && IndiceAnterior != Indice)
                {
                    Slots[IndiceAnterior].ChangeColor(false);
                }
                //Movimentação
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    IndiceAnterior = Indice;
                   
                    if (Indice < Slots.Count - 2)
                    {
                        Indice = (Indice + 2);
                        ScrollValue(Scroll, -1);
                    }
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    IndiceAnterior = Indice;
                    
                    if (Indice >1)
                    {
                        Indice -= 2;
                        ScrollValue(Scroll, 1);
                    }
                    
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    IndiceAnterior = Indice;
                    
                    if(Indice < Slots.Count -1)
                    {
                        Indice = (Indice + 1);
                        //ScrollValue(Scroll, -1);
                    }
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    IndiceAnterior = Indice;
                    
                    if (Indice > 0)
                    {
                        Indice -= 1;
                        //ScrollValue(Scroll, 1);
                    }
                    
                }
                
            }
            if (Input.GetButtonDown("Action"))
            {
                if (Slots[Indice].iten != null)
                {
                    if (Slots[Indice].UsarItem(Persoangem))
                    {
                        Inv.DiscartItem(Slots[Indice].iten);
                        AtualizarLista();
                    }

                }
            }
        }
    }

    //------------------------
    void Pool(int lenght)
    {
        var slotsActives = 0;
        var slotsDisables = 0;
        foreach(ItemContent slot in Slots)
        {
            if(slot.gameObject.activeInHierarchy)
            {
                slotsActives++;
            }else
            {
                slotsDisables++;
            }
        }
        if (slotsActives < lenght)
        {
            for (int i = 0; i < Slots.Count;i++ )
            {
                if(!Slots[i].gameObject.activeInHierarchy)
                {
                    Slots[i].gameObject.SetActive(true);
                }
            }
                while (Slots.Count < lenght)
                {
                    CreateSlot();
                }
            
        }

    }
    void CreateSlot()
    {

        #region Criando o espaço
        ItemContent obj = Instantiate(Prefab);
        obj.transform.SetParent(Screen.transform, false);
        RectTransform rect = obj.GetComponent<RectTransform>();
        RectTransform _tempScreenRect = Screen.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2((X > 0 ? X * 150 : 0), -.5f * (Y > 0 ? Y * 50 : 1));
        //rect.anchoredPosition = new Vector2((X * (_tempScreenRect.sizeDelta.x/2))- (PrefabSize.x/2)-10, ((_tempScreenRect.sizeDelta.y/2)-0) - PrefabSize.y * (Y+1));
        obj.name = "Slot " + X + " - " + Y;
        Slots.Add(obj);
        #endregion
        #region Organizando Posicoes

        if (X % 2 != 0)
        {
            X = 0;
            Y++;
        }
        else
        {
            X++;
        }
        #endregion

    }
    void AtualizarPosicoes()
    {
        for(int i = 1; i < Y+1; i++)
        {
            for(int j = 0; j <2; j ++)
            {
                RectTransform rect = Slots[i*j].GetComponent<RectTransform>();
                RectTransform _tempScreenRect = Screen.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2((j==0?0:j*150),  -.5f * (i>0?i*10:1));
            }
        }
    }
    public void Att()
    {
        AtualizarLista();
        print("Atualizado");
    }
    //-----------------------
    void ReSizeScroll(ScrollRect bar)
    {
        int l;
        if (Slots.Count < Inv.MochilaLenght)
        {
            l = Inv.MochilaLenght;
        }
        else
        {
            l = Slots.Count;
        }
        RectTransform rectTemp = Screen.GetComponent<RectTransform>();
        int slotsnum = (int)(l / 2);
        float tempSize = ((slotsnum * PrefabSize.y) / rectTemp.sizeDelta.y);
        float sizey;
        print("Tamanho 3:  " + tempSize);
        if (tempSize > 1)
        {
            sizey = ((slotsnum + 1) * PrefabSize.y);
            print("Size: " + sizey + " - sizeAntigo: " + rectTemp.sizeDelta.y);
        }
        else
        {
            sizey = rectTemp.sizeDelta.y;
        }
        rectTemp.sizeDelta = new Vector2(rectTemp.sizeDelta.x, sizey);
        float y = ((rectTemp.sizeDelta.y - 220) / 2) * (-1f);
        rectTemp.anchoredPosition = new Vector2(0, y);
        //AtualizarPosicoes();
        bar.verticalScrollbar.value = 0;
    }
    
    void ScrollValue(ScrollRect bar, int sinal)
    {
        RectTransform rectTemp = this.GetComponent<RectTransform>();
        float indicenum = ((Slots.Count+1f) / 2f);
        float tempSize = 1f / indicenum;
        
        print("Tamanho 2: " + tempSize);
        bar.verticalScrollbar.value += tempSize*sinal;
        Mathf.Clamp(bar.verticalScrollbar.value, 0, 1f);
    }
}
