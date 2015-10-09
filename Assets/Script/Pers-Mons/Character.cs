using UnityEngine;
using System.Collections;
[AddComponentMenu("Scripts/TargetScript/CharacterScript/Characters")]
public class Character : Target {
    
    //Variaveis
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
    protected bool notJump = true;
    [HideInInspector]
    public GameStates.CharacterState EstadoDoJogador = GameStates.CharacterState.Playing;
    [HideInInspector]
    public Vector3 CheckPointPosition;
    [HideInInspector]
    public Target Alvo;
    [HideInInspector]
    public int Gold;
    
    //Métodos
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
    public virtual void Movement()
    {

    }
    public virtual bool Jump()
    {
        return false;
    }
    public virtual void Attack()
    {

    }
    public virtual void DeathState()
    {

    }
    public virtual void ReviveState()
    {

    }
    void OnParticleCollision(GameObject other) 
    {
        try
        {
            HealOrDamage(other.GetComponentInParent<Monster>().AtaqueAtual, 0);
        }
        catch
        {
            HealOrDamage(1, 0);
        }
    }
    protected override void UpdateStatus()
    {
        base.UpdateStatus();
        AttEquipInfo(true);
    }
    
}
