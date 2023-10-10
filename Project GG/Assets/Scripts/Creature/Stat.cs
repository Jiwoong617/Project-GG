using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Stat : MonoBehaviour
{
    protected StatInfo stat;

    public int Hp { get { return stat._hp; } set { stat._hp = value; } }
    public int MaxHp { get { return stat._maxHp; } set { stat._maxHp = value; } }
    public int Attack { get { return stat._attack; } set { stat._attack = value; } }
    public int Defence { get { return stat._defence; } set { stat._defence = value; } }
    public float MoveSpeed { get { return stat._moveSpeed; } set { stat._moveSpeed = value; } }

    public virtual void TakeDamage(Stat attacker)
    {
        int damage = attacker.Attack;
        Hp -= damage;

        if (Hp <= 0)
        {
            Hp = 0;
            OnDead();
        }
    }

    public virtual void OnDead()
    {
        Debug.Log($"{gameObject.name} dead");

        //gameObject.SetActive(false);
    }

    public void SetStat(int hp, int atk, int dfc, float speed)
    {
        Hp = hp;
        MaxHp = hp;
        Attack = atk;
        Defence = dfc;
        MoveSpeed = speed;
    }
}

