using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private bool debug;

    [SerializeField] int damage;
    [SerializeField] public float range;
    [SerializeField, Range(0, 360)] float angle;

    private Animator anim;
    private PlayerMover player;
    private float cosResult;
    private SkillData comboAttack;
    private SkillCoolTime skillCoolTime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<PlayerMover>();
        cosResult = Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad);
    }

    private void Start()
    {
        skillCoolTime = player.GetComponent<SkillCoolTime>();
        comboAttack = GameManager.Resource.Load<SkillData>("LDW/Data/ComboAttack");
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }

    private void OnAttack(InputValue value)
    {
        if (player.isCrouching)
        {
            player.isCrouching = false;
            StartCoroutine(player.CrouchRoutine());
            return;
        }
        Attack();
    }

    public void AttackTiming()
    {
        // 1. ���� �ȿ� �ִ���
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
            // 2. ���� �ȿ� �ִ��� (������ ���� �̿�)
            Vector3 dirTarget = (collider.transform.position - transform.position).normalized;
            if (Vector3.Dot(transform.forward, dirTarget) < cosResult)
                continue;

            IHittable hittable = collider.GetComponent<IHittable>();
            hittable?.TakeHit(damage);
        }
    }
    
    public void OnComboAttack(InputValue value)
    {
        anim.SetTrigger("ComboAttack");
        damage = comboAttack.Skills[0].damage;
        Debug.Log($"ComboAttack {damage}");
    }

    private void OnDrawGizmosSelected()
    {
        if (!debug)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + angle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - angle * 0.5f);
        Debug.DrawRay(transform.position, rightDir * range, Color.yellow);
        Debug.DrawRay(transform.position, leftDir * range, Color.yellow);
    }

    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
}