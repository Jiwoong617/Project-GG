using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    public bool isFree = true;

    Stat stat = new();
    [SerializeField] GameObject target;

    [SerializeField]
    protected float attackRange = 0.5f;

    float distance;
    Vector3 originVect;
    private Coroutine attackCo;

    private void Start()
    {
        originVect = transform.localScale;
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
        if (!target.GetComponent<PlayerController>().isAlive)
            return;

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
        attackCo = null;
        gameObject.SetActive(false);
    }

    public void DieFunc() => OnDie();

    private void Attack()
    {
        if(attackCo == null)
            attackCo = StartCoroutine(AttackMotion());
        //    Vector3 dir = target.transform.position - transform.position;
        //    dir.y = transform.position.y;
        //    transform.rotation = Quaternion.LookRotation(dir);
        //    animator.CrossFade("Attack", 0.1f);
        //    //히트 관련
        //    transform.GetChild(0).GetComponent<Collider>().enabled = true;
    }

    public override void OnAttacked(Stat s)
    {
        int damage = s.Attack;
        stat.Hp -= Mathf.Clamp(damage - stat.Defence, 0, int.MaxValue);

        if (stat.Hp <= 0)
        {
            state = State.Die;
            stat.Hp = 0;
        }
        hpBar.value = stat.Hp / stat.MaxHp;
    }

    IEnumerator AttackMotion()
    {
        transform.DOPunchScale(Vector3.one, 0.5f, 7).OnComplete(() => transform.localScale = originVect);
        Collider[] col = Physics.OverlapSphere(transform.position, 1, 1<< LayerMask.NameToLayer("Player"));
        foreach(Collider c in col)
        {
            c.GetComponent<BaseController>().OnAttacked(stat);
            //Debug.Log(c.name);
        }

        yield return new WaitForSeconds(1f);
        attackCo = null;
    }
}
    