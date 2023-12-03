using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

using Newtonsoft.Json;
using Firebase;
using Firebase.Database;
using Firebase.Unity;
using System;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;
using Newtonsoft.Json.Linq;

public class FBManager : MonoBehaviour
{
    public string path;
    private DatabaseReference reference;
    public async Task Register(string _id, string _pw)
    {
        if(CheckValidId(_id) == false)
        {
            // 유효하지 않은 아이디라고 알림, 기능 종료
            Debug.LogError("invalid id");
            return;
        }
        bool checker = await IsSameId(_id);
        if (checker)
        {
            //같은 아이디가 있다고 알림, 기능 종료
            Debug.LogError("same id");
            return;
        }
        // 문제 없다면 회원가입, 완료 알림
        NewUserDataUpload(_id, _pw);
        Debug.Log("register success");
    }
    public async Task Login(string _id, string _pw)
    {
        IDictionary r_idict = null;
        r_idict = await UserDataLoad(DataVarType.Id, _id);
        Debug.Log(r_idict);
        if(r_idict == null)
        {
            // 회원가입이 되지 않은 Id, 회원가입 알림 띄우기
            return;
        }
        string inputPw = (string)r_idict[DataVarType.Pw.ToString()];
        Debug.Log(inputPw + ", "+ _pw);
        if(inputPw == _pw)
        {
            // 비밀번호가 같으면 로그인, 화면 전환, uid토큰 값 로컬 저장
            string uid = (string)r_idict[DataVarType.UID.ToString()];
            // json으로 변환
            string uidJson = JsonConvert.SerializeObject(uid);
            // 로컬에 저장
            File.WriteAllText(path, uidJson);
            Debug.Log("Login success");
        }
        else
        {
            // 다르다면 오류 메세지
            return;
        }
    }
    public bool CheckValidId(string _id)
    {
        string invalidString = "`~!@#$%^&*()-_=+[]{},./<>?;:\'\"\\| ";
        char[] splitedId = _id.ToCharArray();
        int idSize = splitedId.Length;
        for(int i = 0; i < idSize; i++)
        {
            if (invalidString.Contains(splitedId[i]))
            {
                // 특수문자 사용불가 알림
                Debug.LogError("can't use special symbols");
                return false;
            }
        }
        if(_id == null || _id =="")
        {
            // 공백 아이디 사용불가 알림
            Debug.LogError("can't use blank Id");
            return false;
        }
        return true;
    }
    public async Task<bool> IsSameId(string _id)
    {
        bool r_checker = false;
        IDictionary searchData = null;
        if (reference == null) reference = FirebaseDatabase.DefaultInstance.RootReference; // 왜 레퍼런스가 null로 뜨지?
        await reference.Child("UserData").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot data in snapshot.Children)
                {
                    searchData = (IDictionary)data.Value;
                    string targetId = (string)searchData["Id"];
                    if (targetId == _id) r_checker = true;
                }
            }
        });
        return r_checker;
    }
    public void NewUserDataUpload(string _id, string _pw)
    {
        string uid = Guid.NewGuid().ToString(); // 새로운 uid를 생성

        string name = null;
        string gender = Gender.Man.ToString();
        HealthUserData newUserData = new HealthUserData(name, gender, uid, _id, _pw);
        string json = JsonConvert.SerializeObject(newUserData);
        reference.Child("UserData").Child(newUserData.returnUID()).SetRawJsonValueAsync(json);
    }
    public async void UserDataEdit(DataVarType _type, string _value, string _uid)
    {
        IDictionary r_idict = null;
        if (reference == null) reference = FirebaseDatabase.DefaultInstance.RootReference;
        await reference.Child("UserData").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot data in snapshot.Children)
                {
                    if(data.Key == _uid)
                        r_idict = (IDictionary)data.Value;
                }
            }
        });
        // string은 참조형이라 작성함, 문제 있는지 확인 안함
        string id = (string)r_idict[DataVarType.Id.ToString()];
        string pw = (string)r_idict[DataVarType.Pw.ToString()];
        string name = (string)r_idict[DataVarType.Name.ToString()];
        string gender = (string)r_idict[DataVarType.Gender.ToString()];
        string uid = (string)r_idict[DataVarType.UID.ToString()];

        string targetValue;
        switch(_type)
        {
            case DataVarType.Id:
                targetValue = id;
                break;
            case DataVarType.Pw:
                targetValue = pw;
                break;
            case DataVarType.Name:
                targetValue = name;
                break;
            case DataVarType.Gender:
                targetValue = gender;
                break;
            case DataVarType.UID:
                targetValue = uid;
                break;
        }
        targetValue = _value;
        HealthUserData targetData = new HealthUserData(name, gender, uid, id, pw);
        string json = JsonConvert.SerializeObject(targetData);
        await reference.Child("UserData").Child(targetData.returnUID()).SetRawJsonValueAsync(json);
    }
    public async void UserDataEdit(DataVarType _type, int[] _value, string _uid)
    {
        IDictionary r_idict = null;
        if (reference == null) reference = FirebaseDatabase.DefaultInstance.RootReference;
        await reference.Child("UserData").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot data in snapshot.Children)
                {
                    if (data.Key == _uid)
                        r_idict = (IDictionary)data.Value;
                }
            }
        });
        if (r_idict == null) Debug.LogError("data search failed");
        // string은 참조형이라 작성함, 문제 있는지 확인 안함
        string id = (string)r_idict[DataVarType.Id.ToString()];
        string pw = (string)r_idict[DataVarType.Pw.ToString()];
        string name = (string)r_idict[DataVarType.Name.ToString()];
        string gender = (string)r_idict[DataVarType.Gender.ToString()];
        string uid = (string)r_idict[DataVarType.UID.ToString()];

        HealthUserData targetData = new HealthUserData(name, gender, uid, id, pw);
        targetData.arrayEdit(_type, _value);
        string json = JsonConvert.SerializeObject(targetData);
        await reference.Child("UserData").Child(targetData.returnUID()).SetRawJsonValueAsync(json);
    }
    public async Task<IDictionary> UserDataLoad(DataVarType _type, string _value)
    {
        IDictionary r_idict = null;
        if(_type == DataVarType.UID)
        {
            if (_value == null)
            {
                Debug.LogError("UID is null");
                return null;
            }
        }
        if(reference == null) reference = FirebaseDatabase.DefaultInstance.RootReference;
        await reference.Child("UserData").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot data in snapshot.Children)
                {
                    IDictionary targetData = (IDictionary)data.Value;
                    string targetValue = (string)targetData[_type.ToString()];
                    if (targetValue == _value) r_idict = targetData;
                }
            }
        });
        return r_idict;
    }
    private void Awake()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        path = Path.Combine(Application.dataPath, "uid.json");
    }
}
