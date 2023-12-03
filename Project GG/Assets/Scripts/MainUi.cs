using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUi : MonoBehaviour
{
    //private void OnEnable()
    //{
    //    Manager.Health.UpdateUi();
    //}
    private void Start()
    {
        Manager.Health.UpdateUi();
    }
}
