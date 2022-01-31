using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("");
        }

        instance = this;
    }

    public static Player GetInstance()
    {
        return instance;
    }
}
