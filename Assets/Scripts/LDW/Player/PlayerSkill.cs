using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkill : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void UseSkill()
    {
        anim.SetTrigger("Skill");
        Debug.Log("Skill");
    }

    public void OnSkill(InputValue value)
    {
        UseSkill();
    }
}
