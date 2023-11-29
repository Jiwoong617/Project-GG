using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    private static SceneList SceneType = SceneList.GameScene;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            GameObject go = Instantiate(new GameObject("GameManager"), transform);
            _game = go.AddComponent<GameManager>();
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); return; }

        if (SceneType == SceneList.AppScene) { }
        else if (SceneType == SceneList.GameScene)
        {
            Game.GameStart();
        }
    }


    //매니저들
    GameManager _game = new GameManager();
    HealthManager _health = new HealthManager();
    DataManager _data = new DataManager();

    public static GameManager Game { get { return Instance._game; } }
    public static HealthManager Health { get { return Instance._health; } }
    public static DataManager Data { get {  return Instance._data; } }

    public void ChangeScene(SceneList type) => SceneType = type;
}