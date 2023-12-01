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
            // ��ȿ���� ���� ���̵��� �˸�, ��� ����
            Debug.LogError("invalid id");
            return;
        }
        if(IsSameId(_id))
        {
            //���� ���̵� �ִٰ� �˸�, ��� ����
            Debug.LogError("same id");
            return;
        }
        // ���� ���ٸ� ȸ������, �Ϸ� �˸�
        NewUserUserDataUpload(_id, _pw);
    }
    public void Login(string _id, string _pw)
    {
        //�α��� �� �� ��ȯ Ȥ�� UI��ȯ�� �ʿ�
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
                return false;
            }
        }
        return true;
    }
    public bool IsSameId(string _id)
    {
        if (_id == null) return true; // ���̵� ����
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
