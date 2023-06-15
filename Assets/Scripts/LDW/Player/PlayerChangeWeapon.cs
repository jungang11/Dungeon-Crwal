using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Weapons { OneHandSword = 0, DoubleAxe = 1 }
public class PlayerChangeWeapon : MonoBehaviour
{
    [SerializeField] GameObject OneHandSword;
    [SerializeField] GameObject DoubleAxe_L;
    [SerializeField] GameObject DoubleAxe_R;

    private float swapDelay = 0.5f;
    private Animator anim;
    private Weapons curWeapon;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private IEnumerator SwapWeaponRoutine()
    {
        // ÇÑ¼Õ°Ë
        if (curWeapon == Weapons.OneHandSword)
        {
            anim.SetFloat("WeaponState", 0);
            OneHandSword.SetActive(true);
            DoubleAxe_L.SetActive(false);
            DoubleAxe_R.SetActive(false);
            yield return new WaitForSeconds(swapDelay);
        }
        // ½Öµµ³¢
        else if (curWeapon == Weapons.DoubleAxe)
        {
            anim.SetFloat("WeaponState", 1);
            OneHandSword.SetActive(false);
            DoubleAxe_L.SetActive(true);
            DoubleAxe_R.SetActive(true);
            yield return new WaitForSeconds(swapDelay);
        }
    }

    private void OnOneHandSword(InputValue value)
    {
        curWeapon = Weapons.OneHandSword;
        StartCoroutine(SwapWeaponRoutine());
    }

    private void OnDoubleAxe(InputValue value)
    {
        curWeapon = Weapons.DoubleAxe;
        StartCoroutine(SwapWeaponRoutine());
    }
}
