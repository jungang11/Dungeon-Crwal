using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Stat_", menuName = "PluggableMonster/MonsterStat")]
public class MonsterStat : ScriptableObject
{
    [SerializeField] private int health;
    [SerializeField] private float speed;
    [SerializeField] private float maxHealth;
    [SerializeField] private float minHealth;
    [SerializeField] private float sight;   
}
