using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Stat
{
    protected StatInfo stat;

    public int Hp { get { return stat.hp; } set { stat.hp = value; } }
    public int MaxHp { get { return stat.maxHp; } set { stat.maxHp = value; } }
    public int Attack { get { return stat.attack; } set { stat.attack = value; } }
    public int Defence { get { return stat.defence; } set { stat.defence = value; } }
    public float MoveSpeed { get { return stat.moveSpeed; } set { stat.moveSpeed = value; } }

    public virtual void TakeDamage(Stat attacker)
    {
        int damage = attacker.Attack;
        Hp -= damage;

        if (Hp <= 0)
            Hp = 0;
    }

    public void SetStat(StatInfo status)
    {
        Hp = status.hp;
        MaxHp = status.maxHp;
        Attack = status.attack;
        Defence = status.defence;
        MoveSpeed = status.moveSpeed;
    }
}

