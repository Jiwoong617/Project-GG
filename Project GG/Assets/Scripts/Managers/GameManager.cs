using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }


    private void Awake()
    {
        if(instance == null)
            instance = this;

        Init();
    }

    private void Init()
    {
        // ���� ����
        //���߿� find with tags�� ã�� �͵� �� global�� �����ϰ� �ٲܰ���

        // ������ �������� ���� ��������
        // �÷��̾� ����
        // �� ����
        
        // --- ���� ���� �� ī��Ʈ �ٿ� or �ƽ� ����

        // ���̽�ƽ ����
        // ������ ����
        // UI ����
    }
}
