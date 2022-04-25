using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUIController : MonoBehaviour
{
    [SerializeField] private GameObject spellPanel;
    [SerializeField] private Button[] actionButtons;
    [SerializeField] private Button button;
    [SerializeField] private GameObject[] playerHud;
    [SerializeField] private GameObject descriptionPanel;
    [SerializeField] private AudioClip selectSpellSound;

    private bool disableMouseClick = false;

    private void Start()
    {
        descriptionPanel.GetComponentInChildren<TextMeshProUGUI>().text = "";
        spellPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !disableMouseClick)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hitInfo = Physics2D.Raycast(ray.origin, ray.direction);

            // if target is friendly and heal spell is selected
            if (hitInfo.collider != null && hitInfo.collider.CompareTag("Character") && BattleController.Instance.playerSelectedSpell.spellType == Spell.SpellType.Heal)
            {
                BattleController.Instance.SelectTarget(hitInfo.collider.GetComponent<Character>());
                StartCoroutine(DisableMouse());
            }

            // if target is enemy and attack spell or regular attack selected
            if (hitInfo.collider != null && hitInfo.collider.CompareTag("Enemy")
                && (BattleController.Instance.playerIsAttacking || BattleController.Instance.playerSelectedSpell.spellType == Spell.SpellType.Attack))
            {
                BattleController.Instance.SelectTarget(hitInfo.collider.GetComponent<Character>());
                StartCoroutine(DisableMouse());
            }
        }
    }

    // prevents player from spamming
    private IEnumerator DisableMouse()
    {
        disableMouseClick = true;
        yield return new WaitForSeconds(1f);
        disableMouseClick = false;
    }

    public void ToggleSpellPanel(bool state)
    {
        spellPanel.SetActive(state);

        if (state == true)
        {
            BuildSpellList(BattleController.Instance.GetCurrentCharacter().spells);
        }
    }

    public void ToggleActionState(bool state)
    {
        ToggleSpellPanel(state);

        foreach(Button button in actionButtons)
        {
            button.interactable = state;
        }
    }

    public void BuildSpellList(List<Spell> spells)
    {
        if (spellPanel.transform.childCount > 0)
        {
            foreach(Button button in spellPanel.transform.GetComponentsInChildren<Button>())
            {
                Destroy(button.gameObject);
            }
        }

        foreach(Spell spell in spells)
        {
            Button spellButton = Instantiate<Button>(button, spellPanel.transform);
            spellButton.GetComponentInChildren<TextMeshProUGUI>().text = spell.spellName + ": (" + spell.manaCost + ")";
            spellButton.onClick.AddListener(() => SelectSpell(spell));
        }
    }

    private void SelectSpell(Spell spell)
    {
        Debug.Log("Spell selected.");
        SoundManager.instance.PlaySound(selectSpellSound);

        BattleController.Instance.playerSelectedSpell = spell;
        BattleController.Instance.playerIsAttacking = false;
        descriptionPanel.GetComponentInChildren<TextMeshProUGUI>().text = spell.spellDescription;
    }

    public void SelectAttack()
    {
        descriptionPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Select a target to attack!";
        Debug.Log("Attack selected.");
        ToggleSpellPanel(false);
        BattleController.Instance.playerSelectedSpell = null;
        BattleController.Instance.playerIsAttacking = true;
    }

    public void UpdateCharacterUI()
    {
        descriptionPanel.GetComponentInChildren<TextMeshProUGUI>().text = "";
        for (int i = 0; i < BattleController.Instance.characters[0].Count; i++)
        {
           Character character = BattleController.Instance.characters[0][i];
           playerHud[i].GetComponentInChildren<TextMeshProUGUI>().text = character.characterName;
           playerHud[i].GetComponentInChildren<Image>().sprite = character.characterSprite;

           foreach (var slider in playerHud[i].GetComponentsInChildren<Slider>(true))
           {
               if (slider.name == "HPBar")
               {
                   slider.maxValue = character.maxHealth;
                   slider.value = character.health;
                   slider.GetComponentInChildren<TextMeshProUGUI>().text = character.health.ToString();
               }
               else if (slider.name == "SPBar")
               {
                   slider.maxValue = character.maxManaPoints;
                   slider.value = character.manaPoints;
                   slider.GetComponentInChildren<TextMeshProUGUI>().text = character.manaPoints.ToString();
               }
           }
        }
    }

    public void Defend()
    {
        BattleController.Instance.GetCurrentCharacter().Defend();
    }
}
