using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLauncherDemo : MonoBehaviour
{
    [SerializeField] private List<Character> players, enemies;
    [SerializeField] private BattleLauncher launcher;
    [SerializeField] private AudioClip music;

    public void Launch()
    {
        launcher.PrepareBattle(enemies, players);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            SoundManager.instance.ChangeBGM(music);
            Launch();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
           
        }
    }
}
