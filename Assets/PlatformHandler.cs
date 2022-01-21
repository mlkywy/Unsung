using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformHandler : MonoBehaviour
{
    TilemapCollider2D collider;
    PlayerMovement playerMovement;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<TilemapCollider2D>();
        player = GameObject.FindWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Colliding with platform.");
            playerMovement.allowJump = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        collider.isTrigger = false;
        Debug.Log("Exit collider!");
    }

}