using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLauncherDemo : MonoBehaviour
{
    [SerializeField] private List<Character> players, enemies;
    [SerializeField] private BattleLauncher launcher;
    [SerializeField] private AudioClip music;

    public string enemyKey;

    public void Start()
    {
        // load PlayerPrefs value for enemy name key and inactivate if it has been triggered
        if (PlayerPrefs.GetInt(enemyKey) == 1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public void Launch()
    {
        launcher.PrepareBattle(enemies, players, enemyKey);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            SoundManager.instance.ChangeBGM(music);
            PlayerPrefs.SetInt(enemyKey, 1);
            Launch();
        }
    }
}
