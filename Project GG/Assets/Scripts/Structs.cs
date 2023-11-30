using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlayerBody
{
    public int shoulder;
    //여기에 근육 추가
}

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

public struct RoutineJson
{
    public string name;
    public int times;
}
