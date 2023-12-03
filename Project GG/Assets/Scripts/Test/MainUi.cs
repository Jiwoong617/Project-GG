using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUi : MonoBehaviour
{
    void FixedUpdate()
    {
        Manager.Health.HealthUiSync();
    }
}
