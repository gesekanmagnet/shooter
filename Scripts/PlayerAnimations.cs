using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public float speed = 4f;
    public float rotSpeed = 80f;
    public float rot = 0f;
    public float gravity = 8f;
    Animator animator;
    Vector3 moveDir = Vector3.zero;
    
    CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        GetInput();
        SwapAK47();
        SwapShotgun();
    }

    void Movement()
    {
        if(controller.isGrounded)
        {
            if(Input.GetKeyDown(KeyCode.W))
            {
                if(animator.GetBool("Shot") == true)
                {
                    return;
                }
                else if(animator.GetBool("Shot") == false)
                {
                    animator.SetBool("Run", true);
                    animator.SetInteger("Condition", 1);
                    moveDir = new Vector3(0, 0, 1);
                    moveDir *= speed;
                    moveDir = transform.TransformDirection(moveDir);
                }
            }

            if(Input.GetKeyDown(KeyCode.W))
            {
                animator.SetBool("Run", false);
                animator.SetInteger("Condition", 0);
                moveDir = new Vector3(0, 0, 0);
            }

            rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, rot, 0);
            moveDir.y -= gravity * Time.deltaTime;
            controller.Move(moveDir * Time.deltaTime);
        }
    }

    void GetInput()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(animator.GetBool("Run") == true)
            {
                animator.SetBool("Run", false);
                animator.SetInteger("Condition", 0);
            }
            
            if(animator.GetBool("Run") == false)
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        animator.SetBool("Shot", true);
        animator.SetInteger("Condition", 2);
        yield return new WaitForSeconds(0.5f);
        animator.SetInteger("Condition", 0);
        animator.SetBool("Shot", false);
    }

    void SwapAK47()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.SetBool("SwitchWeapon", true);
            animator.SetInteger("Condition", 3);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.SetBool("SwitchWeapon", false);
            animator.SetInteger("Condition", 0);
        }
    }

    void SwapShotgun()
    {
        if(Input.GetKey(KeyCode.Alpha2))
        {
            animator.SetBool("SwitchWeapon", true);
            animator.SetInteger("Condition", 3);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.SetBool("SwitchWeapon", false);
            animator.SetInteger("Condition", 0);
        }
    }
}
