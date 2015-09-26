using UnityEngine;
using System.Collections;

public class Calendar : MonoBehaviour {
    public enum StageOfTheDay { Manha = 0, Tarde = 1, Noite = 2 };
    //Periodo
    static StageOfTheDay Stage = StageOfTheDay.Manha;
    public static StageOfTheDay ActualStage { get { return Stage; } }
    //Dia
    static int Days = 1;
    public static int TotalDaysInAWeek { get { return 10; } }
    public static int TotalDaysInAMonth { get { return 30; } }
    public static int TotalDaysInAYear { get { return 360; } }
    public static int ActualDay { get { return Days; } }
    //Mês
    static int Mounth = 1;
    public static int TotalMonthInAYear { get { return 12; } }
    public static int ActualMounth { get { return Mounth; } }
    //Ano
    static int Year = 1;
    public static int ActualYear { get { return Year; } }
    //Métodos
    public static void IncreaseStage(int HowMuch)
    {
        int estagio = 0;
        switch(Stage)
        {
            case StageOfTheDay.Manha:
                estagio = 0;
                break;
            case StageOfTheDay.Tarde:
                estagio = 1;
                break;
            case StageOfTheDay.Noite:
                estagio = 2;
                break;
        }

        if(estagio + HowMuch > 2)
        {
            int diferenca = (estagio+HowMuch)%3;
            switch (diferenca) { case 0: Stage = StageOfTheDay.Manha; break; case 1: Stage = StageOfTheDay.Tarde; break; case 2: Stage = StageOfTheDay.Noite; break; }
            int aumentarDias = (estagio + HowMuch) / 2;
            Days += aumentarDias;
            if(Days >30)
            {
                Days = 1;
                Mounth++;
                if(Mounth >12)
                {
                    Mounth = 1;
                    Year++;
                }
            }
        }
        else { Stage += HowMuch; }
        
        
    }
    public static void IncreaseDay(int HowMuch)
    {
        Stage = 0;
        Days += HowMuch;
        if (Days > 30)
        {
            Days = 1;
            Mounth++;
            if (Mounth > 12)
            {
                Mounth = 1;
                Year++;
            }
        }
    }

    public static void SaveCalendar(int i)
    {
        PlayerPrefs.SetInt("Dia" + i, Days);
        PlayerPrefs.SetInt("Mes" + i, Mounth);
        PlayerPrefs.SetInt("Ano" + i, Year);
        PlayerPrefs.SetString("Periodo" + i, Stage.ToString());
    }
    public static void LoadCalendar(int i)
    {
        Days = PlayerPrefs.GetInt("Dia" + i);
        Mounth = PlayerPrefs.GetInt("Mes" + i);
        Year = PlayerPrefs.GetInt("Ano" + i);
        switch(PlayerPrefs.GetString("Periodo" + i))
        {
            case "Manha":
                Stage = StageOfTheDay.Manha;
                break;
            case "Tarde":
                Stage = StageOfTheDay.Tarde;
                break;
            case "Noite":
                Stage = StageOfTheDay.Noite;
                break;
        }
    }

}
