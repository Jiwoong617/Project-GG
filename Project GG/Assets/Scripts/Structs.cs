using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct StatInfo
{
    public int hp;
    public int maxHp;
    public int attack;
    public int defence;
    public float moveSpeed;

    public StatInfo(int hp, int maxHp, int attack, int defence, float moveSpeed)
    {
        this.hp = hp; 
        this.maxHp = maxHp;
        this.attack = attack;
        this.defence = defence;
        this.moveSpeed = moveSpeed;
    }
}

public struct RoutineStruct
{
    public string name;
    public int times;
    
    public RoutineStruct(string name, int times)
    {
        this.name = name;
        this.times = times;
    }
}
