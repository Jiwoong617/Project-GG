using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Exercise")]
public class Exercise : ScriptableObject
{
    public string title; // 운동의 한국어 이름
    public BodyParts part; // 운동의 부위
    public int times; // 운동의 횟수
    public int exp; // 운동의 경험치량
    [TextArea] public string description; // 운동의 설명
}
