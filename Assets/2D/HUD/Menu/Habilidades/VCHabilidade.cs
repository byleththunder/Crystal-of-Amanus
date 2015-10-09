using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class VCHabilidade : MonoBehaviour,IDeselectHandler,ISelectHandler {

    public bool IsSeletec = false;

    public void Selecionar()
    {
        //IsSeletec = true;
    }
    public void OnSelect(BaseEventData eventData)
    {
        print("Selecionou");
        IsSeletec = true;
    }
    public void OnDeselect(BaseEventData data)
    {
        print("Deselecionou");
        StartCoroutine(Esperar());
    }
    IEnumerator Esperar()
    {
        yield return new WaitForSeconds(0.2f);
        IsSeletec = false;
    }
	
}
