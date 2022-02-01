using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 4f;
    [SerializeField] private Animator animator;
    [SerializeField] private float jumpForce = 10f;

    private float vertical;
    
    private bool isLadder = false;
    private bool isClimbing = false;
    private bool allowJump = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // freeze player if dialogue is playing
        if (DialogueManager.GetInstance().dialogueIsPlaying || BattleManager.GetInstance().battleHasStarted)
        {
            return;
            Debug.Log("Player frozen.");
        }

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

    private void FixedUpdate()
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
    private void MoveHorizontal() 
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
    private void MoveVertical()
    {
        vertical = Input.GetAxisRaw("Vertical");
        if (isLadder && Mathf.Abs(vertical) > 0f || isLadder && rb.velocity.y != 0f)
        {
            isClimbing = true; 
            Debug.Log("Climbing.");
        }
    }

    // climbing logic
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }

    // jumping logic
    private void Jump()
    {
        if (Input.GetButtonDown("Jump")) 
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            allowJump = false;
            StartCoroutine(EnableJump());
            
        }
    }

    // enable jump with coroutine to avoid double jumping
    private IEnumerator EnableJump()
    {
        yield return new WaitForSeconds(0.5f);
        allowJump = true;
    }
}
