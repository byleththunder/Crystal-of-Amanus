using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

[AddComponentMenu("Scripts/VisualScripts/Visual Contents/Habilidades")]
public class VCHabilidade : MonoBehaviour,IDeselectHandler,ISelectHandler {

    /// <summary "Como Funciona?">
    /// Esse script só serve para saber qual habilidade eu selecionei.
    /// O método esperar, serve para que quando eu clicar slot que eu quero, o botão não deselecionar imediatamento, dando tempo para o outro
    /// script ver o estado desse botão.
    /// </summary>

    public bool IsSeletec = false;

    public void Selecionar()
    {
        //IsSeletec = true;
    }
    public void OnSelect(BaseEventData eventData)
    {
        IsSeletec = true;
    }
    public void OnDeselect(BaseEventData data)
    {
        StartCoroutine(Esperar());
    }
    IEnumerator Esperar()
    {
        yield return new WaitForSeconds(0.2f);
        IsSeletec = false;
    }
	
}
