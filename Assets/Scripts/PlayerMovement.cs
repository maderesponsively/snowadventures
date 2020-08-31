using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 25f;
    public float snowWalkSpeed = 15f;
    private float _horizontalMove = 0f;
    private bool _jump = false;

    public ParticleSystem SnowDustParticles;

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {

        // TODO: Add mobile option. If player is holding down screen and not press left, right or jump.

        // if(!Input.GetMouseButton(0)) {
            _horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            animator.SetFloat("Speed", Mathf.Abs(_horizontalMove));
        // } else {
        //     _horizontalMove = 0;
        // }

        
    }

    // Disable movement if aiming snowball

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _jump = true;
            animator.SetBool("IsJumping", true);
            CreateDust();
        }
    }

    void FixedUpdate()
    {
        controller.Move(_horizontalMove * Time.fixedDeltaTime, false, _jump);
        _jump = false;
    }

    void CreateDust()
    {
        SnowDustParticles.Play();
    }

    public void OnLanding ()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnFlip()
    {
        CreateDust();
    }

    // MOVE TO SNOW PROCESS
    public void isCollecting (bool value)
    {
        animator.SetBool("IsCollecting", value);

    }

    // MOVE TO SNOWMAN PROCESS
    public void isBuilding(bool value)
    {
        animator.SetBool("IsBuilding", value);
    }
}
