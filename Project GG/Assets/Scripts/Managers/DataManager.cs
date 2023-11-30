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
            //������ �������� ���� ���, ó�� ������
            Debug.LogError(dataPath + " �ش� ������ �������� �ʽ��ϴ�.");
        }
        return routineJson;
    }
    // JSON ������ �Ľ�
    // 1. ���� ���� ��ƾ
    // 2. ĳ���� ����
}
