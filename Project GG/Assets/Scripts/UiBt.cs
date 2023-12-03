using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiBt : MonoBehaviour
{
    private enum btFuncEnum
    {
        RegisterBt,
        LoginBt,
    }

    private Button button;
    [SerializeField]
    btFuncEnum funcName;
    public void buttonFunc()
    {
        MethodInfo[] mi = GetType().GetMethods();
        foreach (MethodInfo i in mi)
        {
            if (funcName.ToString().Equals(i.Name))
            {
                i.Invoke(this, null);
            }
        }
    }
    public async void LoginBt()
    {
        Transform registerCanvas = GameObject.Find("RegisterCanvas").transform;
        TMP_InputField idField = registerCanvas.Find("SignInFrame/IdInput").GetComponent<TMP_InputField>();
        TMP_InputField pwField = registerCanvas.Find("SignInFrame/PwInput").GetComponent<TMP_InputField>();

        await Manager.FB.Login(idField.text, pwField.text);
    }
    public async void RegisterBt()
    {
        Transform registerCanvas = GameObject.Find("RegisterCanvas").transform;
        TMP_InputField idField = registerCanvas.Find("SignInFrame/IdInput").GetComponent<TMP_InputField>();
        TMP_InputField pwField = registerCanvas.Find("SignInFrame/PwInput").GetComponent<TMP_InputField>();

        await Manager.FB.Register(idField.text, pwField.text);
    }
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            buttonFunc();
        });
    }
}
