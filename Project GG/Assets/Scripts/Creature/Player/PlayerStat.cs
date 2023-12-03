using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    public override void SetStat(StatInfo status)
    {
        int[] bodyLevelArray = Manager.Health.myHealthData.BodyLevelArray;
        //������ �ڿ� �����ֱ�
        Hp = status.hp + bodyLevelArray[2] + bodyLevelArray[3];
        MaxHp = status.maxHp + bodyLevelArray[2] + bodyLevelArray[3];
        Attack = status.attack + bodyLevelArray[1] * 2;
        Defence = status.defence + bodyLevelArray[0];
        MoveSpeed = status.moveSpeed + bodyLevelArray[4];
    }

    //�� ���� ���� �Լ� ���� ex) �� ���� 5 �̻� -> ��Ÿ� ���� or �ڿ� ȸ�� �ڷ�ƾ ���
}
