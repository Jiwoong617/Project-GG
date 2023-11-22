using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector3 Offset = new Vector3(0, 9, -6);

    private float bias = 5f;

    private void Awake()
    {
        Global.CameraShake = CamShake;
    }

    private void Update()
    {
        if (GameManager.Instance.player == null) return;

        Vector3 targetPos = Global.Player.transform.position;
        transform.position = targetPos + Offset;
        //transform.position = Vector3.Lerp(transform.position, targetPos + Offset, bias * Time.deltaTime);
    }

    private void CamShake()
    {
        transform.DORewind();
        transform.DOShakePosition(0.2f, new Vector3(0.03f, 0.03f, 0), 25);
    }
}
