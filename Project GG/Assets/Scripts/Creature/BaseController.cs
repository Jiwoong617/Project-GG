using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseController : MonoBehaviour
{
    [SerializeField]
    protected State state = State.Idle;
    [SerializeField]
    protected Slider hpBar;

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

    public virtual void OnAttacked(Stat s) { }
}
