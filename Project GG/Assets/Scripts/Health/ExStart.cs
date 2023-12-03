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
            leftEx.text = $"운동 진행 : {count + 1} / {exerciseList.Count}";
            currentEx.text = ex.title;

            float totaltime = ex.times * 3;
            while (totaltime > 0)
            {
                totaltime -= Time.deltaTime;
                remainTime.text = $"{(int)totaltime / 60}:{(int)totaltime % 60}";
                yield return null;
            }

            GainExp(count);

            if (count+1 == exerciseList.Count)
            {
                remainTime.text = $"운동 완료";
                currentEx.text = "";
                yield return new WaitForSeconds(2f);
                break;
            }

            totaltime = 30f;
            currentEx.text = "30초 휴식";
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

    public void GainExp(int n)
    {
        int getExp = exerciseList[n].exp * exerciseList[n].times;
        int targetIndex = int.MaxValue;
        switch(exerciseList[n].part)
        {
            case BodyParts.Abs:
                targetIndex = 0;
                break;
            case BodyParts.Arm:
                targetIndex = 1;
                break;
            case BodyParts.Back:
                targetIndex = 2;
                break;
            case BodyParts.Chest:
                targetIndex = 3;
                break;
            case BodyParts.Leg:
                targetIndex = 4;
                break;
        }
        Manager.Health.myHealthData.BodyExpArray[targetIndex] += getExp;
        while(Manager.Health.myHealthData.BodyExpArray[targetIndex] >= Manager.Health.myHealthData.BodyMaxExpArray[targetIndex])
        {
            Manager.Health.myHealthData.BodyExpArray[targetIndex] -= Manager.Health.myHealthData.BodyMaxExpArray[targetIndex];
            Manager.Health.myHealthData.BodyLevelArray[targetIndex] += 1;
        }
        Manager.FB.UserDataEdit(Manager.Health.myHealthData);
        Manager.Health.UpdateUi();
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
