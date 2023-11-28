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
    public bool Gender;
    //������ ����ID
    public string UID;

    //�迭�� ���� ��ü ���� �ε����� Enum ����
    //������ ��ü ������ ����
    public int[] BodyLevelArray = new int[5];
    //������ ��ü ������ ���� ����ġ
    public int[] BodyExpArray = new int[5];
    //������ ��ü ������ �䱸 ����ġ
    public int[] BodyMaxExpArray = new int[5];
    //�ٽ� �����غ��� ������ �ø� ������ ��������?
    //������ ������ �� Ŭ�󿡼� �ؼ� �����͸� ���ε��ϴϱ�?
    public HealthUserData(string _name, bool _gender, string _uid, string id, string pw)
    {
        Name = _name;
        Gender = _gender;
        //UID�� �켱��ŵ
        UID = _uid;
        Id = id;
        Pw = pw;
    }
    public string returnUID()
    {
        return UID;
    }
}
