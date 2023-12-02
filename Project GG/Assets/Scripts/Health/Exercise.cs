using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Exercise")]
public class Exercise : ScriptableObject
{
    public string title;
    public BodyParts part;
    public int times;
    public int exp;
    public Sprite img;
    [TextArea] public string description;

    public Exercise(string title, BodyParts part , int times, int exp, Sprite img, string description)
    {
        this.title = title;
        this.part = part;
        this.times = times;
        this.exp = exp;
        this.img = img;
        this.description = description;
    }
}
