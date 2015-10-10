using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Scripts/Dungeon Scripts/DungeonScript")]
public class DungeonScript : MonoBehaviour {

    /// <summary>
    /// Nesse script que o calendário vai influenciar. A luz principal vai iluminar a cena com a cor do periodo do dia.
    /// </summary>

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
