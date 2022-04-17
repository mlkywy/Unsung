using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessing : MonoBehaviour
{
    public static PostProcessing instance { get; private set; }

     private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Post Processing in the scene.");
            Destroy(gameObject);
        }
        
        instance = this;
        DontDestroyOnLoad(this);
    }
}
