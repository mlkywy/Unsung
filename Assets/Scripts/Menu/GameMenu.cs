using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject[] choices;
    private GameObject soundManager;

    private void Start()
    {
        soundManager = GameObject.FindWithTag("SoundManager");
        menuPanel.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            menuPanel.SetActive(!menuPanel.activeSelf);
            // StartCoroutine(SelectFirstChoice());
        }
    }

    public void QuitToMenu()
    {
        Destroy(soundManager);
        SceneManager.LoadScene("MainMenu");
    }

    // private IEnumerator SelectFirstChoice()
    // {
    //     // event system requires that we clear it first, then wait for at least one frame before setting current selected object
    //     EventSystem.current.SetSelectedGameObject(null);
    //     yield return new WaitForEndOfFrame();
    //     EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    // }
}
