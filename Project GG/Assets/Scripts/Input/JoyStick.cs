using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private RectTransform bg_rect;
    [SerializeField] private RectTransform stick_rect;


    private float rad; //반지름
    private Vector3 movePos;

    [SerializeField] private GameObject player;
    [SerializeField] private float speed;

    private PlayerController pc;

    void Start()
    {
        player = Global.Player;
        pc = player.GetComponent<PlayerController>();
        rad = bg_rect.rect.width * 0.5f;
    }


    public void OnDrag(PointerEventData eventData)
    {
        Vector2 val = eventData.position - (Vector2)bg_rect.position;
        val = Vector2.ClampMagnitude(val, rad); //벡터 최대 제한
        // ex) (-1, 4)면 최소: -5, 최대: 3
        stick_rect.localPosition = val;


        val = val.normalized;
        float distance = Vector2.Distance(bg_rect.position, stick_rect.position) / rad;
        movePos = new Vector3(val.x, 0, val.y);
        pc.SetDir(movePos);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        stick_rect.localPosition = Vector3.zero;
        movePos = Vector3.zero;
        pc.SetDir(movePos);
    }

    public void StartFunc()
    {
        player = Global.Player;
        pc = player.GetComponent<PlayerController>();
        rad = bg_rect.rect.width* 0.5f;
    }
}