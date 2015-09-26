using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonScript : MonoBehaviour {

    public List<Chest> Baus;
    //Controle de Dia - tarde - noite
    public Color CorManha;
    public Color CorTarde;
    public Color CorNoite;
    //
    public Light LuzPrincipal;
    public virtual void ChangeColorByStage()
    {
        
        Light temp = GameObject.FindObjectOfType<Light>();
        switch(Calendar.ActualStage)
        {
            case Calendar.StageOfTheDay.Manha:
                temp.color = CorManha;
                break;
            case Calendar.StageOfTheDay.Tarde:
                temp.color = CorTarde;
                break;
            case Calendar.StageOfTheDay.Noite:
                temp.color = CorNoite;
                break;
            default:
                Debug.LogError("Não é um estágio: "+Calendar.ActualStage);
                break;
        }
    }
    public virtual void Save()
    {
        
    }
    public virtual void Load()
    {

    }
	
}
