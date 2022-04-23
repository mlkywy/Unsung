using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScene : MonoBehaviour
{
    private bool cutsceneStarted;
    [SerializeField] private TextAsset inkJSON;
    private GameObject soundManager;

    private void Awake()
    {
        SoundManager.instance.StopMusic();
        cutsceneStarted = true;
        soundManager = GameObject.FindWithTag("SoundManager");
    }

    private void Start()
    {
        StartCoroutine(StartCutscene());
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit") && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            Destroy(soundManager);
            StartCoroutine(SceneLoader.instance.SceneTransition("MainMenu"));
        }
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
