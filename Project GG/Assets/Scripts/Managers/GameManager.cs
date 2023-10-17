using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }


    private void Awake()
    {
        if(instance == null)
            instance = this;

        Init();
    }

    private void Init()
    {
        // 실행 순서
        //나중에 find with tags로 찾는 것들 다 global로 참조하게 바꿀거임

        // 선택한 스테이지 정보 가져오기
        // 플레이어 생성
        // 맵 생성
        
        // --- 라운드 시작 전 카운트 다운 or 컷신 생성

        // 조이스틱 생성
        // 스포너 생성
        // UI 생성
    }
}
