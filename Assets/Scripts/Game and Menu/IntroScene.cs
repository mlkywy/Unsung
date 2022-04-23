using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartGameScene();
        }

        // // for testing purposes only!!!
        // if (Input.GetKeyDown(KeyCode.T))
        // {
        //     StartCoroutine(SceneLoader.instance.SceneTransition("Queen"));
        // }
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

    private void StartGameScene()
    {
        StartCoroutine(SceneLoader.instance.SceneTransition("Cave"));
        // SceneManager.LoadScene("Scene1");
    }
}
