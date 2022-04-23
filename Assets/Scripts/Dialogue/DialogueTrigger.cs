using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private bool playerInRange;
    [SerializeField] private TextAsset inkJSON;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        playerInRange = false;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            if (Input.GetButtonDown("Interact"))
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                // Debug.Log(inkJSON.text);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerInRange = true;
            // Debug.Log("Player in range.");

            // flip character to try and face player before dialogue starts?
            if (spriteRenderer != null && collider.GetComponent<SpriteRenderer>().flipX)
            {
                spriteRenderer.flipX = false;
            }
            else if (spriteRenderer != null && !collider.GetComponent<SpriteRenderer>().flipX)
            {
                spriteRenderer.flipX = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerInRange = false;

            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = false;
            }
        }
    }
}
