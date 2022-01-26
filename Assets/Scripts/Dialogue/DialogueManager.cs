using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI dialogueText;
    
    [SerializeField] GameObject namePanel;
    [SerializeField] GameObject NPCPortrait;
    [SerializeField] TMP_Text NPCName;

    Story currentStory;
    bool dialogueIsPlaying;

    static DialogueManager instance;


    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene.");
        }

        instance = this;
    }


    public static DialogueManager GetInstance()
    {
        return instance;
    }


    void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        namePanel.SetActive(false);
        NPCName.text = "";
        NPCPortrait.SetActive(false);
    }


    void Update()
    {
        // return right away if dialogue is not playing
        if (!dialogueIsPlaying) return;

        // continue to next line of dialogue when player input is made
        if (Input.GetButtonDown("Submit"))
        {
            ContinueStory();
            Debug.Log("Continue story.");
        }
    }


    public void EnterDialogueMode(TextAsset inkJSON, string name, Sprite portrait)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        // make text dialogue span width of dialogue panel when there is no portrait
        dialogueText.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0.9f, 0f);
        dialogueText.GetComponent<RectTransform>().sizeDelta = new Vector2(250f, 32f);

        if (name.Length > 0) 
        {
            namePanel.SetActive(true);
            NPCName.text = name;
        }
        if (portrait)
        {
            NPCPortrait.SetActive(true);
            NPCPortrait.GetComponent<Image>().sprite = portrait;

            // text dialogue makes room for portrait when there is one
            dialogueText.GetComponent<RectTransform>().localPosition = new Vector3(15.3f, 0.9f, 0f);
            dialogueText.GetComponent<RectTransform>().sizeDelta = new Vector2(200f, 32f);
        }

        ContinueStory();
    }


    void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        namePanel.SetActive(false);
        NPCName.text = "";
        NPCPortrait.SetActive(false);
        
    }


    void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
        }
        else
        {
            ExitDialogueMode();
            Debug.Log("No more story.");
        }
    }
}
