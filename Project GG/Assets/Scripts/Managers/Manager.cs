using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    private static Manager instance = null;
    public static Manager Instance 
    { 
        get 
        {
            if (instance == null)
                return null;
            return instance; 
        } 
    }

    [SerializeField] private static SceneList SceneType = SceneList.AppScene;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            GameObject go = Instantiate(new GameObject("GameManager"), transform);
            _game = go.AddComponent<GameManager>();
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneChanged;
            Data.dataPath = Path.Combine(Application.dataPath, "Resources/RoutineData.json");
            Health.routines = new();
            Health.HealthDataSync();
            //Health.routines = Data.RoutineDeserialize();
        }
        else { Destroy(gameObject); return; }
    }


    //매니저들
    GameManager _game = new GameManager();
    HealthManager _health = new HealthManager();
    DataManager _data = new DataManager();
    FBManager _fb = new FBManager();

    public static GameManager Game { get { return Instance._game; } }
    public static HealthManager Health { get { return Instance._health; } }
    public static DataManager Data { get {  return Instance._data; } }
    public static FBManager FB { get { return Instance._fb;} }
    
    // 그냥 얘가 Path값 들고 있는게 더 깔끔할듯?
    public string UidPath = Path.Combine(Application.dataPath, "uid.json");

    public void ChangeScene(SceneList type) => SceneType = type;

    private void OnSceneChanged(Scene scene, LoadSceneMode mode)
    {
        if (SceneType == SceneList.GameScene)
            Game.GameStart();
        else if (SceneType == SceneList.AppScene)
        {
            Health.HealthDataSync();
            Health.UpdateUi();
        }
    }
}