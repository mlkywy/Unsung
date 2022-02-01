using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTrigger : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;

    private void Start()
    {
        player = this.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            enemy = collider.gameObject;
            Debug.Log("Collided with enemy!");
            
            StartCoroutine(StartBattle());
        }
    }

   private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Debug.Log("Left enemy collider.");
        }
    }
    
    private IEnumerator StartBattle()
    {
        yield return new WaitForSeconds(2f);

        BattleManager.GetInstance().SetupBattle(player, enemy);
        Debug.Log("Get battle instance!");
    }
}
