using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    private CharacterController controller;
    private Animator anim;

    [Header("Pertaining to Movement")]
    private Vector3 moveDir;
    [SerializeField] private float moveSpeed;
    [Range(0f, 5f)]
    [SerializeField] private float ySpeed;
    [SerializeField] private bool isGround;
    [SerializeField] private bool isWalking;
    [SerializeField] private bool isRunning;

    [Header("Pertaining to Movement ability")]
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] private float jumpSpeed;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }
    private void Start()
    {
        //moveSpeed = 3f; 
        jumpSpeed = 3f;
        moveSpeed = 5;
    }
    private void Update()
    {
        Move();
        Jump();
    }
    private void FixedUpdate()
    {
        GroundCheck(); // �������� ���꿡 ���ؼ��� ����������Ʈ�� �����־, ���� ������ �� �ʿ��� ���꿡 ���ؼ� ��ȣ�� �����ϰڴ�. 
    }

    private void Move()
    {
        //���� ���� ������ 
        if (moveDir.magnitude <= 0) // �ȿ����� 
        {
            moveSpeed = Mathf.Lerp(moveSpeed, 0, 0.5f); // where value t = 0 - 1, in 100 % percentage // linearly decrase speed value from current to 0, 50% per frame 
        }
        else if (isWalking)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, 0.5f);
        }
        else  //            �ܹ���
        {
            moveSpeed = Mathf.Lerp(moveSpeed, runSpeed, 0.5f);
        }
        //controller.Move(moveDir * moveSpeed * Time.deltaTime); // �ʴ� ���������� �����Ѵ�. (���ӷ��� ����) // �̰� ��������� ������ 
        // where transform.forward = z-axis 
        controller.Move(transform.forward * moveDir.z * moveSpeed * Time.deltaTime);
        controller.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime);

        //anim.SetFloat("ZSpeed", moveDir.z, 0.1f, Time.deltaTime);
        //anim.SetFloat("XSpeed", moveDir.x, 0.1f, Time.deltaTime);
        //anim.SetFloat("Speed", moveSpeed);
    }

    private void OnMove(InputValue value)
    {

        Vector2 input = value.Get<Vector2>();

        moveDir = new Vector3(input.x, 0, input.y); // convert Vector2 value into Vector 3, (place y value into z) 
    }

    private void Jump()
    {
        //������ ���󼭴� ��� ��ü�̴� (��Ŭ������ ��������) �߷¿� ���Ͽ�
        //1. ���ӷ� (�ö󰥴�, ��������), 2. �ִ� ���̰� �����Ѵ�.
        //Falling 
        ySpeed += Physics.gravity.y * Time.deltaTime; // where Physics.gravity = Project Setting Gravity setting (where default is 9.81N) 
        // thus, this is movment declared by constant acceleration. 

        // Accelration to the ground must stop for 2 conditions. 
        // 1. if player has reached Ground : Using RayCast 
        if (GroundCheck() && ySpeed < 0) // yspeed must be 0 ONLY when player is falling 
        {
            ySpeed = 0;
        }
        // 2. if accerlation == gravity 
        if (ySpeed == Physics.gravity.y)
            ySpeed = Physics.gravity.y;

        controller.Move(Vector3.up * ySpeed * Time.deltaTime);
    }
    private void OnJump(InputValue value)
    {
        if (GroundCheck())
            ySpeed = jumpSpeed;
    }
    private bool GroundCheck()
    {
        RaycastHit hit;
        isGround = Physics.SphereCast(transform.position + Vector3.up * 1, 0.5f, Vector3.down, out hit, 0.7f);  // (vector3 origin, radius, direction, sphere depth) 
        return isGround;
    }
    private void OnWalk(InputValue value)
    {
        isWalking = value.isPressed; // if pressed = True, Released = false 
    }
}
