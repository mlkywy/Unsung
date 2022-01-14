using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHandler : MonoBehaviour
{
    BoxCollider2D boxCollider;
    CharacterMovement characterMovement;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        player = GameObject.FindWithTag("Player");
        characterMovement = player.GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("This is colliding.");
            characterMovement.allowJump = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        characterMovement.allowJump = false;
        boxCollider.enabled = false;
    }


}
