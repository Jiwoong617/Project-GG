using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private string dataPath = Path.Combine(Application.persistentDataPath, "RoutineData.json");

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
