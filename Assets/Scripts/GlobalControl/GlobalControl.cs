using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    // player stats that we want to keep persistent across new scenes and battles
    public int unitLevel;
    public int damage;
    public int maxHP;
    public int currentHP;

    public static GlobalControl instance;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
