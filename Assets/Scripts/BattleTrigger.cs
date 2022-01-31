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
            StartCoroutine(ChangeScenes());
        }
    }

   private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Debug.Log("Left enemy collider.");
        }
    }
    
    private IEnumerator ChangeScenes()
    {
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("BattleScreen");
    }
}
