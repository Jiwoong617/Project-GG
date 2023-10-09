using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ui에 들어갈 버튼의 부모 스크립트
/// </summary>
public class UiButton : MonoBehaviour
{
    protected Button button;

    protected virtual void buttonFunc()
    {

    }
    protected void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            buttonFunc();
        });
    }
}
