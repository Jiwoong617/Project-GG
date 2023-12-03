using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ExStart : MonoBehaviour
{
    public List<Exercise> exerciseList;

    public TextMeshProUGUI remainTime;
    public TextMeshProUGUI currentEx;
    public TextMeshProUGUI leftEx;
    public Button exitButton;

    private Coroutine co;
    private void Start()
    {
        co = StartCoroutine(ExTimer());
    }
    
    private void Update()
    {
        if (co != null)
        {
            TouchDuringEx();
        }
    }


    IEnumerator ExTimer()
    {
        int count = 0;
        while(count < exerciseList.Count)
        {
            Exercise ex = exerciseList[count];
            leftEx.text = $"� ���� : {count + 1} / {exerciseList.Count}";
            currentEx.text = ex.title;

            float totaltime = ex.times * 3;
            while (totaltime > 0)
            {
                totaltime -= Time.deltaTime;
                remainTime.text = $"{(int)totaltime / 60}:{(int)totaltime % 60}";
                yield return null;
            }

            GainExp();

            if (count+1 == exerciseList.Count)
            {
                remainTime.text = $"� �Ϸ�";
                currentEx.text = "";
                yield return new WaitForSeconds(2f);
                break;
            }

            totaltime = 3f;
            currentEx.text = "30�� �޽�";
            while (totaltime > 0)
            {
                totaltime -= Time.deltaTime;
                remainTime.text = $"{(int)totaltime / 60}:{(int)totaltime % 60}";
                yield return null;
            }

            count++;

        }
        exerciseList.Clear();
        co = null;
        Destroy(gameObject);
    }

    private void TouchDuringEx()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home) || Input.GetKeyDown(KeyCode.Menu))
            ExitBtn();
    }

    public void GainExp()
    {

    }

    public void ExitBtn()
    {
        if (co != null)
        {
            StopCoroutine(co);
            co = null;
        }

        exerciseList.Clear();
        Destroy(gameObject);
    }
}