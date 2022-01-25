using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    bool playerInRange;

    [Header("Ink JSON")]
    [SerializeField] TextAsset inkJSON;


    void Awake()
    {
        playerInRange = false;
    }


    void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown("space"))
            {
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
        }
    }
}
