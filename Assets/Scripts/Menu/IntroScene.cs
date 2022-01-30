using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScene : MonoBehaviour
{
    private bool gameStarted;
    [SerializeField] private TextAsset inkJSON;

    private void Awake()
    {
        gameStarted = true;
    }

    private void Start()
    {
        if (gameStarted && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            Debug.Log(inkJSON.text);
        }
    }
}
