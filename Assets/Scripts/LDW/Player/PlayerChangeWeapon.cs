using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Weapons { OneHandSword = 0, DoubleAxe = 1, Bow = 2 }
public class PlayerChangeWeapon : MonoBehaviour
{
    [SerializeField] GameObject OneHandSword;
    [SerializeField] GameObject DoubleAxe_L;
    [SerializeField] GameObject DoubleAxe_R;
    [SerializeField] GameObject Bow;

    private float swapDelay = 0.5f;
    private Animator anim;
    private PlayerMover player;
    private Weapons curWeapon;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<PlayerMover>();
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
            Bow.SetActive(false);
            yield return new WaitForSeconds(swapDelay);
        }
        // ½Öµµ³¢
        else if (curWeapon == Weapons.DoubleAxe)
        {
            anim.SetFloat("WeaponState", 1);
            OneHandSword.SetActive(false);
            DoubleAxe_L.SetActive(true);
            DoubleAxe_R.SetActive(true);
            Bow.SetActive(false);
            yield return new WaitForSeconds(swapDelay);
        }
        else if(curWeapon == Weapons.Bow)
        {
            anim.SetFloat("WeaponState", 2);
            OneHandSword.SetActive(false);
            DoubleAxe_L.SetActive(false);
            DoubleAxe_R.SetActive(false);
            Bow.SetActive(true);
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

    private void OnBow(InputValue value)
    {
        curWeapon = Weapons.Bow;
        StartCoroutine(SwapWeaponRoutine());
    }
}
