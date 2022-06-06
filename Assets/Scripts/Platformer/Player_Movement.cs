using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    [SerializeField] float ladderPower;
    [SerializeField] float gravity;
    [SerializeField] float jumpTimeStart;
    [SerializeField] float scale;

    private float jumpTime;
    private float horizontal;
    private float vertical;
    private bool canJump = false;
    private bool isOnLadder = false;
    private bool isJumping = false;

    Rigidbody2D rb;
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Move();
        Jump();
        Ladder();
        GravityManagement();
    }
    
    void Move()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if(rb.velocity.x > 0f)
        {
            transform.localScale = new Vector2(scale, scale);
        }
        else if(rb.velocity.x < 0f)
        {
            transform.localScale = new Vector2(-scale, scale);
        }

        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

    void Jump()
    {
        if(vertical > 0.1f && canJump)
        {
            isJumping = true;
            jumpTime = jumpTimeStart;
            rb.velocity = new Vector2(rb.velocity.x, vertical * jumpPower);
        }

        if(vertical > 0.1f && isJumping)
        {
            if(jumpTime > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, vertical * jumpPower);
                jumpTime -= Time.deltaTime;
            }
        }
        else
        {
            isJumping = false;
        }

        if(vertical == 0f)
        {
            isJumping = false;
        }
        
        animator.SetBool("Grounded", canJump);
    }

    void Ladder()
    {
        if(isOnLadder)
        {
            rb.velocity = new Vector2(rb.velocity.x, vertical * ladderPower);
        }
    }

    void GravityManagement()
    {
        if(!isOnLadder)
        {   
            if(rb.velocity.y > 0.001f)
            {
                rb.gravityScale = 1.5f;
            }
            else
            {
                rb.gravityScale = gravity;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            canJump = true;
        }
        else if(collision.gameObject.tag == "Ladder")
        {
            isOnLadder = true;
            rb.gravityScale = 0;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            canJump = false;
        }
        else if(collision.gameObject.tag == "Ladder")
        {
            isOnLadder = false;
            rb.gravityScale = gravity;
        }
    }
}
