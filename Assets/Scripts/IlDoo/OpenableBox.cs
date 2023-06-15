using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableBox : MonoBehaviour
{
    [SerializeField] Item item; 
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void Open()
    {
        anim.SetBool("Open", true); 
    }

    public void Close()
    {
        anim.SetBool("Open", false);
    }
}
