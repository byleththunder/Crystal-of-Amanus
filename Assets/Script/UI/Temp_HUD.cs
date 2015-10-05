using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Temp_HUD : MonoBehaviour
{
    Target Personagem;
    //Barras
    public Image X_Vida, X_Amanus, X_EXP;
    public Text Vida, Amanus;
    //Sprites Sheets
    public Sprite[] Numeros = new Sprite[9];
    public Sprite[] Semanas = new Sprite[4];
    public Sprite[] Stages = new Sprite[3];
    //Numero do Dia
    public Image Dia;
    //Numero do Mês
    public Image Mes;
    //Semana
    public Image Semana;
    //Icone das Fases do Dia
    public Image Ico;
    //Ponteiro [65,0,-65]
    public RectTransform Ponteiro;
    // Calendario
    
    // Use this for initialization
    void Start()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if (obj != null)
        {
            //print("OBJ: " + (obj.name));
            Personagem = (Target)obj.GetComponent(typeof(Target));
            //print("Personagem: " + (Personagem != null));
        }
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (Personagem != null)
        {

            float _Vida = (float)Personagem.Vida / (float)Personagem.VidaTotal;
            float _Amanus = (float)Personagem.Amanus / (float)Personagem.AmanusTotal;
            _Amanus = Mathf.Clamp(_Amanus, 0, 1);
            _Vida = Mathf.Clamp(_Vida, 0, 1);
            X_Vida.fillAmount = _Vida;
            X_Amanus.fillAmount = _Amanus;
            Vida.text = Personagem.Vida + " / " + Personagem.VidaTotal;
            Amanus.text = Personagem.Amanus + " / " + Personagem.AmanusTotal;
        }
        PonteiroMove();
        OrganizarCalendario();

    }
    void PonteiroMove()
    {
        switch(Calendar.ActualStage)
        {
            case Calendar.StageOfTheDay.Manha:
                Ponteiro.localEulerAngles = (new Vector3(0, 0, 65));
                break;
            case Calendar.StageOfTheDay.Tarde:
                Ponteiro.localEulerAngles = (new Vector3(0, 0, 0));
                break;
            case Calendar.StageOfTheDay.Noite:
                Ponteiro.localEulerAngles = (new Vector3(0, 0, -65));
                break;
        }
    }
    void OrganizarCalendario()
    {
        Dia.sprite = IntegerToImage(Calendar.ActualDay);
        Mes.sprite = IntegerToImage(Calendar.ActualMounth);
        Semana.sprite = SemanaToImage(Calendar.ActualWeek);
        Ico.sprite = StageToImage(Calendar.ActualStage);

    }
    Sprite IntegerToImage(int i)
    {
        Sprite img = null;
        switch(i)
        {
            case 1:
                img = Numeros[0];
                break;
            case 2:
                img = Numeros[1];
                break;
            case 3:
                img = Numeros[2];
                break;
            case 4:
                img = Numeros[3];
                break;
            case 5:
                img = Numeros[4];
                break;
            case 6:
                img = Numeros[5];
                break;
            case 7:
                img = Numeros[6];
                break;
            case 8:
                img = Numeros[7];
                break;
            case 9:
                img = Numeros[8];
                break;

        }
        return img;
    }
    Sprite SemanaToImage(int i)
    {
        Sprite img = null;
        switch (i)
        {
            case 1:
                img = Semanas[0];
                break;
            case 2:
                img = Semanas[1];
                break;
            case 3:
                img = Semanas[2];
                break;
            case 4:
                img = Semanas[3];
                break;
            default:
                break;

        }
        return img;
    }
    Sprite StageToImage(Calendar.StageOfTheDay i)
    {
        Sprite img = null;
        switch (i)
        {
            case Calendar.StageOfTheDay.Manha:
                img = Stages[0];
                break;
            case Calendar.StageOfTheDay.Tarde:
                img = Stages[1];
                break;
            case Calendar.StageOfTheDay.Noite:
                img = Stages[2];
                break;
            
            default:
                break;

        }
        return img;
    }
    
}
