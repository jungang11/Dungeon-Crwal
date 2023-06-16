using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackRange : MonoBehaviour
{
    public LayerMask enemyMask;

    public UnityEvent<PlayerController> OnInRangeEnemy;
    public UnityEvent<PlayerController> OnOutRangeEnemy;

    private void OnTriggerEnter(Collider other)
    {
        if (enemyMask.IsContain(other.gameObject.layer))
        {
            PlayerController enemy = other.GetComponent<PlayerController>();
            enemy.OnDied.AddListener(() => { OnOutRangeEnemy?.Invoke(enemy); });
            OnInRangeEnemy?.Invoke(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (enemyMask.IsContain(other.gameObject.layer))
        {
            PlayerController enemy = other.GetComponent<PlayerController>();
            OnOutRangeEnemy?.Invoke(enemy);
        }
    }
}
