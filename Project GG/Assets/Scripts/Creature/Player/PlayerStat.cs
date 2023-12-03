using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    public override void SetStat(StatInfo status)
    {
        //레벨별 뒤에 더해주기
        Hp = status.hp;
        MaxHp = status.maxHp;
        Attack = status.attack;
        Defence = status.defence;
        MoveSpeed = status.moveSpeed;
    }

    //몸 레벨 관련 함수 정의 ex) 팔 레벨 5 이상 -> 사거리 증가 or 자연 회복 코루틴 등등
}
