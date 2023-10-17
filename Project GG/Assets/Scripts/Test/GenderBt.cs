using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenderBt : UiButton
{
    Toggle toggle;
    private RectTransform cursor;
    private Vector3 manVec;
    private Vector3 womanVec;

    private void Start()
    {
        toggle = GameObject.Find("ui").GetComponent<Toggle>();
        cursor = GameObject.Find("Cursor").GetComponent<RectTransform>();
        manVec = Vector3.zero;
        womanVec = new Vector3(50,0,0);
    }
    public override void buttonFunc()
    {
        
        if(toggle.isOn == true)
        {
            cursor.position= manVec;
        }
        else
        {
            cursor.position = womanVec;
        }
    }
}
