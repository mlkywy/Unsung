using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public float speed;
    float vertical;
    
    bool isLadder = false;
    bool isClimbing = false;
    bool allowJump = true;

    public Animator animator;

    [SerializeField] float jumpForce;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        MoveHorizontal();
        MoveVertical();
        if (allowJump) Jump();

        if (isClimbing && rb.velocity.y != 0f)
        {
          animator.SetFloat("IsClimbing", Mathf.Abs(vertical));
        }
        else 
        {
          animator.SetFloat("IsClimbing", 0f);
        }
    }


    void FixedUpdate()
    {
        if (isClimbing) 
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
        } 
        else 
        {
            rb.gravityScale = 4f;
        }
    }


    // walking
    void MoveHorizontal() 
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float moveBy = horizontal * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);

        // walk animation
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        // flip the character
        Vector3 playerScale = transform.localScale;
        if (horizontal < 0f) {
            playerScale.x = -1;
        }

        if (horizontal > 0f) {
            playerScale.x = 1;
        }

        transform.localScale = playerScale;
    }


    // climbing
    void MoveVertical()
    {
        vertical = Input.GetAxisRaw("Vertical");
        if (isLadder && Mathf.Abs(vertical) > 0f || isLadder && rb.velocity.y != 0f)
        {
            isClimbing = true; 
            Debug.Log("Climbing.");
        }
    }


    // climbing logic
    void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }


    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }


    // jumping logic
    void Jump()
    {
        if (Input.GetButtonDown("Jump")) 
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            allowJump = false;
            StartCoroutine(EnableJump());
            
        }
    }


    // enable jump with coroutine to avoid double jumping
    IEnumerator EnableJump()
    {
        yield return new WaitForSeconds(0.5f);
        allowJump = true;
    }
}
