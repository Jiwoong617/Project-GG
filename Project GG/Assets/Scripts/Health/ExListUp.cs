using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExListUp : MonoBehaviour
{
    [SerializeField] GameObject ExerciseList;
    [SerializeField] GameObject ExerciseUI;
    private int count = 0;

    public void OnClickedBodyParts()
    {
        foreach(Transform child in ExerciseList.transform)
            Destroy(child.gameObject);

        string name = EventSystem.current.currentSelectedGameObject.name;
        Object[] obj = Resources.LoadAll($"Prefabs/Health/{name}");
        foreach(Object o in obj)
        {
            Exercise ex = o as Exercise;
            ExerciseUI go = Instantiate(ExerciseUI, ExerciseList.transform).GetComponent<ExerciseUI>();
            go.ex = ex;
            go.title.text = ex.title;
            go.img.sprite = ex.img;
        }
    }

}
