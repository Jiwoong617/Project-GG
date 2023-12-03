using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
            string uid = "684714fc-e251-4fb6-8e2e-7ebc98e74060";
            //string uid = File.ReadAllText(path);
            if (uid == null)
            {
                Debug.LogError("uid is null");
                return;
            }
            await Manager.FBManager.UserDataLoad(DataVarType.UID, uid);
            IDictionary data = Manager.Idict;

            int[] bodyLevelArray = (int[])data[DataVarType.BodyLevelArray.ToString()];
            int[] bodyExpArray = (int[])data[DataVarType.BodyExpArray.ToString()];
            int[] bodyMaxExpArray = (int[])data[DataVarType.BodyMaxExpArray.ToString()];

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

                partsLv.text = "Lv" + bodyLevelArray[cnt];
                curExp.text = bodyExpArray[cnt].ToString();
                maxExp.text = bodyMaxExpArray[cnt].ToString();
                expGauge.value = bodyExpArray[cnt] / bodyMaxExpArray[cnt];
                cnt++;
            }
        }
    }
}
