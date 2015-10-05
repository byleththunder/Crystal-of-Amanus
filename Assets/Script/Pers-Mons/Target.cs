using UnityEngine;
using System.Collections;
using System;

public enum TargetVision { Front, Back, Left, Right };
[System.Serializable]

public class Target : MonoBehaviour
{
    //Variaveis
    public string Nome;
    public int Vida;
    public int VidaTotal;
    public int Amanus;
    public int AmanusTotal;
    public int Ataque;
    public TargetVision visao;
    public GameObject obj;
    public Skill[] Habilidades;
    //Métodos
    public void HealOrDamage(int _vida, int _amanus)
    {
        Vida -= _vida;
        if (Vida < 0)
        {
            Vida = 0;
        }
        Amanus -= _amanus;
        if (Amanus < 0)
        {
            Amanus = 0;
        }
        
    }

    








}
