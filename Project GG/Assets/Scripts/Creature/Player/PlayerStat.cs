using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    PlayerStat bodyInfo;


    public void SetStat()
    {
        int hp = 0; //부위에 따른 값 수정
        int atk = 0;
        int dfc = 0;
        float speed = 0;
        //base.SetStat(hp, atk, dfc, speed);
    }

    //몸 레벨 관련 함수 정의 ex) 팔 레벨 5 이상 -> 사거리 증가 or 자연 회복 코루틴 등등
}
