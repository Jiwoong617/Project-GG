using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : BaseController
{
    public PlayerStat stat = new();
    public Vector3 moveDir;

    [SerializeField] private Animator animator;

    const string ANIM_IDLE = "Idle";
    const string ANIM_MOVE = "Move";
    const string ANIM_SPRINT = "Sprint";
    const string ANIM_ATTACK = "Attack";

    private void Awake()
    {
        Global.Player = gameObject;
    }

    private void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();

        //임시
        stat.SetStat(new StatInfo(100, 100, 100, 100, 10));
    }


    protected override void OnIdle()
    {
        if(moveDir != Vector3.zero)
        {
            state = State.Moving;
            return;
        }

        animator.Play(ANIM_IDLE);
    }

    protected override void OnMoving()
    {
        if(moveDir == Vector3.zero)
        {
            state = State.Idle;
            return;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), 20 * Time.deltaTime);
        transform.position += moveDir * Time.deltaTime * stat.MoveSpeed;

        if(stat.MoveSpeed > 15) animator.Play(ANIM_SPRINT);
        else animator.Play(ANIM_MOVE);
    }

    protected override void OnDie()
    {

    }

    public void SetDir(Vector3 dir) => moveDir = dir;
}