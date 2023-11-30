using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public List<RoutineStruct> routines = new List<RoutineStruct>();
    public List<Exercise> exercises = new List<Exercise>();

    public void ExerciseInRoutine()
    {
        routines = Manager.Data.RoutineDeserialize();
        ObjectHolder exerciseList = Resources.Load<ObjectHolder>("Prefabs/Health/ExerciseList");

        foreach(RoutineStruct routine in routines)
        {
            string name = routine.name;
            Exercise exercise = exerciseList.HoldingObjects.Find(x => x.name == name) as Exercise;
            exercises.Add(exercise);
        }
    }
}
