using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;
    private float _horizontalMove = 0f;
    private bool _jump = false;

    [SerializeField]
    private int _snowCollected = 0;
    [SerializeField]
    private int _snowCollectedMax = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(_horizontalMove));
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _jump = true;
            animator.SetBool("IsJumping", true);
        }
    }

    public void OnLanding ()
    {
        animator.SetBool("IsJumping", false);
    }

    void FixedUpdate()
    {
        // Move our Character
        controller.Move(_horizontalMove * Time.fixedDeltaTime, false, _jump);
        _jump = false;
    }

    public void CollectSnow()
    {
        if(_snowCollected <= _snowCollectedMax)
        {
            animator.SetBool("IsCollecting", true);
        }
    }

    public void CollectedSnow()
    {
        _snowCollected++;
        animator.SetBool("IsCollecting", false);
    }

    public void StopCollectingSnow()
    {
        animator.SetBool("IsCollecting", false);
    }
}
