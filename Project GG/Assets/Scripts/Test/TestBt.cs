//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;
//using UnityEngine.UI;
//using Newtonsoft.Json;
//using Firebase;
//using Firebase.Database;
//using Firebase.Unity;

//public class TestBt : MonoBehaviour
//{
//    /*Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(Task => {
//        var dependencyStatus = task.Result;
//        if (dependencyStatus == Firebase.DependencyStatus.Available)
//        {
//            // Create and hold a reference to your FirebaseApp,
//            // where app is a Firebase.FirebaseApp property of your application class.
//            app = Firebase.FirebaseApp.DefaultInstance;

//            // Set a flag here to indicate whether Firebase is ready to use by your app.
//        }
//        else
//        {
//            UnityEngine.Debug.LogError(System.String.Format(
//              "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
//            // Firebase Unity SDK is not safe to use here.
//        }
//    });*/

//    private TMP_InputField nameInput;
//    private TMP_Text nameTMP;
//    private string name;

//    private Slider genderSlider;
//    private bool gender;

//    DatabaseReference reference;
//    // Start is called before the first frame update
//    private void Awake()
//    {
//        nameInput = GameObject.Find("NameInput").GetComponent<TMP_InputField>();
//        nameTMP = nameInput.transform.Find("Text Area/Text").GetComponent<TMP_Text>();
//        genderSlider = GameObject.Find("Gender").GetComponent<Slider>();
//    }
//    private void Start()
//    {
//        reference = FirebaseDatabase.DefaultInstance.RootReference;
//        UserDataConfirm();
//    }
//    public void UserDataConfirm()
//    {
//        name = nameInput.text;

//        if (genderSlider.value == (int)Gender.Man) gender = false;
//        else gender = true;
//        HealthUserData newUserData = new HealthUserData(name,gender);
//        string json = JsonConvert.SerializeObject(newUserData);
//        reference.Child("UserData").Child(newUserData.returnUID()).SetRawJsonValueAsync(json);
//    }
//}
