using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameMenu : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject[] choices;

    public GameObject popup;

    private GameObject soundManager;
    private GameObject postProcessing;
    private GameObject battleLauncher;
    
    [SerializeField] private AudioClip menuToggleSound;

    private SavePlayerPos playerPosData;
    private ParallaxBackground cameraPosData;
    private DialogueManager dialogueData;

    private void Start()
    {
        playerPosData = FindObjectOfType<SavePlayerPos>();
        cameraPosData = FindObjectOfType<ParallaxBackground>();
        dialogueData = FindObjectOfType<DialogueManager>();

        soundManager = GameObject.FindWithTag("SoundManager");
        battleLauncher = GameObject.FindWithTag("BattleLauncher");

        menuPanel.SetActive(false);
        popup.SetActive(false);
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
        dialogueData.OnApplicationQuit();

        if (cameraPosData != null) 
        {
            cameraPosData.CameraPosSave();
        }

        StartCoroutine(ShowPopup());

        Debug.Log("Game has been saved!");
    }

    private IEnumerator ShowPopup()
    {
        popup.SetActive(true);

        yield return new WaitForSeconds(1f);
        popup.SetActive(false);
    }

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
