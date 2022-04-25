using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance { get; private set; }
    public Animator transition;
    public float transitionTime = 1f;

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator SceneTransition(string sceneName)
    {
        // play animation
        transition.SetTrigger("Start");

        // wait
        yield return new WaitForSeconds(transitionTime);

        // load scene
        SceneManager.LoadScene(sceneName);
        
    }

    public IEnumerator SceneTransition(int sceneIndex)
    {
        // play animation
        transition.SetTrigger("Start");

        // wait
        yield return new WaitForSeconds(transitionTime);

        // load scene
        SceneManager.LoadScene(sceneIndex);
        
    }

}