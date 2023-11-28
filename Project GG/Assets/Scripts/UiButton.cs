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

    public virtual void buttonFunc()
    {

    }
    protected virtual void Start()
    {
        if(TryGetComponent<Button>(out Button bt))
        {
            button = bt;
            button.onClick.AddListener(() =>
            {
                buttonFunc();
            });
        }
    }
}
