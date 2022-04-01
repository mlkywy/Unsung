using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleDialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;

    public void SetText(string text)
    {
        dialoguePanel.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }
}
