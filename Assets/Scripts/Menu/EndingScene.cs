using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScene : MonoBehaviour
{
    private bool cutsceneStarted;
    [SerializeField] private TextAsset inkJSON;

    private void Awake()
    {
        SoundManager.instance.StopMusic();
        cutsceneStarted = true;
    }

    private void Start()
    {
        StartCoroutine(StartCutscene());
    }

    private IEnumerator StartCutscene()
    {
        if (cutsceneStarted && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            yield return new WaitForSeconds(1f);
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            Debug.Log(inkJSON.text);
        }
    }

}
