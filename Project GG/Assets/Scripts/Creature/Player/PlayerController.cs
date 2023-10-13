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

    const string ANIM_IDLE = "player_idle";
    const string ANIM_WALKING = "player_walking";
    const string ANIM_ATTACK = "player_attack";

    private void Awake()
    {
    }

    private void Start()
    {
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

        //animator.Play(ANIM_IDLE);
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
        //animator.Play(ANIM_WALKING);
    }

    protected override void OnDie()
    {

    }

    public void SetDir(Vector3 dir) => moveDir = dir;
}