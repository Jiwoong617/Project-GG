using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public void RoutineSerialize(Dictionary<string, int> _data)
    {
        string routineJson = _data.ToString();
    }
    public void RoutineDeserialize(string _json)
    {
        //Routine�� ������ ������ List<int>�� �ε����� ���� ���� �� �������� ã�� �������� ����
        List<int> routineList = Array.ConvertAll(_json.Split(','), int.Parse).ToList();
        
    }
    // JSON ������ �Ľ�
    // 1. ���� ���� ��ƾ
    // 2. ĳ���� ����
}
