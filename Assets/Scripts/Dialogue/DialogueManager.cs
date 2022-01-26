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

        if (name.Length > 0) 
        {
            namePanel.SetActive(true);
            NPCName.text = name;
        }
        if (portrait)
        {
            NPCPortrait.SetActive(true);
            NPCPortrait.GetComponent<Image>().sprite = portrait;
        }

        ContinueStory();
    }


    void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        namePanel.SetActive(false);
        NPCName.text = "";
        NPCPortrait.SetActive(false);
        dialogueText.text = "";
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
