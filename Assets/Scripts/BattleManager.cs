using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleManager : MonoBehaviour
{
    [SerializeField] private GameObject battlePanel;

    [Header("Buttons")]
    public GameObject[] choices;

    [Header("Battlestations")]
    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    private Unit playerUnit;
    private Unit enemyUnit;

    [Header("Dialogue")]
    public TextMeshProUGUI dialogueText;

    [Header("Battle Huds")]
    public BattleHUD playerHud;
    public BattleHUD enemyHud;

    [Header("State")]
    public BattleState state;

    public bool battleHasStarted { get; private set; }

    private static BattleManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Battle Manager in the scene.");
        }

        instance = this;
    }

    public static BattleManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        battleHasStarted = false;
        battlePanel.SetActive(false);
    }

    public void SetupBattle(GameObject playerPrefab, GameObject enemyPrefab)
    {
        state = BattleState.START;
        battleHasStarted = true;
        battlePanel.SetActive(true);

        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = enemyUnit.unitName + " blocks your path.";

        playerHud.SetHUD(playerUnit);
        enemyHud.SetHUD(enemyUnit);

        StartCoroutine(PlayerGoesFirst());
    }

    private IEnumerator PlayerGoesFirst()
    {
        yield return new WaitForSeconds(2f);
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    private void PlayerTurn()
    {
        StartCoroutine(SelectFirstChoice());
        dialogueText.text = "Choose an action!";
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerHeal());
    }

    private IEnumerator PlayerAttack()
    {
        // damage the enemy
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHud.SetHP(enemyUnit.currentHP);
        dialogueText.text = "The attack is successful!";

        Debug.Log(enemyUnit.currentHP);

        yield return new WaitForSeconds(2f);

        // check if enemy is dead & change state
        if (isDead)
        {
            // end the battle
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            // enemy's turn
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    private IEnumerator PlayerHeal()
    {
        playerUnit.Heal(5);

        playerHud.SetHP(playerUnit.currentHP);

        dialogueText.text = "You feel renewed strength!";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    private IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
        
        playerHud.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        // check if player is dead & change state
        if (isDead)
        {
            // end the battle
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
        else
        {
            // player's turn
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    private IEnumerator EndBattle() 
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
        } 
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";
        }

        yield return new WaitForSeconds(2f);
        battlePanel.SetActive(false);
        battleHasStarted = false;
    }

     private IEnumerator SelectFirstChoice()
    {
        // event system requires that we clear it first, then wait for at least one frame before setting current selected object
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }
}
