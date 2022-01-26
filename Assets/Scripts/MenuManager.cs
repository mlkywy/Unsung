using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject[] frame;


    private void Start()
    {
        frame[1].SetActive(false);
    }

    
    private void Update()
    {
        if (Input.anyKeyDown && frame[0].activeInHierarchy)
        {
            frame[0].SetActive(false);
            frame[1].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !frame[0].activeInHierarchy)
        {
            frame[0].SetActive(true);
            frame[1].SetActive(false);
        }
    }


    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void QuitGame() 
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
