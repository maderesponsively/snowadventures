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


    private bool _snowWalking = false;



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

    public void OnLanding ()
    {
        animator.SetBool("IsJumping", false);
    }

    public void StartCollecting ()
    {
        animator.SetBool("IsCollecting", true);
    }

    public void EndCollecting()
    {
        animator.SetBool("IsCollecting", false);
    }

    void FixedUpdate()
    {
        controller.Move(_horizontalMove * Time.fixedDeltaTime, false, _jump);
        _jump = false;
    }

    //private void UpdateSnowCollision()
    //{
    //    if ((_snowCollected == _snowCollectedMax) && _canCollect)
    //    {
    //        Physics2D.IgnoreLayerCollision(8, 9, true);
    //    }
    //    else if (_snowCollected < _snowCollectedMax && !_canCollect)
    //    {
    //        Physics2D.IgnoreLayerCollision(8, 9, false);
    //    }
    //}

    //public void StartCollectingSnow()
    //{
    //    if(_snowCollected <= _snowCollectedMax)
    //    {
    //        _snowWalking = true;
    //        animator.SetBool("IsCollecting", true);
    //    }
    //}

    //public void CollectedSnow()
    //{
    //    _snowCollected = _snowCollected + 0.5f;
    //    _uIManager.SetSnowLevel(_snowCollected);
    //}

    //public void StopCollectingSnow()
    //{
    //    _snowWalking = false;
    //    animator.SetBool("IsCollecting", false);
    //}

    
}
