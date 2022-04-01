using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public string spellName;
    public int power;
    public int manaCost;

    public enum SpellType { Attack, Heal }
    public SpellType spellType;

    [SerializeField] private Vector3 targetPosition;

    public GameObject DialoguePanel;
    public BattleDialogue dialogueController;

    private void Update()
    {
        if (targetPosition != Vector3.zero)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, .15f);
            if (Vector3.Distance(transform.position, targetPosition) < 0.25f)
            {
                Destroy(this.gameObject, 1);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Cast(Character target)
    {
        DialoguePanel = GameObject.FindWithTag("DialoguePanel");
        dialogueController = DialoguePanel.GetComponent<BattleDialogue>();

        targetPosition = target.transform.position;
        // Debug.Log(spellName + "was cast on " + target.characterName + "!");
        dialogueController.SetText($"{spellName} was cast on {target.characterName}!");

        if (spellType == SpellType.Attack)
        {
            // hurt target
            target.Hurt(power);
        }
        else if (spellType == SpellType.Heal)
        {
            // heal target
            target.Heal(power);
        }
    }
}
