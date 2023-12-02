using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private int count = 10;

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

    }

    private void AddRoutineBtn()
    {
        RoutineStruct routineStruct = new RoutineStruct(ex.name, count);
        Manager.Health.routines.Add(routineStruct);
    }
}
