using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HealthUserData
{
    //유저의 이름
    public string Name;
    //유저의 성별
    public bool Gender;
    //유저의 고유ID
    //서버에 같은 값이 있는지 참조해야하는데 어떻게???
    public string UID;

    //배열에 대한 신체 부위 인덱스는 Enum 참조
    //유저의 신체 부위별 레벨
    public int[] BodyLevelArray = new int[5];
    //유저의 신체 부위별 현재 경험치
    public int[] BodyExpArray = new int[5];
    //유저의 신체 부위별 요구 경험치
    public int[] BodyMaxExpArray = new int[5];
    //다시 생각해보니 서버에 올릴 이유가 없을지도?
    //레벨업 같은건 다 클라에서 해서 데이터만 업로드하니까?
    public HealthUserData(string _name, bool _gender, string _uid)
    {
        Name = _name;
        Gender = _gender;
        //UID는 우선스킵
        UID = _uid;
    }
    public string returnUID()
    {
        return UID;
    }
}
