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
    private GameObject postProcessing;
    private GameObject battleLauncher;
    
    [SerializeField] private AudioClip menuToggleSound;

    SavePlayerPos playerPosData;

    private void Start()
    {
        playerPosData = FindObjectOfType<SavePlayerPos>();

        soundManager = GameObject.FindWithTag("SoundManager");
        battleLauncher = GameObject.FindWithTag("BattleLauncher");

        menuPanel.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (DialogueManager.GetInstance().dialogueIsPlaying)
            {
                return;
            }

            SoundManager.instance.PlaySound(menuToggleSound);
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

    // this function messes things up for some reason??
    // public void LoadSave()
    // {
    //      if (PlayerPrefs.GetInt("LoadSaved") == 1)
    //     {
    //         StartCoroutine(SceneLoader.instance.SceneTransition(PlayerPrefs.GetInt("SavedScene")));
    //         // SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));
    //     }
    //     else 
    //     {
    //         return;
    //     }
    // }

    public void QuitToMenu()
    {
        // destroy sound manager before returning to main menu screen
        Destroy(soundManager);

        // prevents incorrect battle launching in world scene
        Destroy(battleLauncher);

        StartCoroutine(SceneLoader.instance.SceneTransition("MainMenu"));
        // SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator SelectFirstChoice()
    {
        // event system requires that we clear it first, then wait for at least one frame before setting current selected object
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }
}
