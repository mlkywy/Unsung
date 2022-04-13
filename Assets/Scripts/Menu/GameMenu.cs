using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameMenu : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject[] choices;
    private GameObject soundManager;

    SavePlayerPos playerPosData;

    private void Start()
    {
        playerPosData = FindObjectOfType<SavePlayerPos>();
        soundManager = GameObject.FindWithTag("SoundManager");
        menuPanel.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            menuPanel.SetActive(!menuPanel.activeSelf);
            StartCoroutine(SelectFirstChoice());
        }
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("LoadSaved", 1);
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        playerPosData.PlayerPosSave();
        Debug.Log("Game has been saved!");
    }

    public void LoadSave()
    {
         if (PlayerPrefs.GetInt("LoadSaved") == 1)
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));
        }
        else 
        {
            return;
        }
    }

    public void QuitToMenu()
    {
        Destroy(soundManager);
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator SelectFirstChoice()
    {
        // event system requires that we clear it first, then wait for at least one frame before setting current selected object
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }
}
