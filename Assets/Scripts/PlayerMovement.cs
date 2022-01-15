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

    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveHorizontal();
        MoveVertical();
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
            animator.SetBool("IsClimbing", false);
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
        }
    }

    // climbing logic
    void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }
}
