using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

[AddComponentMenu("Scripts/VisualScripts/Loja/Shop")]
public class Shop : MonoBehaviour
{
    public GameObject Prefab;
    public List<Item> Produtos;
    public List<int> Quantidades;
    public int QuantidadeDeItens = 1;
    int ContaTotal = 0;
    public Character Personagem;
    List<int> Selecionados = new List<int>();
    public int ItemSelect = -1;
    //Ui Components
    public Text Descricao, Conta, CharGold;
    public GameObject Painel_Itens;
    
    // Use this for initialization
    void Start()
    {
        QuantidadeDeItens = Produtos.Count;
        Personagem = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        Quantidades = new List<int>();
        for (int i = 0; i < QuantidadeDeItens; i++)
        {
            Quantidades.Add(0);
        }
        GerarSlot();
    }

    // Update is called once per frame
    void Update()
    {
        if(ItemSelect>-1)
        {
            Descricao.text = "Tipo: " +Produtos[ItemSelect].Tipo.ToString()+"\nDescrição:\n\t"+Produtos[ItemSelect].Descricao;
        }
        int _contatemp = 0;
        for(int i = 0; i <QuantidadeDeItens; i++)
        {
            _contatemp += Produtos[i].Preco * Quantidades[i];
        }
        ContaTotal = _contatemp;
        Conta.text = "Total: " + ContaTotal + " Ag";
        if(Personagem)
        {
            CharGold.text = "Prata: " + Personagem.Gold + " Ag";
        }
    }
    public void Comprar()
    {
        //Reseta a conta e os itens selecionados
        for (int i = 0; i < Selecionados.Count; i++)
        {
            Selecionados.RemoveAt(i);
        }
        //verifica quais itens foram selecionados
        for (int i = 0; i < QuantidadeDeItens; i++)
        {
            if (Quantidades[i] > 0)
            {
                Selecionados.Add(i);
            }
        }
        if (Personagem.Gold < ContaTotal)//Se o personagem não tem dinheiro suficiente, ele não poderá comprar.
        {
            print("Dinheiro insuficiente");
            return;
        }
        else
        {
            Inventario inv = Personagem.GetComponent<Inventario>();
            for (int i = 0; i < Selecionados.Count; i++)
            {
                inv.PickItem(Produtos[Selecionados[i]], Quantidades[Selecionados[i]]);
                Quantidades[Selecionados[i]] = 0;
            }
            print("comprou");
            Personagem.Gold -= ContaTotal;
        }
        return;
    }
    public void Vender()
    {

    }
    public int AumentarQuantidade(int indice)
    {
         Quantidades[indice]++;
         return Quantidades[indice];
    }
    public int DiminuirQuantidade(int indice)
    {
         if(Quantidades[indice]-1 < 0)
         {
             Quantidades[indice] = 0;
         }else
         {
             Quantidades[indice]--;
         }
         return Quantidades[indice];
    }
    void GerarSlot()
    {
        for(int i = 0; i <QuantidadeDeItens;i++)
        {
            GameObject _temp = (GameObject)Instantiate(Prefab);
            RectTransform _transform = _temp.GetComponent<RectTransform>();
            _temp.transform.SetParent(Painel_Itens.transform, false);
            if (i > 0)
            {
                _transform.anchoredPosition = new Vector2((-.5f), (-1.5f) * (i *30));
            }
            else
            {
                _transform.anchoredPosition = new Vector2((-.5f), (-1.5f) * (i +1));
            }
            _temp.GetComponent<Shop_Content>().indiceDoItem = i;
            _temp.GetComponent<Shop_Content>().Loja = this;
        }
    }
}
