using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectHolder")]
public class ObjectHolder : ScriptableObject
{
    public List<Object> HoldingObjects;
}
