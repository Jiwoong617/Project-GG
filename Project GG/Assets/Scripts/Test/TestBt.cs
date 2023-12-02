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

public class TestBt : MonoBehaviour
{
    private TMP_InputField nameInput;
    private TMP_Text nameTMP;
    private string name;

    private Toggle genderToggle;
    private string gender;

    DatabaseReference reference;

    private string path;
    // Start is called before the first frame update
    
    /// <summary>
    /// �� ������ �����͸� ���ε��ϴ� �Լ�
    /// </summary>
    /// <param name="_name"></param>
    public void NewUserUserDataUpload()
    {
        // Json������ �������� �ʾҴٸ� ���� ���� Json���Ϸ� ����
        string uid = Guid.NewGuid().ToString();
        string uidJson = JsonConvert.SerializeObject(uid);
        File.WriteAllText(path, uidJson);
        //if(File.Exists(path) == true)
        //{
        //    // Json������ �����Ѵٸ� �ҷ�����
        //    uidJson = File.ReadAllText(path);
        //    uid = uidJson;
        //}
        name = nameInput.text;
        string id = null;
        string pw = null;
        gender = Gender.Man.ToString();

        HealthUserData newUserData = new HealthUserData(name, gender,uid, id, pw);
        string json = JsonConvert.SerializeObject(newUserData);
        reference.Child("UserData").Child(newUserData.returnUID()).SetRawJsonValueAsync(json);
    }
    /// <summary>
    /// ���� ������ �����͸� �����ؼ� ���ε��ϴ� �Լ�
    /// </summary>
    public void EditedUserDataUpload()
    {

    }
    /// <summary>
    /// ������ �����͸� �ҷ����� �Լ�
    /// </summary>
    /// <param name="_uid"></param>
    /// <param name="_dataName"></param>
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
    /// <summary>
    /// ������ ���̽��� �ʱ�ȭ�ϴ� �Լ�
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
        //Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(Task => {
        //    var dependencyStatus = Task.Result;
        //    if (dependencyStatus == Firebase.DependencyStatus.Available)
        //    {
        //        // Create and hold a reference to your FirebaseApp,
        //        // where app is a Firebase.FirebaseApp property of your application class.
        //        FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;

        //        // Set a flag here to indicate whether Firebase is ready to use by your app.
        //    }
        //    else
        //    {
        //        UnityEngine.Debug.LogError(System.String.Format(
        //          "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
        //        // Firebase Unity SDK is not safe to use here.
        //    }
        //});
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        path = Path.Combine(Application.dataPath, "uid.json");
        //NewUserUserDataUpload();
        string targetUid = "4a160052-d74e-4668-ac29-6cc14816efab";
        IDictionary i = UserDataLoad(targetUid);
    }
}
