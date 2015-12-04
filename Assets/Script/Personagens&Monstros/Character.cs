using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Scripts/Personagens e Monstros/Personagens Sripts/Characters")]
public class Character : Target {
    
    //Variaveis
    [Space(.5f)]
    [Header("Character")]
    [Space(.5f)]

   
    [Space(0.5f)]
    [HideInInspector]
    public Item[] Equipamentos = new Item[2];
    [Header("Velocidade de movimento do personagem")]
    [Range(1,10)]
    public float Speed;
    protected float moveX;
    protected float moveZ;
    [Header("Altura do Pulo do Personagem")]
    [Range(1, 10)]
    public float moveY;
    protected bool attack;
    protected bool DamageAttk = false;
    protected bool IsJump = false;
    [HideInInspector]
    public GameStates.CharacterState EstadoDoJogador = GameStates.CharacterState.Playing;
    [HideInInspector]
    public Vector3 CheckPointPosition;
    [HideInInspector]
    public Target Alvo;
    [HideInInspector]
    public int Gold;
    public long Divida = 100000000;
    //Métodos
    
    /// <summary>
    /// Método para Equipar armas e mudar os stats
    /// </summary>
    /// <param name="_Equipamento">Equipamento que deseja equipar</param>
    /// <returns>Retorna o equipamento que estava usando, caso não esteja usando nada, retorna nulo</returns>
    public Item Equipar(Item _Equipamento)
    {
        Item _temp = null;
      switch(_Equipamento.TipoDeEquipamentos)
      {
          case Item.EquipmentTypes.Arma:
              if (Equipamentos[0] != null) { _temp = Equipamentos[0]; }
              Equipamentos[0] = _Equipamento;
              break;
          case Item.EquipmentTypes.Armadura:
              if (Equipamentos[1] != null) { _temp = Equipamentos[1]; }
              Equipamentos[1] = _Equipamento;

              break;
      }
      AttEquipInfo(false);
      return _temp;
    }
    /// <summary>
    /// Aumenta os atributos do personagem com base no equipamento.
    /// </summary>
    /// <param name="sobreescrever">Se você quiser que um equipamento atualize os estatus do personagem, mesmo que ele esteja equipado coloque verdadeiro</param>
    public void AttEquipInfo(bool sobreescrever)
    {
        for (int i = 0; i < Equipamentos.Length; i++)
        {
            if (Equipamentos[i] != null)
            {
                if (!Equipamentos[i].IsEquip || sobreescrever)
                {
                    Ataque += Equipamentos[i].Ataque;
                    Vida += Equipamentos[i].Vida;
                    Amanus += Equipamentos[i].Amanus;
                    Equipamentos[i].IsEquip = true;
                }
            }
        }
    }
    /// <summary>
    /// Método que controla a movimentação do personagem
    /// </summary>
    public virtual void Movement()
    {

    }
    /// <summary>
    /// Método que controla o pulo do personagem
    /// </summary>
    /// <returns>Se retornar verdadeiro, o personagem pode pular, caso contrario o personagem está no ar</returns>
    public virtual bool Jump()
    {
        return false;
    }
    /// <summary>
    /// Método que controla o ataque do personagem
    /// </summary>
    public virtual void Attack()
    {

    }
    /// <summary>
    /// Quando o personagem morre ele realiza as ações descritas nesse método
    /// </summary>
    public virtual void DeathState()
    {

    }
    /// <summary>
    /// Quando o personagem decide continuar jogando, ele realiza as ações descritas nesse método
    /// </summary>
    public virtual void ReviveState()
    {

    }
    
    void OnParticleCollision(GameObject other) 
    {
        try
        {
            HealOrDamage(other.GetComponentInParent<Monster>().AtaqueAtual, 0);
            print(Vida);
        }
        catch
        {
            HealOrDamage(5, 0);
            print("Dano Padrão");
        }
        //print(other.transform.parent.name);
    }
    protected override void UpdateStatus()
    {
        base.UpdateStatus();
        AttEquipInfo(true);
    }
    
}
