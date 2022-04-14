using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSavePosition : MonoBehaviour
{
    SavePlayerPos playerPosData;

    private void Start()
    {
        playerPosData = FindObjectOfType<SavePlayerPos>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("LoadSaved", 1);
            PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
            playerPosData.PlayerPosSave();
            Debug.Log("Game has been saved!");
        }
    }
}
