using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;
using Ink.UnityIntegration;

public class DialogueManager : MonoBehaviour
{
     // variable for the load_globals.ink JSON
    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;

    [Header("Parameters")]
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private AudioClip clip;

    [Header("Text UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject continueIcon;

    [Header("Speaker UI")]
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private Animator portraitAnimator;
    [SerializeField] private GameObject namePanel;
    [SerializeField] private GameObject portraitFrame;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

    private Coroutine displayLineCoroutine;
    private bool canContinueToNextLine = false;
    private bool submitButtonPressedThisFrame = false;

    // tags
    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string VOICE_TAG = "voice";

    private static DialogueManager instance;

    private DialogueVariables dialogueVariables;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene.");
        }

        instance = this;

        dialogueVariables = new DialogueVariables(loadGlobalsJSON);
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
     
        namePanel.SetActive(false);
        portraitFrame.SetActive(false);

        clip = null;

        // get all of the choices text
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {

        if (Input.GetButtonDown("Submit"))
        {
            submitButtonPressedThisFrame = true;
        }

        // return right away if dialogue is not playing
        if (!dialogueIsPlaying) return;

        // continue to next line of dialogue when player input is made
        if (canContinueToNextLine
            && submitButtonPressedThisFrame
            && currentStory.currentChoices.Count == 0)
        {
            submitButtonPressedThisFrame = false;
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        submitButtonPressedThisFrame = false;
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        dialogueVariables.StartListening(currentStory);

        // make text dialogue span width of dialogue panel when there is no portrait
        dialogueText.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0.9f, 0f);
        dialogueText.GetComponent<RectTransform>().sizeDelta = new Vector2(250f, 32f);

        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        dialogueVariables.StopListening(currentStory);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        namePanel.SetActive(false);
        portraitFrame.SetActive(false);

        clip = null;
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }

            // set text for current dialogue line
            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));

            // handle tags
            HandleTags(currentStory.currentTags);
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        // set the text to the full line, but set the visible characters to 0
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;

        // hide items while text is playing
        continueIcon.SetActive(false);
        HideChoices();

        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;

        // display each letter one at a time
        foreach (char letter in line.ToCharArray())
        {
            if (submitButtonPressedThisFrame)
            {
                submitButtonPressedThisFrame = false;
                dialogueText.maxVisibleCharacters = line.Length;
                break;
            } 

            // check for rich text tag, if found, add without waiting
            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            else
            {
                if (letter != ' ' && letter != '.' && letter != ',' && letter != '!' && letter != '?')
                {
                    // play voice audio clip on each letter
                    SoundManager.instance.PlaySound(clip);
                }

                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);
            }
            yield return null; 
        }

        // actions to take after the entire line is finished displaying
        continueIcon.SetActive(true);
        DisplayChoices();

        canContinueToNextLine = true;
    }

    private void HandleTags(List<string> currentTags)
    {
        // loop through each tag and handle it accordingly
        foreach (string tag in currentTags)
        {
            // parse the tag
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            // handle the tag
            switch (tagKey)
            {
                case SPEAKER_TAG:
                    // set name panel and name text
                    namePanel.SetActive(true);
                    displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    // set portrait frame and portrait
                    portraitFrame.SetActive(true);
                    portraitAnimator.Play(tagValue);
                    // text dialogue makes room for portrait when there is one
                    dialogueText.GetComponent<RectTransform>().localPosition = new Vector3(15.3f, 0.9f, 0f);
                    dialogueText.GetComponent<RectTransform>().sizeDelta = new Vector2(200f, 32f);
                    break;
                case VOICE_TAG:
                    // set audio clip
                    clip = (AudioClip)Resources.Load(tagValue);
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }
        }
    }

    private void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }

    private void DisplayChoices()
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

    private IEnumerator SelectFirstChoice()
    {
        if (currentStory.currentChoices.Count > 0)
        {
            // event system requires that we clear it first, then wait for at least one frame before setting current selected object
            EventSystem.current.SetSelectedGameObject(null);
            yield return new WaitForEndOfFrame();
            EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
        }
    } 

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);

        if (variableValue == null)
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }

        return variableValue;
    }

    // this method will get called anytime the application quits
    // depending on game, can save variable state in other places
    public void OnApplicationQuit()
    {
        if (dialogueVariables != null)
        {
            dialogueVariables.SaveVariables();
        }
    }
}
