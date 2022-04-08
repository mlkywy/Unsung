using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ferryman : MonoBehaviour
{
    private void Update()
    {
        bool ferrymanRide = ((Ink.Runtime.BoolValue) DialogueManager.GetInstance().GetVariableState("ferryman_ride")).value;

        if (ferrymanRide)
        {
            if (DialogueManager.GetInstance().dialogueIsPlaying)
            {
                return;
            } 
            else 
            {
                StartCoroutine(NextArea()); 
            }
        }
    }

    private IEnumerator NextArea()
    {
        yield return new WaitForSeconds(2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene2");
    }
}
