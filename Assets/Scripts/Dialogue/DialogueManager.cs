using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

    [Header("Choices UI")]
    [SerializeField] GameObject[] choices;
    TextMeshProUGUI[] choicesText;

    Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

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

        // get all of the choices text
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
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
            // set text for current dialogue line
            dialogueText.text = currentStory.Continue();

            // display choices, if any, for this dialogue line
            DisplayChoices();
        }
        else
        {
            ExitDialogueMode();
            Debug.Log("No more story.");
        }
    }


    void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // check to make sure the UI can support the number of choices coming in
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " + currentChoices.Count);
        }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        // go through remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }


    IEnumerator SelectFirstChoice()
    {
        // event system requires that we clear it first, then wait for at least one frame before setting current selected object
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
        Debug.Log("Set object in event system.");
    }
}
