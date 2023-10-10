using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : BaseController
{
    public PlayerStat stat;
    public Vector3 moveDir;
    private CharacterController controller;

    [SerializeField] private Animator animator;

    const string ANIM_IDLE = "player_idle";
    const string ANIM_WALKING = "player_walking";
    const string ANIM_ATTACK = "player_attack";
    const string ANIM_DODGE = "player_dodge";

    private void Awake()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    protected override void OnIdle()
    {
        PressedBattleKey();

        if (Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f)
        {
            state = State.Moving;
            return;
        }

        animator.Play(ANIM_IDLE);
    }

    protected override void OnMoving()
    {
        PressedBattleKey();

        Vector3 inputDir = (((Vector3.right + Vector3.back) * Input.GetAxisRaw("Horizontal") + (Vector3.forward + Vector3.right) * Input.GetAxisRaw("Vertical"))).normalized;
        moveDir = Vector3.MoveTowards(moveDir, inputDir, 0.05f);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), 20 * Time.deltaTime);
        //controller.Move(stat.MoveSpeed * Time.deltaTime * moveDir);
        animator.Play(ANIM_WALKING);

        if (inputDir == Vector3.zero)
            state = State.Idle;
    }

    protected override void OnDie()
    {

    }


    private void PressedBattleKey()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("UI Clicked");
            return;
        }


        if (Input.GetMouseButton(0))
            state = State.Attack;
    }

    void MakeStateMoving()
    {
        //stat.isAttacking = false;
        //stat.isDodging = false;

        //칼 콜라이더 비활성화
        transform.GetChild(0).GetComponent<Collider>().enabled = false;

        state = State.Moving;
    }
}