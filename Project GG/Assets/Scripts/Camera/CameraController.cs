using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector3 Offset = new Vector3(0, 9, -6);

    private float bias = 5f;

    private void Update()
    {
        if (Manager.Game.player == null) return;

        Vector3 targetPos = Global.Player.transform.position;
        transform.position = targetPos + Offset;
        //transform.position = Vector3.Lerp(transform.position, targetPos + Offset, bias * Time.deltaTime);
    }

}
