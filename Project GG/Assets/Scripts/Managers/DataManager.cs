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
            //������ �������� ���� ���, ó�� ������
            Debug.LogError(dataPath + " �ش� ������ �������� �ʽ��ϴ�.");
        }
        return routineList;
    }
    // JSON ������ �Ľ�
    // 1. ���� ���� ��ƾ
    // 2. ĳ���� ����
}
