using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    //private static GameManager instance = null;
    //public static GameManager Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //            return null;
    //        return instance;
    //    }
    //}

    //private void Awake()
    //{
    //    if(instance == null)
    //        instance = this;

    //    GameStart();
    //}

    public void GameStart()
    {
        // 실행 순서
        //나중에 find with tags로 찾는 것들 다 global로 참조하게 바꿀거임

        player = Resources.Load<GameObject>("Prefabs/Game/Player");
        Instantiate(player);
        Instantiate(Resources.Load<GameObject>("Prefabs/Game/Map"));

        StartCoroutine(StartCount());

        Instantiate(Resources.Load<GameObject>("Prefabs/Game/Spawner"));
    }

    public void GameOver()
    {
        GameObject gameoverUI = GameObject.Find("Canvas").transform.GetChild(2).gameObject;
        gameoverUI.SetActive(true);
        gameoverUI.GetComponent<TextMeshProUGUI>().color = new(1, 1, 1, 0);
        gameoverUI.GetComponent<TextMeshProUGUI>().DOFade(1, 3);

        Invoke(nameof(ChangeScene), 5);
    }

    public IEnumerator StartCount()
    {
        yield return new WaitForSeconds(0.3f);
        TextMeshProUGUI text = GameObject.Find("StartCounter").GetComponent<TextMeshProUGUI>();
        text.color = new(1, 1, 1, 1);
        for (int i = 3; i>=0; i--)
        {
            if (i == 0) text.text = "GO!";
            else text.text = i.ToString();
            text.transform.DORewind();
            text.transform.DOShakePosition(0.3f, 20, 30);
            yield return new WaitForSecondsRealtime(0.4f);
        }
        text.transform.DOLocalMoveY(500, 0.3f).SetEase(Ease.InQuad).OnComplete(() => text.transform.localPosition = new Vector3(0, 80, 0));
        text.DOColor(new(1, 1, 1, 0), 0.3f);
        text.gameObject.SetActive(false);
    }

    private void ChangeScene()
    {
        Manager.Instance.ChangeScene(SceneList.AppScene);
        SceneManager.LoadScene("HealthScene");
    }
}
