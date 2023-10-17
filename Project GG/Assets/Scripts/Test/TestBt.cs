using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

using Newtonsoft.Json;
using Firebase;
using Firebase.Database;
using Firebase.Unity;
using System;

public class TestBt : MonoBehaviour
{
    /*Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(Task => {
        var dependencyStatus = task.Result;
        if (dependencyStatus == Firebase.DependencyStatus.Available)
        {
            // Create and hold a reference to your FirebaseApp,
            // where app is a Firebase.FirebaseApp property of your application class.
            app = Firebase.FirebaseApp.DefaultInstance;

            // Set a flag here to indicate whether Firebase is ready to use by your app.
        }
        else
        {
            UnityEngine.Debug.LogError(System.String.Format(
              "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            // Firebase Unity SDK is not safe to use here.
        }
    });*/

    private TMP_InputField nameInput;
    private TMP_Text nameTMP;
    private string name;

    private Toggle genderToggle;
    private bool gender;

    DatabaseReference reference;

    private string uid;
    private string path;
    // Start is called before the first frame update
    
    /// <summary>
    /// 유저의 데이터를 업로드, 수정을 하는 함수
    /// </summary>
    /// <param name="_name"></param>
    public void UserDataUpload()
    {
        string uidJson;
        if(File.Exists(path) == true)
        {
            // Json파일이 존재한다면 불러오기
            uidJson = File.ReadAllText(path);
            uid = uidJson;
        }
        else
        {
            // Json파일이 존재하지 않았다면 새로 생성 Json파일로 저장
            uid = Guid.NewGuid().ToString();
            uidJson = JsonConvert.SerializeObject(uid);
            File.WriteAllText(path, uidJson);
        }
        name = nameInput.text;

        if (genderToggle.isOn) gender = false;
        else gender = true;

        HealthUserData newUserData = new HealthUserData(name, gender,uid);
        string json = JsonConvert.SerializeObject(newUserData);
        reference.Child("UserData").Child(newUserData.returnUID()).SetRawJsonValueAsync(json);
    }
    /// <summary>
    /// 유저의 데이터를 불러오는 함수
    /// </summary>
    /// <param name="_uid"></param>
    /// <param name="_dataName"></param>
    public void UserDataLoad(string _uid, string _dataName)
    {
        reference.Child("UserData").Child(_uid).GetValueAsync().ContinueWith(task =>
        {
            if(task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot data in snapshot.Children)
                {
                    if(data.Key == _dataName)
                    {
                        UnityEngine.Debug.Log(data.Value);
                    }
                    //IDictionary rank = (IDictionary)data.Value;
                }
            }
        });
    }
    /// <summary>
    /// 데이터 베이스를 초기화하는 함수
    /// </summary>
    public void UserDataReset()
    {
        reference.RemoveValueAsync();
    }
    private void Awake()
    {
        nameInput = GameObject.Find("NameInput").GetComponent<TMP_InputField>();
        nameTMP = nameInput.transform.Find("Text Area/Text").GetComponent<TMP_Text>();
        genderToggle = GameObject.Find("ui").GetComponent<Toggle>();
    }
    private void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        path = Path.Combine(Application.dataPath, "uid.json");
    }
}
