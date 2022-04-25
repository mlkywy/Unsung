using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject[] frame;
    [SerializeField] private GameObject[] choices;
    [SerializeField] private AudioClip music;

    private void Start()
    {
        frame[1].SetActive(false);
    }

    public void Update()
    {
        if (Input.anyKeyDown && frame[0].activeInHierarchy)
        {
            frame[0].SetActive(false);
            frame[1].SetActive(true);
            StartCoroutine(SelectFirstChoice());
        }

        if (Input.GetButtonDown("Cancel") && !frame[0].activeInHierarchy)
        {
            frame[0].SetActive(true);
            frame[1].SetActive(false);
        }
    }

    public void StartGame()
    {
        PlayerPrefs.DeleteAll();

        StartCoroutine(SceneLoader.instance.SceneTransition("Intro"));
    }

    public void ContinueGame()
    {
        if (PlayerPrefs.GetInt("LoadSaved") == 1)
        {
            StartCoroutine(SceneLoader.instance.SceneTransition(PlayerPrefs.GetInt("SavedScene")));
        }
        else 
        {
            return;
        }
    }

    private void QuitGame() 
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
