using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Exercise")]
public class Exercise : ScriptableObject
{
    public string name;
    public BodyParts part;
    public int exp;
    public string imgLoc;
    public string description;
}
