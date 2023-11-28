using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenderBt : MonoBehaviour
{
    Toggle toggle;
    private RectTransform cursor;
    private Vector3 manVec;
    private Vector3 womanVec;

    private void Awake()
    {
        toggle = GameObject.Find("ui").GetComponent<Toggle>();
        cursor = GameObject.Find("Cursor").GetComponent<RectTransform>();
        manVec = Vector3.zero;
        womanVec = new Vector3(50,0,0);
    }
    private void toggleFunc()
    {
        if(toggle.isOn == true)
        {
            cursor.anchoredPosition = womanVec;
        }
        else
        {
            cursor.anchoredPosition = manVec;
        }
    }
    private void Update()
    {
        toggleFunc();
    }
}
