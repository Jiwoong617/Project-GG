using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Newtonsoft.Json;
using Firebase.Database;
using System.Threading.Tasks;

public class HealthManager : MonoBehaviour
{
    public List<RoutineStruct> routines;
    public HealthUserData myHealthData;
    public GameObject main;

    public List<Exercise> ExerciseInRoutine()
    {
        List<Exercise> exercises = new();
        ObjectHolder exerciseList = Resources.Load<ObjectHolder>("Prefabs/Health/ExerciseList");

        foreach(RoutineStruct routine in routines)
        {
            string name = routine.name;
            Exercise exercise = exerciseList.HoldingObjects.Find(x => x.name == name) as Exercise;

            Exercise ex = new Exercise(exercise.title, exercise.part, exercise.times, exercise.exp, exercise.img, exercise.description); ;
            ex.times = routine.times;
            exercises.Add(ex);
        }
        return exercises;
    }
    /// <summary>
    /// FB에서 데이터 가져오는 코드
    /// </summary>
    public async void HealthDataSync()
    {
        string nowScene = SceneManager.GetActiveScene().name.ToString() + "Scene";
        string targetScene = SceneList.AppScene.ToString();
        if (nowScene == targetScene)
        {
            string uidJson = File.ReadAllText(Manager.Instance.UidPath);
            string uid = JsonConvert.DeserializeObject<string>(uidJson);
            if (uid == null)
            {
                Debug.LogError("uid is null");
                return;
            }
            HealthUserData userData = await Manager.FB.UserHealthDataLoad(DataVarType.UID, uid);
            myHealthData = userData.DeepCopy();
            UpdateUi();
        }
    }
    /// <summary>
    /// healthUi들 가져오는 코드
    /// </summary>
    /// <returns></returns>
    public List<GameObject> SearchHealthUi()
    {
        List<GameObject> healthUis = new List<GameObject>();
        healthUis = GameObject.FindGameObjectsWithTag("UserDataUI").ToList();
        healthUis.Reverse();

        return healthUis; ;
    }
    /// <summary>
    /// Ui 업데이트 해주는 코드
    /// </summary>
    public void UpdateUi()
    {
        bool isFlag = false;
        if (main == null)
            main = GameObject.FindWithTag("main");
        if (main.activeSelf == false) isFlag = true;
        main.SetActive(true);
        List<GameObject> healthUis = SearchHealthUi();
        int cnt = 0;
        foreach (GameObject go in healthUis)
        {
            TMP_Text partsLv = go.transform.Find(MainHealthUi.PartsLv.ToString()).GetComponent<TMP_Text>();
            TMP_Text curExp = go.transform.Find(MainHealthUi.CurrentExp.ToString()).GetComponent<TMP_Text>();
            TMP_Text maxExp = go.transform.Find(MainHealthUi.MaxExp.ToString()).GetComponent<TMP_Text>();
            Slider expGauge = go.transform.Find(MainHealthUi.ExpGauge.ToString()).GetComponent<Slider>();

            partsLv.text = "Lv" + myHealthData.BodyLevelArray[cnt];
            curExp.text = myHealthData.BodyExpArray[cnt].ToString();
            maxExp.text = myHealthData.BodyMaxExpArray[cnt].ToString();
            expGauge.value = (float)myHealthData.BodyExpArray[cnt] / myHealthData.BodyMaxExpArray[cnt];
            cnt++;
        }
        if(isFlag == true)
            main.SetActive(false);
    }
}
