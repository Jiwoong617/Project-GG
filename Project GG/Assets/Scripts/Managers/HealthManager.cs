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

public class HealthManager : MonoBehaviour
{
    public string path;
    public List<RoutineStruct> routines;

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
    public async void HealthUiSync()
    {
        path = Path.Combine(Application.dataPath, "uid.json");
        string nowScene = SceneManager.GetActiveScene().name.ToString() + "Scene";
        string targetScene = SceneList.AppScene.ToString();
        if (nowScene == targetScene)
        {
            string uid = "d609dcd1-1564-467a-bbb9-587ad252ee5b";
            //string uid = File.ReadAllText(path);
            if (uid == null)
            {
                Debug.LogError("uid is null");
                return;
            }
            HealthUserData userData = await Manager.FB.UserHealthDataLoad(DataVarType.UID, uid);

            List<GameObject> healthUis = new List<GameObject>();
            string uiPath = "UiCanvas/Main/Scroll View/Viewport/Content/";
            healthUis.Add(GameObject.Find(uiPath + "AbsLv"));
            healthUis.Add(GameObject.Find(uiPath + "ArmLv"));
            healthUis.Add(GameObject.Find(uiPath + "BackLv"));
            healthUis.Add(GameObject.Find(uiPath + "ChestLv"));
            healthUis.Add(GameObject.Find(uiPath + "LegLv"));

            int cnt = 0;
            foreach (GameObject go in healthUis)
            {
                TMP_Text partsLv = go.transform.Find(MainHealthUi.PartsLv.ToString()).GetComponent<TMP_Text>();
                TMP_Text curExp = go.transform.Find(MainHealthUi.CurrentExp.ToString()).GetComponent<TMP_Text>();
                TMP_Text maxExp = go.transform.Find(MainHealthUi.MaxExp.ToString()).GetComponent<TMP_Text>();
                Slider expGauge = go.transform.Find(MainHealthUi.ExpGauge.ToString()).GetComponent<Slider>();

                partsLv.text = "Lv" + userData.BodyLevelArray[cnt];
                curExp.text = userData.BodyExpArray[cnt].ToString();
                maxExp.text = userData.BodyMaxExpArray[cnt].ToString();
                expGauge.value = userData.BodyExpArray[cnt] / userData.BodyMaxExpArray[cnt];
                cnt++;
            }
        }
    }
}
