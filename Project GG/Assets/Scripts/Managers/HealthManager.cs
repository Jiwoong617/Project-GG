using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    string path;
    public List<RoutineStruct> routines = new List<RoutineStruct>();
    public List<Exercise> exercises = new List<Exercise>();

    public void ExerciseInRoutine()
    {
        routines = Manager.Data.RoutineDeserialize();
        ObjectHolder exerciseList = Resources.Load<ObjectHolder>("Prefabs/Health/ExerciseList");

        foreach(RoutineStruct routine in routines)
        {
            string name = routine.name;
            Exercise exercise = exerciseList.HoldingObjects.Find(x => x.name == name) as Exercise;
            exercises.Add(exercise);
        }
    }
    public async void HealthUiSync()
    {
        string nowScene = SceneManager.GetActiveScene().ToString();
        string targetScene = SceneList.AppScene.ToString();
        if (nowScene == targetScene)
        {
            string uid = File.ReadAllText(path);
            if(uid == null)
            {
                Debug.LogError("uid is null");
                return;
            }
            await Manager.FBManager.UserDataLoad(DataVarType.UID, uid);
            IDictionary data = Manager.FBManager.Idict;

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

        }
    }
    private void Awake()
    {
        path = Path.Combine(Application.dataPath, "uid.json");
    }
}
