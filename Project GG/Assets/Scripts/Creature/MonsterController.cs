using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    public bool isFree = true;

    Stat stat = new();
    Animator animator;
    [SerializeField] GameObject target;

    [SerializeField]
    protected float attackRange = 1f;

    float distance;
    Vector3 originVect;

    private void Start()
    {
        //animator = GetComponent<Animator>();
        originVect = transform.position;
    }

    public void Init(Vector3 pos, StatInfo status)
    {
        isFree = false;
        gameObject.SetActive(true);
        state = State.Moving;

        stat.SetStat(status);
        transform.position = pos;

        target = GameObject.FindGameObjectWithTag("Player");
    }

    protected override void OnMoving()
    {
        distance = (target.transform.position - transform.position).magnitude;

        if (target != null)
        {
            if (distance <= attackRange)
            {
                Attack();

                return;
            }
        }

        if (distance < 0.5f)
        {
            return;
        }
        else
        {
            Vector3 dir = (target.transform.position - transform.position).normalized;
            //dir.y = transform.position.y;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
            transform.position += dir * Time.deltaTime * stat.MoveSpeed;

            //animator.Play("Walk");
        }
    }

    protected override void OnDie()
    {
        isFree = true;
        gameObject.SetActive(false);
    }

    public void DieFunc() => OnDie();

    private void Attack()
    {
    //    Vector3 dir = target.transform.position - transform.position;
    //    dir.y = transform.position.y;
    //    transform.rotation = Quaternion.LookRotation(dir);
    //    animator.CrossFade("Attack", 0.1f);
    //    //히트 관련
    //    transform.GetChild(0).GetComponent<Collider>().enabled = true;
    }

    public override void OnAttacked(Stat s)
    {
        stat.TakeDamage(s);
        if (stat.Hp <= 0)
            state = State.Die;
    }

    IEnumerator MoveMotion()
    {
        transform.DOPunchScale(Vector3.one, 2f, 2).OnComplete(() => transform.position = originVect);
        yield return new WaitForSeconds(2f);
    }
}
    