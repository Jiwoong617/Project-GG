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
    [SerializeField]
    private TMP_InputField idField;
    [SerializeField]
    private TMP_InputField pwField;

    private string path;
    private DatabaseReference reference;
    public void Register(string _id, string _pw)
    {
        if(CheckValidId(_id) == false)
        {
            // 유효하지 않은 아이디라고 알림, 기능 종료
            Debug.LogError("invalid id");
            return;
        }
        if(IsSameId(_id))
        {
            //같은 아이디가 있다고 알림, 기능 종료
            Debug.LogError("same id");
            return;
        }
        // 문제 없다면 회원가입, 완료 알림
        NewUserUserDataUpload(_id, _pw);
    }
    public void Login(string _id, string _pw)
    {
        //로그인 후 씬 전환 혹은 UI변환이 필요
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
                return false;
            }
        }
        return true;
    }
    public bool IsSameId(string _id)
    {
        if (_id == null) return true; // 아이디가 공백
        bool checker = false;
        IDictionary searchData = null;
        reference.Child("UserData").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot data in snapshot.Children)
                {
                    searchData = (IDictionary)data.Value;
                    string targetId = (string)searchData["Id"];
                    if (targetId == _id) checker = true;
                    else checker = false;
                }
            }
        });
        return checker;
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
    public IDictionary UserDataLoad(string _uid)
    {
        if (_uid == null)
        {
            Debug.LogError("UID is null");
            return null;
        }
        IDictionary outData = null;
        reference.Child("UserData").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot data in snapshot.Children)
                {
                    if (data.Key == _uid)
                        outData = (IDictionary)data.Value;
                }
            }
            Debug.Log(outData);
        });
        return outData;
    }
    private void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        path = Path.Combine(Application.dataPath, "uid.json");
    }
}
