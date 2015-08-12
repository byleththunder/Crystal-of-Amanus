using UnityEngine;
using System.Collections;
using System;

public enum TargetVision {Front, Back, Left, Right};
public interface ITarget {

     string Nome { get; }
	//
     int Vida { get; set; }
	//
     int VidaTotal { get;}
	//
     int Amanus { get; set; }
	//
     int AmanusTotal { get; }
	//
     int Ataque { get; }

     TargetVision visao { get; set; }

     void StatsChange (int _vida, int _amanus);

     GameObject obj { get; }

     Item[] Equipamentos { get; set; }
    





}
