using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Calendar Script/Calendario")]
public class Calendar : MonoBehaviour {

    ///<summary "Como Funciona ?">
    ///Esse calendário é baseado no calendário da revolução francesa.
    ///Ele possui 360 dias no ano, 30 dias no mês e 10 dias em uma semana.
    ///Ele tem 4 semanas e 10 meses.
    ///</summary>
    
    ///<summary "Para quê serve ?">
    ///Com o passar do tempo os periodos do dia vão mudando, e eles influenciam na iluminação da cena.
    ///O 10 dia da semana, é um dia de descanço, aonde o jogador não pode completar nenhuma quest.
    ///</summary>

    //Enum
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
    //Semana
    static int Week = 1;
    public static int ActualWeek { get { return Week; } }
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
            Week = (Days + 10) / 10;
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
        Week = (Days + 10) / 10;
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

    public static void  SetCalendar(int Dia, int Mes, int Ano, StageOfTheDay Periodo)
    {
        Days = Dia;
        Week = (Days + 10) / 10;
        Mounth = Mes;
        Year = Ano;
        Stage = Periodo;
    }
    

}
