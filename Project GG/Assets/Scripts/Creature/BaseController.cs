using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [SerializeField]
    protected State state = State.Idle;

    protected void Update()
    {
        switch (state)
        {
            case State.Idle:
                OnIdle();
                break;
            case State.Moving:
                OnMoving();
                break;
            case State.Die:
                OnDie();
                break;
        }
    }

    protected virtual void OnIdle() { }
    protected virtual void OnMoving() { }
    protected virtual void OnDie() { }
}
