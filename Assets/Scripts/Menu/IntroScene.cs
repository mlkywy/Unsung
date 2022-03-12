using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScene : MonoBehaviour
{
    private bool gameStarted;
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private AudioClip music;

    private void Awake()
    {
        SoundManager.instance.ChangeBGM(music);
        gameStarted = true;
    }

    private void Start()
    {
        StartCoroutine(StartIntro());
    }

    private IEnumerator StartIntro()
    {
        if (gameStarted && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            yield return new WaitForSeconds(1f);
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            Debug.Log(inkJSON.text);
        }
    }
}
