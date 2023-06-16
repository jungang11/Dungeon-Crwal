using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] float attackSoundIntensity; 
    SoundMaker soundMaker;

    private void Awake()
    {
        soundMaker = GetComponent<SoundMaker>();
    }
    private void OnAttack(InputValue value)
    {
        soundMaker.TriggerSound(transform, attackSoundIntensity);
        Debug.Log("Attack"); 
    }
}
