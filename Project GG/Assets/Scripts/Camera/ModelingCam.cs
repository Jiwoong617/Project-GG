using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class ModelingCam : MonoBehaviour
{
    Vector3 mouseDownPos;
    Vector3 mouseUpPos;
    float mouseDis;
    Camera cam;

    [SerializeField]
    GameObject Modeling;

    private IEnumerator rotateModeling()
    {
        if(Input.GetMouseButtonDown(0))
        {
            PointerEventData pointer = new PointerEventData(EventSystem.current);
            pointer.position = Input.mousePosition;

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, raycastResults);

            if(raycastResults.Count > 0 )
            {
                foreach(RaycastResult r in raycastResults )
                {
                    if(r.gameObject == gameObject)
                    {
                        mouseDownPos = cam.ScreenToViewportPoint(Input.mousePosition);
                        while (Input.GetMouseButton(0))
                        {
                            mouseUpPos = cam.ScreenToViewportPoint(Input.mousePosition);
                            mouseDis = mouseDownPos.x - mouseUpPos.x;
                            Modeling.transform.Rotate(new Vector3(0, mouseDis * 100, 0));
                            yield return mouseDownPos = cam.ScreenToViewportPoint(Input.mousePosition);
                        }
                    }
                }
            }
        }
    }
    private void Awake()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        StartCoroutine(rotateModeling());
    }
}
