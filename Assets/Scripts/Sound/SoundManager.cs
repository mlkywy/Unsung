using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    public AudioSource source;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Sound Manager in the scene.");
            Destroy(gameObject);
        }
        
        instance = this;
        DontDestroyOnLoad(this);

        source = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    public void ChangeBGM(AudioClip music)
    {
        if (source.clip.name == music.name) 
        {
            return;
        }

        source.Stop();
        source.clip = music;
        source.Play();
        Debug.Log("Changed bg musci!");
    }
}
