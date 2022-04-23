using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEventTrigger : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSON;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (!DialogueManager.GetInstance().dialogueIsPlaying)
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                Destroy(gameObject);
            }
        }
    }
}
