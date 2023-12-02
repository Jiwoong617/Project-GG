using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoutineUI : MonoBehaviour
{
    public Transform ExList;
    public GameObject ExInfo;

    private void OnEnable()
    {
        //Debug.Log(Manager.Health.routines.Count);
        List<Exercise> exList = Manager.Health.ExerciseInRoutine();
        foreach (Exercise ex in exList)
        {
            RoutineExInfo rex = Instantiate(ExInfo, ExList).GetComponent<RoutineExInfo>();
            rex.ex = ex;
        }
    }

    public void ResetRoutineList()
    {
        foreach (Transform t in ExList)
            Destroy(t.gameObject);
        gameObject.SetActive(false);
    }
}
