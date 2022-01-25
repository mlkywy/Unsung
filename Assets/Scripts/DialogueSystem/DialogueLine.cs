using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass
    {
        [Header ("Text Options")]
        [TextArea] [SerializeField] string input;
        [SerializeField] TMP_Text textHolder;
        [SerializeField] Color textColor;
        [SerializeField] TMP_FontAsset textFont;

        [Header ("Timing")]
        [SerializeField] float delay;

        [Header ("Sound")]
        [SerializeField] AudioClip sound;

        void Awake()
        {
            textHolder = GetComponent<TMP_Text>();

            StartCoroutine(WriteText(input, textHolder, textColor, textFont, delay, sound));
            Debug.Log("Text is showing up on screen.");
        }
    }

}

