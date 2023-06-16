using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour, IHittable
{
    [SerializeField] private int hp;
    private Animator anim;
    protected List<PlayerController> enemyList;

    [SerializeField] Transform archor;
    [SerializeField] Transform arrowPoint;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyList = new List<PlayerController>();
    }

    private void OnEnable()
    {
        StartCoroutine(LookRoutine());
        StartCoroutine(AttackRoutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void AddEnemy(PlayerController enemy)
    {
        enemyList.Add(enemy);
    }

    public void RemoveEnemy(PlayerController enemy)
    {
        enemyList.Remove(enemy);
    }

    public void TakeHit(int damage)
    {
        anim.SetTrigger("TakeDamage");
        hp -= damage;

        if (hp < 0)
        {
            StartCoroutine(DieRoutine());
        }
    }

    private IEnumerator DieRoutine()
    {
        anim.SetTrigger("Die");
        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
        yield return null;
    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (enemyList.Count > 0)
            {
                anim.SetTrigger("ShootArrow");
                yield return new WaitForSeconds(1.2f);
                Attack(enemyList[0]);
                yield return new WaitForSeconds(5f);
            }
            else
            {
                yield return null;
            }
        }
    }

    public void Attack(PlayerController enemy)
    {
        Arrow arrow = GameManager.Resource.Instantiate<Arrow>("LDW/Arrow", arrowPoint.position, arrowPoint.rotation);
        arrow.SetTarget(enemy);
        arrow.SetDamage(1);
    }

    IEnumerator LookRoutine()
    {
        while (true)
        {
            if (enemyList.Count > 0)
            {
                // 맨 처음 enemy의 위치를 바라보게 함
                Vector3 dir = (enemyList[0].transform.position - transform.position).normalized;
                archor.transform.rotation = Quaternion.Lerp(archor.transform.rotation, Quaternion.LookRotation(dir), 0.1f);
            }
            yield return null;
        }
    }
}
