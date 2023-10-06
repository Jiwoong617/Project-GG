using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField]
    protected int _hp;
    [SerializeField]
    protected int _maxHp;
    [SerializeField]
    protected int _attack;
    [SerializeField]
    protected float _moveSpeed;

    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int Attack { get { return _attack; } set { _attack = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    private void Start()
    {
    }

    //normal damage
    public virtual void TakeDamage(Stat attacker)
    {
        int damage = attacker.Attack;
        Hp -= damage;

        if (Hp <= 0)
        {
            Hp = 0;
            OnDead();
        }
    }

    public virtual void OnDead()
    {
        Debug.Log($"{gameObject.name} dead");

        //gameObject.SetActive(false);
    }

    protected virtual void SetStat()
    {
        Hp = 100;
        MaxHp = 100;
        Attack = 5;
        MoveSpeed = 5;
    }
}

