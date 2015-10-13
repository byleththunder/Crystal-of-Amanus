using UnityEngine;
using System.Collections;
using System;

public enum TargetVision { Front, Back, Left, Right };

[AddComponentMenu("Scripts/Personagens e Monstros/Target",0)]
public class Target : MonoBehaviour
{
    //Variaveis
    [Range(1,100)]
    public int Level = 1;
    [Header("Nome do Personagem")]
    public string Nome;
    protected int Vida;
    [Header("Vida maxíma no Level 1")]
    public int VidaTotal;
    protected int Amanus;
    [Header("Amanus maxíma no Level 1")]
    public int AmanusTotal;
    protected int Ataque;
    [Header("Ataque básico (sem valores adicionais) no Level 1")]
    [Tooltip("Valores adicionais só vão existir em personagens, pois eles possuem equipamentos que podem aumentar os status (ATK,VP,AP)")]
    public int AtaquePadrao;
    protected int ExpPadrao;
    protected int Exp;
    [HideInInspector]
    public TargetVision visao;
    [HideInInspector]
    public GameObject obj;
    [HideInInspector]
    public Skill[] Habilidades;
    [Header("Particula que da um Feedback quando o joggador passa de nivel")]
    public ParticleSystem LvlUpAnim;
    ///<summary "Propriedades">
    ///Estou usando propriedades nessa classe somente para passar 
    ///valores de variaveis que eu não quero que sejam alteradas diretamente.
    ///</summary>
    
    public int VidaAtual { get { return Vida; } }
    public int AmanusAtual { get { return Amanus; } }
    public int AtaqueAtual { get { return Ataque; } }
    public int ExperienciaAtual { get { return Exp; } }
    public int NextLevel { get { return (Level == 1? 100:((int)(NextLevel+(NextLevel*0.1)))); } }
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
    protected void LevelUp(int _exp)
    {
        Exp += _exp;
      
        if(Exp >= NextLevel)
        {
            Level++;
            Exp = 0;
            UpdateStatus();
            if(LvlUpAnim!= null)
            {
                LvlUpAnim.emissionRate = 10;
                Invoke("LvlUpAnimOff", 1);
            }
        }
        

    }
    protected virtual void UpdateStatus()
    {
        VidaTotal = VidaTotal * Level;
        Vida = VidaTotal;
        AmanusTotal *= Level;
        Amanus = AmanusTotal;
        Ataque = AtaquePadrao * Level;
    }
    void LvlUpAnimOff()
    {
        LvlUpAnim.emissionRate = 0;
    }
    
    
    








}
