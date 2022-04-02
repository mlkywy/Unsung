using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeController : MonoBehaviour
{
    public static ScreenShakeController instance;

    [SerializeField] private float shakeTimeRemaining;
    [SerializeField] private float shakePower;
    [SerializeField] private float shakeRotation;

    public float rotationMultiplier = 25f;

    private void Start()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Screen Shake Controller in the scene.");
        }

        instance = this;
    }

    // for testing purposes!
    
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.K))
    //     {
    //         StartShake(0.2f, 0.05f);
    //     }
    // }

    private void LateUpdate()
    {
        if (shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;

            float xAmount = Random.Range(-1f, 1f) * shakePower;
            float yAmount = Random.Range(-1f, 1f) * shakePower;
            float rotationAmount = Random.Range(-1f, 1f) * shakeRotation;

            Debug.Log("Shaking camera!");
            transform.position += new Vector3(xAmount, yAmount, 0f);
            transform.rotation = Quaternion.Euler(0f, 0f, rotationAmount);
        }
        else
        {
            Debug.Log("Resetting camera!");
            transform.position = new Vector3(0f, 0f, -10f);
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        
    }


    public void StartShake(float length, float power)
    {
        shakeTimeRemaining = length;
        shakePower = power;

        shakeRotation = power * rotationMultiplier;
    }
}
