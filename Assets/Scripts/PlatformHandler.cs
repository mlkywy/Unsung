using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHandler : MonoBehaviour
{
    [SerializeField] PlatformEffector2D effector;
    public float waitTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            waitTime = 0.5f;
        }



        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (waitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                effector.surfaceArc = 210f;
                waitTime = 0.5f;
            } 
            else 
            {
                waitTime -= Time.deltaTime;
            }
        }

        if (Input.GetButtonDown("Jump")) 
        {
            effector.rotationalOffset = 0f;
            effector.surfaceArc = 150f;
        }
    }
}
