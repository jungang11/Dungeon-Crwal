using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour, IHittable
{
    [SerializeField] private int hp;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
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
}
