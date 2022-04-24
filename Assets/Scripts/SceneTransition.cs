using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    private DialogueManager dialogueData;

    public void Start()
    {
        dialogueData = FindObjectOfType<DialogueManager>();
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && !collider.isTrigger)
        {
            StartCoroutine(SceneLoader.instance.SceneTransition(sceneToLoad));    
            PlayerPrefs.DeleteKey("Saved");
            PlayerPrefs.DeleteKey("SavedCamera");
            dialogueData.OnApplicationQuit();
        }
    }
}
