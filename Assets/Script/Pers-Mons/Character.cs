using UnityEngine;
using System.Collections;

public class Character : Target {
    
    //Variaveis
    public Item[] Equipamentos = new Item[2];
    public float Speed;
    public float moveX;
    public float moveZ;
    public float moveY;
    public bool attack;
    public bool notJump = true;
    public GameStates.CharacterState EstadoDoJogador = GameStates.CharacterState.Playing;
    public Vector3 CheckPointPosition;
    public Target Alvo;
    //Temporario - Esperando animação 2D de defesa
    public GameObject Escudo;
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
      return _temp;
    }
    public void AttEquipInfo()
    {
        for (int i = 0; i < Equipamentos.Length; i++)
        {
            if (Equipamentos[i] != null)
            {
                if (!Equipamentos[i].IsEquip)
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
    
}
