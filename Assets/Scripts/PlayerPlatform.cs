using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerPlatform : MonoBehaviour
{
    GameObject currentPlatform;
    [SerializeField] BoxCollider2D playerCollider;
    [SerializeField] CompositeCollider2D platformCollider;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Key down!");
            if (currentPlatform != null)
            {
                Debug.Log("Start Coroutine!");
                StartCoroutine(DisableCollision());
            }
        }
    }

    // climbing logic
    void OnCollisionEnter2D(Collision2D collision) 
    {
       if (collision.gameObject.CompareTag("Platform"))
       {
           currentPlatform = collision.gameObject;
       }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
       {
           currentPlatform = null;
       }
    }

    IEnumerator DisableCollision()
    {
        platformCollider = currentPlatform.GetComponent<CompositeCollider2D>();
        Physics2D.IgnoreCollision(playerCollider, platformCollider);

        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
}
