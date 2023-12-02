using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HealthUserData
{
    //������ ���̵�
    public string Id;
    //������ ��й�ȣ
    public string Pw;
    //������ �̸�
    public string Name;
    //������ ����
    public string Gender;
    //������ ����ID
    public string UID;

    //�迭�� ���� ��ü ���� �ε����� Enum ����
    //������ ��ü ������ ����
    public int[] BodyLevelArray = new int[5] { 1, 1, 1, 1, 1 };
    //������ ��ü ������ ���� ����ġ
    public int[] BodyExpArray = new int[5];
    //������ ��ü ������ �䱸 ����ġ
    public int[] BodyMaxExpArray = new int[5] { 100, 100, 100, 100, 100 };
    //�ٽ� �����غ��� ������ �ø� ������ ��������?
    //������ ������ �� Ŭ�󿡼� �ؼ� �����͸� ���ε��ϴϱ�?
    public HealthUserData(string _name, string _gender, string _uid, string id, string pw)
    {
        Name = _name;
        Gender = _gender;
        UID = _uid;
        Id = id;
        Pw = pw;
    }
    public void arrayEdit(dataVarType _type, int[] _value)
    {
        switch (_type)
        {
            case dataVarType.BodyLevelArray:
                BodyLevelArray = _value;
                break;
            case dataVarType.BodyExpArray:
                BodyExpArray = _value;
                break;
            case dataVarType.BodyMaxExpArray:
                BodyMaxExpArray = _value;
                break;
        }
        // ������ ���
        int arraySize = BodyLevelArray.Length;
        for(int i = 0; i< arraySize; i++)
        {
            if (BodyExpArray[i] >= BodyMaxExpArray[i])
            {
                BodyLevelArray[i] += 1;
                BodyExpArray[i] -= BodyMaxExpArray[i];
            }
        }
    }
    public string returnUID()
    {
        return UID;
    }
}
