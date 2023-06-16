using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Data/Skill")]
public class SkillData : ScriptableObject
{
    [SerializeField] SkillInfo[] skills;
    public SkillInfo[] Skills { get { return skills; } }

    [Serializable]
    public class SkillInfo
    {
        public int damage;
        public float delay;
        public float range;
        public float cool;

        public Sprite icon;
    }
}