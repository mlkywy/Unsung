using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    bool playerInRange;
    [SerializeField] TextAsset inkJSON;
    [SerializeField] string name;
    [SerializeField] Sprite portrait;


    void Awake()
    {
        playerInRange = false;
    }


    void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON, name, portrait);
                Debug.Log(inkJSON.text);
            }
        }
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player in range.");
        }
    }


     void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player left range.");
        }
    }
}
