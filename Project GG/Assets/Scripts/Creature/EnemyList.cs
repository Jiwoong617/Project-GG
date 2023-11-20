using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyList")]
public class EnemyList : ScriptableObject
{
    public List<GameObject> EnemyPref;
}
