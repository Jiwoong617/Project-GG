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

    private int count = 10;

    private void Start()
    {
        if (ex != null)
        {
            exName.text = ex.title;
            image.sprite = ex.img;
            exDescription.text = ex.description;
        }
        upBtn.onClick.AddListener(OnClickerUpBtn);
        downBtn.onClick.AddListener(OnClickerDownBtn);
        exitBtn.onClick.AddListener(() => Destroy(gameObject));
    }

    private void OnClickerUpBtn()
    {
        count = Mathf.Clamp(count+1, 0, 50);
        countText.text = count.ToString();
    }
    private void OnClickerDownBtn()
    {
        count = Mathf.Clamp(count-1, 0, 50);
        countText.text = count.ToString();
    }
}
