using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class Menu : MonoBehaviour
{
    public Button Bt_Item, Bt_Arma, Bt_Voltar;
    public GameObject Jogador, Organizador;
    public ItemContent prefabItens;
    List<ItemContent> itens;
    public GameObject Painel_Principal, Painel_Item;
    int x = 0;
    int y = 0;
    int QuantidadeDeItensNaMochila = 0;
    int indice = 0;
    int indicePassado = -1;
    bool switchTela = false;
    public bool oneTime = true;
    
    // Use this for initialization
    void Start()
    {
        
        Bt_Item.onClick.AddListener(MenuItem);
        Bt_Voltar.onClick.AddListener(MenuPrincipal);
        itens = new List<ItemContent>();
    }

    // Update is called once per frame

    void Update()
    {



        if (Painel_Item.activeInHierarchy == true)
        {
            //OrganizarItens();
            ////AttItenFormat();
            //NavegarLista();

        }

    }
    void MenuPrincipal()
    {
        if (Painel_Item.activeInHierarchy)
        {
            Painel_Item.SetActive(false);
            Painel_Principal.SetActive(true);
        }
    }
    void MenuItem()
    {
        if (Painel_Principal.activeInHierarchy == true)
        {
            Painel_Item.SetActive(true);
            Painel_Principal.SetActive(false);
        }
    }
    //void OrganizarItens()
    //{
    //    Inventario mochila = Jogador.GetComponent<Inventario>();

    //    if (QuantidadeDeItensNaMochila < mochila._mochila.Count)
    //    {

    //        for (int i = 0; i < mochila._mochila.Count; i++)
    //        {
    //            if (i == mochila._mochila.Count - 1)
    //            {
    //                QuantidadeDeItensNaMochila = mochila._mochila.Count;
    //            }
    //            if (Organizador.transform.parent.FindChild(mochila._mochila[i].Nome))
    //            {
    //                itens.Find(x => x.name == mochila._mochila[i].Nome).quantidade = mochila._mochila[i].Quantidade;
    //            }
    //            else
    //            {
    //                ItemContent novoIten = Instantiate(prefabItens) as ItemContent;
    //                itens.Add(novoIten);
    //                // print(novoIten == null);
    //                novoIten.name = mochila._mochila[i].Nome;
    //                novoIten.iten = mochila._mochila[i];
    //                //novoIten.transform.parent = Organizador.transform;
    //                novoIten.transform.SetParent(Organizador.transform, false);
    //                novoIten.x = x;
    //                novoIten.y = y;
    //                novoIten.transform.localPosition = new Vector3(x + (x * 500), y - (y * 50), 0);
    //                x = (x + 1) % 2;
    //                if (x == 0)
    //                {
    //                    y++;
    //                }
    //            }
    //        }
    //    }
    //    AttInformacoes();

    //}
    //public void AttInformacoes()
    //{
    //    print("Att");

       

    //    if (itens != null)
    //    {
    //        for (int i = 0; i < Mochila.Count; i++)
    //        {
    //            itens[i].iten = Mochila[i];
    //        }
    //    }

    //}
    //void AttItenFormat()
    //{
    //    x = y = 0;
    //    for (int j = 0; j < itens.Count; j++)
    //    {
    //        itens[j].x = x;
    //        itens[j].y = y;
    //        x = (x + 1) % 2;
    //        if (x == 0)
    //        {
    //            y++;
    //        }
    //    }
    //    RectTransform org = Organizador.GetComponent<RectTransform>();
    //    foreach (ItemContent i in itens)
    //    {

    //    }
    //}
    //void NavegarLista()
    //{
    //    if (indice > -1 && indice < itens.Count)
    //    {
    //        if (itens[indice].Painel != null)
    //        {
    //            itens[indice].Painel.color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 100.0f);
    //        }


    //    }
    //    if (indicePassado > -1 && indicePassado < itens.Count)
    //    {
    //        itens[indicePassado].Painel.color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.0f);
    //    }

    //    if (Input.GetButtonDown("Action"))
    //    {
            

    //        //Mochila[indice].UsarItem();
    //        AttInformacoes();
    //        if (itens[indice].iten.Quantidade <= 0)
    //        {
    //            Destroy(itens[indice].gameObject);
    //            itens.RemoveAt(indice);
    //        }


    //        AttItenFormat();

    //    }
    //    if (Input.GetKeyDown(KeyCode.RightArrow) && indice + 1 < itens.Count)
    //    {

    //        if (indicePassado != null)
    //        {
    //            indicePassado = indice;
    //        }
    //        indice++;
    //    }
    //    if (Input.GetKeyDown(KeyCode.LeftArrow) && indice - 1 > -1)
    //    {
    //        if (indicePassado != null)
    //        {
    //            indicePassado = indice;
    //        }
    //        indice--;
    //    }
    //    if (Input.GetKeyDown(KeyCode.DownArrow) && indice + 2 < itens.Count)
    //    {
    //        if (indicePassado != null)
    //        {
    //            indicePassado = indice;
    //        }
    //        indice += 2;
    //    }
    //    if (Input.GetKeyDown(KeyCode.UpArrow) && indice - 2 > -1)
    //    {
    //        if (indicePassado != null)
    //        {
    //            indicePassado = indice;
    //        }
    //        indice -= 2;
    //    }
    //}
}
