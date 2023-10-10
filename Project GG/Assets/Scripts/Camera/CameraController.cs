using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector3 Offset = new Vector3(0, 9, -6);
    [SerializeField] GameObject player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        Global.CameraShake = CamShake;
    }

    private void Update()
    {
        transform.position = player.transform.position + Offset;
    }

    private void CamShake()
    {
        transform.DORewind();
        transform.DOShakePosition(0.2f, new Vector3(0.03f, 0.03f, 0), 25);
    }
}
