using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    public override void SetStat(StatInfo status)
    {
        int[] bodyLevelArray = Manager.Health.myHealthData.BodyLevelArray;
        //레벨별 뒤에 더해주기
        Hp = status.hp + bodyLevelArray[2] + bodyLevelArray[3];
        MaxHp = status.maxHp + bodyLevelArray[2] + bodyLevelArray[3];
        Attack = status.attack + bodyLevelArray[1] * 2;
        Defence = status.defence + bodyLevelArray[0];
        MoveSpeed = status.moveSpeed + bodyLevelArray[4];
    }

    //몸 레벨 관련 함수 정의 ex) 팔 레벨 5 이상 -> 사거리 증가 or 자연 회복 코루틴 등등
}
