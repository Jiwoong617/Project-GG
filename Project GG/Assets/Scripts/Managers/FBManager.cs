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

public class FBManager : MonoBehaviour
{
    private string path;
    private DatabaseReference reference;
    private bool checker;
    public async Task Register(string _id, string _pw)
    {
        checker = false;
        if(CheckValidId(_id) == false)
        {
            // 유효하지 않은 아이디라고 알림, 기능 종료
            Debug.LogError("invalid id");
            return;
        }
        await IsSameId(_id);
        if (checker)
        {
            //같은 아이디가 있다고 알림, 기능 종료
            Debug.LogError("same id");
            return;
        }
        // 문제 없다면 회원가입, 완료 알림
        NewUserUserDataUpload(_id, _pw);
        Debug.Log("register success");
    }
    public async Task Login(string _id, string _pw)
    {
        IDictionary data = null;
        Task myTask = UserDataLoad(dataVarType.Id, _id, data);
        await myTask;
        Debug.Log(data);
        if(data == null)
        {
            // 회원가입이 되지 않은 Id, 회원가입 알림 띄우기
            return;
        }
        string inputPw = (string)data[dataVarType.Pw.ToString()];
        Debug.Log(inputPw + ", "+ _pw);
        if(inputPw == _pw)
        {
            // 비밀번호가 같으면 로그인, 화면 전환
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
    public async Task IsSameId(string _id)
    {
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
                    if (targetId == _id) checker = true;
                }
            }
        });
        return;
    }
    public void NewUserUserDataUpload(string _id, string _pw)
    {
        string uid = Guid.NewGuid().ToString(); // 새로운 uid를 생성
        //// json으로 변환
        //string uidJson = JsonConvert.SerializeObject(uid);
        //// 로컬에 저장
        //File.WriteAllText(path, uidJson);

        string name = null;
        bool gender = false;
        HealthUserData newUserData = new HealthUserData(name, gender, uid, _id, _pw);
        string json = JsonConvert.SerializeObject(newUserData);
        reference.Child("UserData").Child(newUserData.returnUID()).SetRawJsonValueAsync(json);
    }
    public async Task UserDataLoad(dataVarType _type, string _value, IDictionary _data)
    {
        if(_type == dataVarType.UID)
        {
            if (_value == null)
            {
                Debug.LogError("UID is null");
                return;
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
                    if (targetValue == _value) _data = targetData;
                }
            }
        });
        Debug.Log(_data);
        return;
    }
    private void Awake()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        path = Path.Combine(Application.dataPath, "uid.json");
    }
}
