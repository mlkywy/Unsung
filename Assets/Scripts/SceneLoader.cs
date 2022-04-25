using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance { get; private set; }
    public Animator transition;
    public float transitionTime = 1f;

    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 newPosition;

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator TeleportPlayer()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        transition.SetTrigger("End");

        player.transform.position = newPosition;
    }

    public IEnumerator SceneTransition(string sceneName)
    {
        // play animation
        transition.SetTrigger("Start");

        // wait
        yield return new WaitForSeconds(transitionTime);

        // load scene
        SceneManager.LoadSceneAsync(sceneName);
        
    }

    public IEnumerator SceneTransition(int sceneIndex)
    {
        // play animation
        transition.SetTrigger("Start");

        // wait
        yield return new WaitForSeconds(transitionTime);

        // load scene
        SceneManager.LoadSceneAsync(sceneIndex);
        
    }

}