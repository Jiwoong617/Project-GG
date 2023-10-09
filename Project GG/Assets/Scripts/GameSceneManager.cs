using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Scene이 바뀌는 것을 관리해주는 Manager
/// </summary>
public class GameSceneManager : MonoBehaviour
{
    private static GameSceneManager instance;
    public static GameSceneManager Instance
    {
        get
        {
            if (instance != null)
            {
                Destroy(instance);
            }
            return instance;
        }
    }
    public SceneList nowScene;

    /// <summary>
    /// Scene을 바꿔주는 함수
    /// </summary>
    /// <param name="_sceneState">바꾸고 싶은 Scene</param>
    public void SceneChange(SceneList _sceneState)
    {
        SceneManager.LoadScene(_sceneState.ToString());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
