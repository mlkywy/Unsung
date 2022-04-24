using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ferryman : MonoBehaviour
{
    private bool startNextScene = false;

    private void Update()
    {
        bool ferrymanRide = ((Ink.Runtime.BoolValue) DialogueManager.GetInstance().GetVariableState("ferryman_ride")).value;

        if (ferrymanRide && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            startNextScene = true;
            
            if (Input.GetButtonDown("Submit") && startNextScene)
            {
                Debug.Log("Loading scene now.");
                GoNext();
            } 
        }
    }

    private void GoNext()
    {
        StartCoroutine(SceneLoader.instance.SceneTransition("Hole")); 
    }

    // private void OnTriggerEnter2D(Collider2D collider)
    // {
    //     if (collider.CompareTag("Player") && startNextScene)
    //     {
    //         GoNext();
    //     }
    // }

    //  private void OnTriggerExit2D(Collider2D collider)
    // {
    //     if (collider.CompareTag("Player") && startNextScene)
    //     {
    //         GoNext();
    //     }
    // }
}
