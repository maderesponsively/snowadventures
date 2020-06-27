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

    //private bool _snowWalking = false;



    //[SerializeField]
    //private int _snowCollectedMax = 4;

    //[SerializeField]
    //private float _snowCollected = 0;

    //private bool _canCollect = true;

    //private UIManager _uIManager;


    // Start is called before the first frame update
    void Start()
    {
        //_uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        //_uIManager.SetPlayerSnowBarMax(_snowCollectedMax);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();


        //UpdateSnowCollision();
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

    void FixedUpdate()
    {
        controller.Move(_horizontalMove * Time.fixedDeltaTime, false, _jump);
        _jump = false;
    }

    public void OnLanding ()
    {
        animator.SetBool("IsJumping", false);
    }

    public void isCollecting (bool value)
    {
        //_snowWalking = value;
        animator.SetBool("IsCollecting", value);

    }

    public void isBuilding(bool value)
    {
        //animator.SetBool("IsCollecting", value);
    }    
}
