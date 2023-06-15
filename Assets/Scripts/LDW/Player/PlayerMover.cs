using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private bool debug;

    [SerializeField] private float walkSpeed;       // �̵� �ӵ�
    [SerializeField] private float runSpeed;        // �̵� �ӵ�
    [SerializeField] private float crouchSpeed;     // �ɱ� �ӵ�
    [SerializeField] private float jumpForce;       // ���� �ӵ�
    private float applySpeed;

    //[SerializeField] FootStepSound footStepSound;   // �߼Ҹ�
    [SerializeField] private float walkStepRange;   // �ȱ� �Ҹ� ����
    [SerializeField] private float runStepRange;    // �޸��� �Ҹ� ����
    [SerializeField] private float crouchStepRange; // �ɱ� �Ҹ� ����

    private CharacterController controller;
    private Animator anim;
    private Vector3 moveDir;
    private float ySpeed;

    // ���� ����
    private bool isGrounded;
    private bool isWalking;
    public bool isCrouching;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        isCrouching = false;
    }

    private void OnEnable()
    {
        StartCoroutine(MoveRoutine());
        StartCoroutine(JumpRoutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void FixedUpdate()
    {
        GroundCheck();
    }

    float lastStepTime = 0.5f;
    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            if (moveDir.magnitude == 0)
            {
                applySpeed = Mathf.Lerp(applySpeed, 0, 0.1f);
                anim.SetFloat("MoveSpeed", applySpeed);
                yield return null;
                continue;
            }

            if (isWalking)
                applySpeed = Mathf.Lerp(applySpeed, walkSpeed, 0.1f);
            else if (isCrouching)
                applySpeed = Mathf.Lerp(applySpeed, crouchSpeed, 0.1f);
            else
                applySpeed = Mathf.Lerp(applySpeed, runSpeed, 0.1f);

            Vector3 forwardVec = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
            Vector3 rightVec = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z).normalized;

            controller.Move(forwardVec * moveDir.z * applySpeed * Time.deltaTime);
            controller.Move(rightVec * moveDir.x * applySpeed * Time.deltaTime);
            anim.SetFloat("xSpeed", moveDir.x, 0.1f, Time.deltaTime);
            anim.SetFloat("ySpeed", moveDir.z, 0.1f, Time.deltaTime);
            anim.SetFloat("MoveSpeed", applySpeed);

            Quaternion lookRotation = Quaternion.LookRotation(forwardVec * moveDir.z + rightVec * moveDir.x);
            transform.rotation = Quaternion.Lerp(lookRotation, transform.rotation, 0.1f);
            // �߼Ҹ� ���
            lastStepTime -= Time.deltaTime;
            if (lastStepTime < 0)
            {
                lastStepTime = 0.5f;
                GenerateFootStepSound();
            }
            yield return null;
        }
    }

    private void GenerateFootStepSound()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, FootStepRange());
        foreach (Collider collider in colliders)
        {
            IListenable listenable = collider.GetComponent<IListenable>();
            listenable?.Listen(transform);
        }
    }

    private float FootStepRange()
    {
        if (isWalking)
            return walkStepRange;
        else if (isCrouching)
            return crouchStepRange;
        else
            return runStepRange;
    }

    private void OnDrawGizmosSelected()
    {
        if (!debug)
            return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, walkStepRange);
        Gizmos.DrawWireSphere(transform.position, runStepRange);
        Gizmos.DrawWireSphere(transform.position, crouchStepRange);
    }

    private void OnMove(InputValue value)
    {
        moveDir.x = value.Get<Vector2>().x;
        moveDir.z = value.Get<Vector2>().y;
    }

    private void OnWalk(InputValue value)
    {
        if (!isCrouching)
            isWalking = value.isPressed;
    }

    private IEnumerator JumpRoutine()
    {
        while (true)
        {
            // �Ʒ� �������� ����ؼ� �߷��� ����
            ySpeed += Physics.gravity.y * Time.deltaTime;

            if (isGrounded && ySpeed < 0)
                ySpeed = 0;

            controller.Move(Vector3.up * ySpeed * Time.deltaTime);

            yield return null;
        }
    }

    private void OnJump(InputValue value)
    {
        if (isCrouching)
        {
            isCrouching = false;
            StartCoroutine(CrouchRoutine());
            return;
        }
        anim.SetTrigger("IsJump");
        ySpeed = jumpForce;
    }

    public IEnumerator CrouchRoutine()
    {
        if (isCrouching)
        {
            applySpeed = crouchSpeed;
            anim.SetBool("Crouching", true);
            controller.center = new Vector3(0f, 0.8f, 0f);
            controller.height = 1.4f;
        }
        else
        {
            applySpeed = walkSpeed;
            anim.SetBool("Crouching", false);
            controller.center = new Vector3(0f, 0.9f, 0f);
            controller.height = 1.8f;
        }
        yield return null;
    }

    private void OnCrouch(InputValue value)
    {
        isCrouching = !isCrouching;

        StartCoroutine(CrouchRoutine());
    }

    private void GroundCheck()
    {
        isGrounded = Physics.SphereCast(transform.position + Vector3.up * 1, 0.5f, Vector3.down, out RaycastHit hit, 0.5f);
    }
}