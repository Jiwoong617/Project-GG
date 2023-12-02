using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoutineExInfo : MonoBehaviour
{
    public Exercise ex;
    public Text TitleTimes;
    public Image img;

    private void Start()
    {
        img.sprite = ex.img;
        TitleTimes.text = $"{ex.title} - {ex.times}¹ø";
    }

    public void Del()
    {
        int idx = 0;
        for(int i = 0; i<transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i) == transform)
            {
                idx = i;
                break;
            }
        }

        Manager.Health.routines.RemoveAt(idx);
        Destroy(gameObject);
    }
}
