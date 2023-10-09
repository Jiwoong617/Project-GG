using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Scene�� �ٲ�� ���� �������ִ� Manager
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
    /// Scene�� �ٲ��ִ� �Լ�
    /// </summary>
    /// <param name="_sceneState">�ٲٰ� ���� Scene</param>
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
