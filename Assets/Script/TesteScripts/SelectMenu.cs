using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum TipoOpcoes { AtivarGameObject, Confirm, Back, Sair, SairTotalmente, AtivarScript };
public class SelectMenu : MonoBehaviour {
    public List<GameObject> obj;
    public SelectConteiner ConteinerPrefab;
    public List<Object> Destino;
    List<SelectConteiner> Opcoes;
    public List<string> NomeOpcoes;
    public List<TipoOpcoes> TipoDeOpcoes;
    public List<int> Sizes;
    public int size = 1;
	int indice = 0;
	int indicePassado = -1;
	// Use this for initialization
	void Start () {
        
		SetList();
	}
	
	// Update is called once per frame
	void Update () {
        if (this.enabled)
        {
            NavegarLista();
        }
	}
    void SetList()
    {
        if (NomeOpcoes.Count != 0)
        {
            Opcoes = new List<SelectConteiner>();
            for (int i = 0; i < NomeOpcoes.Count; i++)
            {
                SelectConteiner cont = Instantiate(ConteinerPrefab) as SelectConteiner;
                
                if (cont != null)
                {
                    Opcoes.Add(cont);

                    cont.transform.SetParent(gameObject.transform, false);
                    cont.name = NomeOpcoes[i];
                    cont.transform.localPosition = new Vector3(0, 40 - (50*i), 0);
                    cont.transform.localScale = new Vector3(1, 1, 1);
                    cont.OpcaoNome = NomeOpcoes[i];
                    cont.tipo = TipoDeOpcoes[i];
                    
                }
            }
        }
    }
	void NavegarLista()
	{
		if (indice > -1 && indice < Opcoes.Count)
		{
			if(Opcoes[indice].Painel != null)
			{
				Opcoes[indice].ChangeColor(true);
			}
			
			
		}
		if (indicePassado > -1 && indicePassado < Opcoes.Count)
		{
			Opcoes[indicePassado].ChangeColor(false);
		}
		
		if (Input.GetButtonDown("Action"))
		{

			if(Opcoes[indice].tipo == TipoOpcoes.AtivarGameObject)
			{
				((GameObject)Destino[indice]).SetActive(true);
				this.gameObject.SetActive(false);
				Opcoes[indice].ChangeColor(false);
				indice = 0;
				indicePassado = -1;
			}
			else if(Opcoes[indice].tipo == TipoOpcoes.Back)
			{
                print("Indice : " + indice);
				if(Destino[indice] != null){
                    ((GameObject)Destino[indice]).SetActive(true);
				}
				this.gameObject.SetActive(false);
				Opcoes[indice].ChangeColor(false);
				indice = 0;
				indicePassado = -1;
			}
			else if(Opcoes[indice].tipo == TipoOpcoes.Sair)
			{
                if (Destino[indice] != null)
                {
                    ((GameObject)Destino[indice]).SetActive(false);
                }
                Opcoes[indice].ChangeColor(false);
				
				
                
				indice = 0;
				indicePassado = -1;
			}
            else if (Opcoes[indice].tipo == TipoOpcoes.SairTotalmente)
            {
                if (Destino[indice] != null)
                {
                    ((GameObject)Destino[indice]).SetActive(false);
                }
                Opcoes[indice].ChangeColor(false);
                this.gameObject.SetActive(false);


                indice = 0;
                indicePassado = -1;
            }
            else if (Opcoes[indice].tipo == TipoOpcoes.AtivarScript)
            {
                if (Destino[indice] != null)
                {
                    ((MonoBehaviour)Destino[indice]).enabled = true;
                }
                Opcoes[indice].ChangeColor(false);
                this.gameObject.SetActive(false);


                indice = 0;
                indicePassado = -1;
            }
			else if(Opcoes[indice].tipo == TipoOpcoes.Confirm)
			{
                if (Destino[indice] != null)
                {
                    ((ISelectable)Destino[indice]).ConfirmButton(Opcoes[indice].OpcaoNome);
                }
                Opcoes[indice].ChangeColor(false);
                this.gameObject.SetActive(false);


                indice = 0;
                indicePassado = -1;
			}
		}

		if (Input.GetKeyDown(KeyCode.DownArrow) && indice < Opcoes.Count-1)
		{
			if (indicePassado != null)
			{
				indicePassado = indice;
			}
			indice++;
		}
		if (Input.GetKeyDown(KeyCode.UpArrow) && indice-1 > -1)
		{
			if (indicePassado != null)
			{
				indicePassado = indice;
			}
			indice--;
		}
	}
    void IrDestino()
    {
        if (Destino != null)
        {
            //Destino.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
