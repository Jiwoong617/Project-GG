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
    public void RoutineSerialize(RoutineJson _routineJson)
    {
        string json = JsonConvert.SerializeObject(_routineJson);
        File.WriteAllText(dataPath, json);
    }
    public RoutineJson RoutineDeserialize()
    {
        RoutineJson routineJson = new RoutineJson();
        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText(dataPath);
            routineJson = JsonConvert.DeserializeObject<RoutineJson>(json);
        }
        else
        {
            //파일이 존재하지 않은 경우, 처음 생성시
            Debug.LogError(dataPath + " 해당 파일이 존재하지 않습니다.");
        }
        return routineJson;
    }
    // JSON 데이터 파싱
    // 1. 직접 만든 루틴
    // 2. 캐릭터 정보
}
