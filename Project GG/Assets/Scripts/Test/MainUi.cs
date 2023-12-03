using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUi : MonoBehaviour
{
    void Update()
    {
        Manager.Health.HealthUiSync();
    }
}
