using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Exercise")]
public class Exercise : ScriptableObject
{
    public string title;
    public BodyParts part;
    public int exp;
    [TextArea] public string description;
}
