using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public string dataPath; // 파일의 경로, 이벤트 함수(하단)에서 Awake

    //private void testCode()
    //{
    //    List<Exercise> exerciseList = new List<Exercise>();
    //    for(int i = 0; i < 2; i++)
    //    {
    //        Exercise exercise = new Exercise()
    //        {
    //            title = "운동" + i,
    //            part = BodyParts.Arm,
    //            times = 0,
    //            exp = 0,
    //            description = "운 동 !"
    //        };
    //        exerciseList.Add(exercise);
    //    }
    //    List<RoutineStruct> routineList = RoutineDataExtract(exerciseList);
    //    RoutineSerialize(routineList);
    //    routineList = new List<RoutineStruct>();
    //    routineList = RoutineDeserialize();
    //}

    public List<RoutineStruct> RoutineDataExtract(List<Exercise> _exercises)
    {
        List<RoutineStruct> routineList = new List<RoutineStruct>();
        RoutineStruct routineData = new RoutineStruct();
        foreach(Exercise exercise in _exercises)
        {
            routineData.name = exercise.name;
            routineData.times = exercise.times;
            routineList.Add(routineData);
        }
        return routineList;
    }
    public void RoutineSerialize(List<RoutineStruct> _routineList)
    {
        string json = JsonConvert.SerializeObject(_routineList);
        File.WriteAllText(dataPath, json);
    }
    public List<RoutineStruct> RoutineDeserialize()
    {
        List<RoutineStruct> routineList = new List<RoutineStruct>();
        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText(dataPath);
            routineList = JsonConvert.DeserializeObject<List<RoutineStruct>>(json);
        }
        else
        {
            //파일이 존재하지 않은 경우, 처음 생성시
            Debug.LogError(dataPath + " 해당 파일이 존재하지 않습니다.");
        }
        return routineList;
    }
    // JSON 데이터 파싱
    // 1. 직접 만든 루틴
    // 2. 캐릭터 정보
}
