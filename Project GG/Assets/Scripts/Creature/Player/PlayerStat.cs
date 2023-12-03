using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    public override void SetStat(StatInfo status)
    {
        //������ �ڿ� �����ֱ�
        Hp = status.hp;
        MaxHp = status.maxHp;
        Attack = status.attack;
        Defence = status.defence;
        MoveSpeed = status.moveSpeed;
    }

    //�� ���� ���� �Լ� ���� ex) �� ���� 5 �̻� -> ��Ÿ� ���� or �ڿ� ȸ�� �ڷ�ƾ ���
}
