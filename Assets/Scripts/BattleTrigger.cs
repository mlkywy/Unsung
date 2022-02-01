using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
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

        BattleManager.GetInstance().SetupBattle();
        Debug.Log("Get battle instance!");
    }
}
