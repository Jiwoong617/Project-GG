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
            // ��ȿ���� ���� ���̵��� �˸�, ��� ����
            Debug.LogError("invalid id");
            return;
        }
        await IsSameId(_id);
        if (checker)
        {
            //���� ���̵� �ִٰ� �˸�, ��� ����
            Debug.LogError("same id");
            return;
        }
        // ���� ���ٸ� ȸ������, �Ϸ� �˸�
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
            // ȸ�������� ���� ���� Id, ȸ������ �˸� ����
            return;
        }
        string inputPw = (string)data[dataVarType.Pw.ToString()];
        Debug.Log(inputPw + ", "+ _pw);
        if(inputPw == _pw)
        {
            // ��й�ȣ�� ������ �α���, ȭ�� ��ȯ
            Debug.Log("Login success");
        }
        else
        {
            // �ٸ��ٸ� ���� �޼���
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
                // Ư������ ���Ұ� �˸�
                Debug.LogError("can't use special symbols");
                return false;
            }
        }
        if(_id == null || _id =="")
        {
            // ���� ���̵� ���Ұ� �˸�
            Debug.LogError("can't use blank Id");
            return false;
        }
        return true;
    }
    public async Task IsSameId(string _id)
    {
        IDictionary searchData = null;
        if (reference == null) reference = FirebaseDatabase.DefaultInstance.RootReference; // �� ���۷����� null�� ����?
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
        string uid = Guid.NewGuid().ToString(); // ���ο� uid�� ����
        //// json���� ��ȯ
        //string uidJson = JsonConvert.SerializeObject(uid);
        //// ���ÿ� ����
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
