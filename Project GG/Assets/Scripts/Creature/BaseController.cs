using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [SerializeField]
    protected State state = State.Idle;

    [SerializeField]
    protected GameObject target;


    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                OnIdle();
                break;
            case State.Moving:
                OnMoving();
                break;
            case State.Attack:
                OnAttack();
                break;
            case State.Dodge:
                OnDodge();
                break;
            case State.Die:
                OnDie();
                break;
        }
    }

    protected virtual void OnIdle() { }
    protected virtual void OnMoving() { }
    protected virtual void OnAttack() { }
    protected virtual void OnDodge() { }
    protected virtual void OnDie() { }
}
