using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ChangeButton : Selectable, IDeselectHandler
{

    public InputsName Botao;
    public bool Axis = false;
    public bool Negativo = false;
    [Space(1)]
    public Text Texto;
    private bool Selecionado = false;
    
    void Update()
    {
        if (!Application.isPlaying) return;
        if (Selecionado)
        {
            Texto.text = "...";
            if (Axis)
            {
                Selecionado = !GameInput.ChangeAxis(Botao, Negativo);
            }
            else
            {
                Selecionado = !GameInput.ChangeKey(Botao);
            }
        }
        else
        {
            if (Axis)
            {
                Texto.text = GameInput.GetAxisName(Botao, Negativo);
            }
            else
            {
                Texto.text = GameInput.GetKeyName(Botao);
            }
        }
    }

    public void Clicou()
    {
        Selecionado = true;
    }


    void IDeselectHandler.OnDeselect(BaseEventData eventData)
    {
        Selecionado = false;
    }
    [ContextMenu("Criar ou Adicionar Texto")]
    public void GetOrCreateChildText()
    {
        Text temp = transform.FindChild("Text").GetComponent<Text>();
        if(temp!=null)
        {
            Texto = temp;
        }else
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(transform, false);
            obj.AddComponent<Text>();
            obj.name = "Texto";
            Texto = obj.GetComponent<Text>();
            
        }
    }
}
