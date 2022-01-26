using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private bool playerInRange;
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private new string name;
    [SerializeField] private Sprite portrait;


    private void Awake()
    {
        playerInRange = false;
    }


    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON, name, portrait);
                Debug.Log(inkJSON.text);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player in range.");
        }
    }


    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player left range.");
        }
    }
}
