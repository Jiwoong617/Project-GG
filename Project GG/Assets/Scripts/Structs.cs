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
    public int _hp;
    public int _maxHp;
    public int _attack;
    public int _defence;
    public float _moveSpeed;
}
