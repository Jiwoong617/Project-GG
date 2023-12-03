using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ExerciseInfo : MonoBehaviour
{
    public Exercise ex;
    public TextMeshProUGUI exName;
    public Image image;
    public TextMeshProUGUI exDescription;
    public Button upBtn, downBtn, exitBtn;
    public TextMeshProUGUI countText;
    public Button startBtn, addRoutineBtn;
    public GameObject exStart;

    private int count;

    private void Start()
    {
        if (ex != null)
        {
            exName.text = ex.title;
            image.sprite = ex.img;
            exDescription.text = ex.description;
        }
        upBtn.onClick.AddListener(OnClickedUpBtn);
        downBtn.onClick.AddListener(OnClickedDownBtn);
        exitBtn.onClick.AddListener(() => Destroy(gameObject));
        startBtn.onClick.AddListener(StartBtn);
        addRoutineBtn.onClick.AddListener(AddRoutineBtn);

        count = 10;
    }

    private void OnClickedUpBtn()
    {
        count = Mathf.Clamp(count+1, 0, 50);
        countText.text = count.ToString();
    }
    private void OnClickedDownBtn()
    {
        count = Mathf.Clamp(count-1, 0, 50);
        countText.text = count.ToString();
    }
    
    private void StartBtn()
    {
        ExStart es = Instantiate(exStart, transform.parent.parent.transform).GetComponent<ExStart>();
        if (es.exerciseList == null)
            es.exerciseList = new();
        es.exerciseList.Add(new Exercise(ex.title, ex.part, count, ex.exp, ex.img, ex.description));
        Destroy(gameObject);
    }

    private void AddRoutineBtn()
    {
        RoutineStruct routineStruct = new RoutineStruct(ex.name, count);
        Manager.Health.routines.Add(routineStruct);
        Manager.Data.RoutineSerialize(Manager.Health.routines);
    }
}
