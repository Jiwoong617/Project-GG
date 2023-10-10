using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    Stat stat;
    NavMeshAgent nav;
    Animator animator;

    [SerializeField]
    protected float maxMoveRange = 20f;
    [SerializeField]
    protected float detectRange = 10f;
    [SerializeField]
    protected float attackRange = 1f;

    protected Vector3 offSet;
    float distance;
    bool isAttacking;

    private void Start()
    {
        stat = GetComponent<Stat>();
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        offSet = transform.position;
    }

    protected override void OnIdle()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
            return;

        distance = (player.transform.position - transform.position).magnitude;
        if (distance <= detectRange)
        {
            target = player;
            state = State.Moving;

            return;
        }

        animator.Play("Idle");
    }

    protected override void OnMoving()
    {
        distance = (target.transform.position - transform.position).magnitude;

        if (target != null)
        {
            if (distance <= attackRange)
            {
                nav.SetDestination(transform.position);
                state = State.Attack;

                return;
            }
        }

        if (maxMoveRange <= (offSet - transform.position).magnitude)
        {
            nav.SetDestination(offSet);
            target = null;
            //복귀
            return;
        }

        if (distance < 0.5f)
        {
            state = State.Idle;
        }
        else
        {
            nav.SetDestination(target.transform.position);
            nav.speed = stat.MoveSpeed;

            Vector3 dir = target.transform.position - transform.position;
            dir.y = transform.position.y;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);

            animator.Play("Walk");
        }
    }

    private void Attack()
    {
        if (isAttacking)
            return;
        isAttacking = true;

        Vector3 dir = target.transform.position - transform.position;
        dir.y = transform.position.y;
        transform.rotation = Quaternion.LookRotation(dir);
        animator.CrossFade("Attack", 0.1f);
        //히트 관련
        transform.GetChild(0).GetComponent<Collider>().enabled = true;
    }

    void MakeStateMoving()
    {
        isAttacking = false;

        //칼 콜라이더 비활성화
        transform.GetChild(0).GetComponent<Collider>().enabled = false;

        state = State.Moving;
    }
}
