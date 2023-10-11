using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HealthUserData
{
    //������ �̸�
    private string Name;
    //������ ����
    private bool Gender;
    //������ ����ID
    //������ ���� ���� �ִ��� �����ؾ��ϴµ� ���???
    private string UID;

    //�迭�� ���� ��ü ���� �ε����� Enum ����
    //������ ��ü ������ ����
    private int[] BodyLevelArray = new int[5];
    //������ ��ü ������ ���� ����ġ
    private int[] BodyExpArray = new int[5];
    //������ ��ü ������ �䱸 ����ġ
    private int[] BodyMaxExpArray = new int[5];
    //�ٽ� �����غ��� ������ �ø� ������ ��������?
    //������ ������ �� Ŭ�󿡼� �ؼ� �����͸� ���ε��ϴϱ�?
    public HealthUserData(string _name, bool _gender)
    {
        Name = _name;
        Gender = _gender;
        //UID�� �켱��ŵ
        //UID = 0;
    }
    public string returnUID()
    {
        return UID;
    }
}
