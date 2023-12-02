using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private void Update()
    {
        transform.position = transform.parent.position + 2.2f*Vector3.up;
        transform.rotation = Camera.main.transform.rotation;
    }
}
