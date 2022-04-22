using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ferryman : MonoBehaviour
{
    private void Update()
    {
        bool ferrymanRide = ((Ink.Runtime.BoolValue) DialogueManager.GetInstance().GetVariableState("ferryman_ride")).value;

        if (ferrymanRide)
        {
            if (DialogueManager.GetInstance().dialogueIsPlaying)
            {
                Debug.Log("Dialogue is playing!");
                return;
            } 

            Debug.Log("Loading scene now.");
            StartCoroutine(SceneLoader.instance.SceneTransition("Hole")); 
        }
    }
}
