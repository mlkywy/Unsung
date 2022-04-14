using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] choices;
    [SerializeField] private AudioClip music;
    private GameObject soundManager;

    private void Start()
    {
        SoundManager.instance.ChangeBGM(music);
        soundManager = GameObject.FindWithTag("SoundManager");
        StartCoroutine(SelectFirstChoice()); 
    }

    public void MainMenu()
    {
        Destroy(soundManager);
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame() 
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    private IEnumerator SelectFirstChoice()
    {
        // event system requires that we clear it first, then wait for at least one frame before setting current selected object
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }
}
