using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseUI : MonoBehaviour
{
    public Transform parent;
    public Exercise ex;
    public Text title;
    public Image img;

    [SerializeField] GameObject ExInfo;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => Instantiate(ExInfo, parent).GetComponent<ExerciseInfo>().ex = ex);
    }
}
