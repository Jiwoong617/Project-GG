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
        //Routine의 데이터 형식은 List<int>로 인덱스를 통해 접근 후 벨류값을 찾는 형식으로 구현
        List<int> routineList = Array.ConvertAll(_json.Split(','), int.Parse).ToList();
        
    }
    // JSON 데이터 파싱
    // 1. 직접 만든 루틴
    // 2. 캐릭터 정보
}
