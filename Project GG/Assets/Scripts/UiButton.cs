using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ui�� �� ��ư�� �θ� ��ũ��Ʈ
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
