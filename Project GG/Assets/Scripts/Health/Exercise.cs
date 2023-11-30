using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Exercise")]
public class Exercise : ScriptableObject
{
    public string title; // ��� �ѱ��� �̸�
    public BodyParts part; // ��� ����
    public int times; // ��� Ƚ��
    public int exp; // ��� ����ġ��
    [TextArea] public string description; // ��� ����
}
