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
        NewUserDataUpload(_id, _pw);
        Debug.Log("register success");
    }
    public async Task Login(string _id, string _pw)
    {
        IDictionary Idict = Manager.Idict;
        Idict = null;
        await UserDataLoad(DataVarType.Id, _id);
        Debug.Log(Idict);
        if(Idict == null)
        {
            // ȸ�������� ���� ���� Id, ȸ������ �˸� ����
            return;
        }
        string inputPw = (string)Idict[DataVarType.Pw.ToString()];
        Debug.Log(inputPw + ", "+ _pw);
        if(inputPw == _pw)
        {
            // ��й�ȣ�� ������ �α���, ȭ�� ��ȯ, uid��ū �� ���� ����
            string uid = (string)Idict[DataVarType.UID.ToString()];
            // json���� ��ȯ
            string uidJson = JsonConvert.SerializeObject(uid);
            // ���ÿ� ����
            File.WriteAllText(path, uidJson);
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
    public void NewUserDataUpload(string _id, string _pw)
    {
        string uid = Guid.NewGuid().ToString(); // ���ο� uid�� ����

        string name = null;
        string gender = Gender.Man.ToString();
        HealthUserData newUserData = new HealthUserData(name, gender, uid, _id, _pw);
        string json = JsonConvert.SerializeObject(newUserData);
        reference.Child("UserData").Child(newUserData.returnUID()).SetRawJsonValueAsync(json);
    }
    public async void UserDataEdit(DataVarType _type, string _value, string _uid)
    {
        IDictionary Idict = Manager.Idict;
        Idict = null;
        if (reference == null) reference = FirebaseDatabase.DefaultInstance.RootReference;
        await reference.Child("UserData").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot data in snapshot.Children)
                {
                    if(data.Key == _uid)
                        Idict = (IDictionary)data.Value;
                }
            }
        });
        // string�� �������̶� �ۼ���, ���� �ִ��� Ȯ�� ����
        string id = (string)Idict[DataVarType.Id.ToString()];
        string pw = (string)Idict[DataVarType.Pw.ToString()];
        string name = (string)Idict[DataVarType.Name.ToString()];
        string gender = (string)Idict[DataVarType.Gender.ToString()];
        string uid = (string)Idict[DataVarType.UID.ToString()];

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
        IDictionary Idict = Manager.Idict;
        Idict = null;
        if (reference == null) reference = FirebaseDatabase.DefaultInstance.RootReference;
        await reference.Child("UserData").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot data in snapshot.Children)
                {
                    if (data.Key == _uid)
                        Idict = (IDictionary)data.Value;
                }
            }
        });
        // string�� �������̶� �ۼ���, ���� �ִ��� Ȯ�� ����
        string id = (string)Idict[DataVarType.Id.ToString()];
        string pw = (string)Idict[DataVarType.Pw.ToString()];
        string name = (string)Idict[DataVarType.Name.ToString()];
        string gender = (string)Idict[DataVarType.Gender.ToString()];
        string uid = (string)Idict[DataVarType.UID.ToString()];

        HealthUserData targetData = new HealthUserData(name, gender, uid, id, pw);
        targetData.arrayEdit(_type, _value);
        string json = JsonConvert.SerializeObject(targetData);
        await reference.Child("UserData").Child(targetData.returnUID()).SetRawJsonValueAsync(json);
    }
    public async Task UserDataLoad(DataVarType _type, string _value)
    {
        IDictionary Idict = Manager.Idict;
        Idict = null;
        if(_type == DataVarType.UID)
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
                    if (targetValue == _value) Idict = targetData;
                }
            }
        });
        Debug.Log(Idict);
        return;
    }
    private void Awake()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        path = Path.Combine(Application.dataPath, "uid.json");
    }
}
