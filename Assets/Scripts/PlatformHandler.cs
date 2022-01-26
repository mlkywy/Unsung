using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHandler : MonoBehaviour
{
    [SerializeField] PlatformEffector2D effector;
    [SerializeField] new bool collider;


    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }


    void Update()
    {
        if (collider && Input.GetKeyDown(KeyCode.S) || collider && Input.GetKeyDown(KeyCode.DownArrow))
        {
            effector.rotationalOffset = 180f;
            effector.surfaceArc = 210f;
            StartCoroutine(Wait());
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           collider = true;
        }
    }


    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           collider = false;
        }
    }


    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.25f);
        effector.rotationalOffset = 0f;
        effector.surfaceArc = 150f;
    }
}
