using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour
{
    /* 
    players    enemies
    0          1
    0 1 2 3    0 1 2 3
    */

    private const int PLAYER_TEAM = 0;
    private const int ENEMY_TEAM = 1;

    public static BattleController Instance { get; set; }

    public Dictionary<int, List<Character>> characters = new Dictionary<int, List<Character>>();

    public int characterTurnIndex = 0;
    public Spell playerSelectedSpell;
    public bool playerIsAttacking;

    [Header("Parameters")]
    [SerializeField] private BattleSpawnPoint[] spawnPoints;
    [SerializeField] private BattleUIController uiController;
    [SerializeField] private BattleDialogue dialogueController;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;

    private int actTurn;

    public string currentEnemyKey;

    private void Start()
    {
        if  (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        
        characters.Add(0, new List<Character>());
        characters.Add(1, new List<Character>());
        FindObjectOfType<BattleLauncher>().Launch();
        uiController.UpdateCharacterUI();
        
        // prevents incorrect battle launching in world scene
        Destroy(GameObject.FindWithTag("BattleLauncher"));
    }

    public void StartBattle(List<Character> players, List<Character> enemies)
    {
        Debug.Log("Setup battle!");

        for (int i = 0; i < players.Count; i++)
        {
            characters[PLAYER_TEAM].Add(spawnPoints[i + 4].Spawn(players[i]));
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            characters[ENEMY_TEAM].Add(spawnPoints[i].Spawn(enemies[i]));
        }

        dialogueController.SetText($"The battle has begun! It is {characters[PLAYER_TEAM][0].characterName}'s turn!");
    }

    private IEnumerator TurnDelay()
    {
        yield return new WaitForSeconds(1f);
        NextAct();
    }

    // function for the enemy to select a random target that is still alive
    public Character GetRandomPlayer()
    {
        // check for alive characters
       List<Character> eligibleCharacters = new List<Character>();
       
       for (int i = 0; i < characters[PLAYER_TEAM].Count; i++)
       {
           if (!characters[PLAYER_TEAM][i].isDead)
           {
               eligibleCharacters.Add(characters[PLAYER_TEAM][i]);
           }
       }
       
       // check if people are alive
       if (characters[PLAYER_TEAM].All(c => c.isDead))
       {
           Debug.Log("Battle over!");
           // call function to end battle
           StartCoroutine(EndBattle());
       }
       
       return eligibleCharacters[Random.Range(0, eligibleCharacters.Count)];
    }

    // function for the enemy to heal their own members (if one of them has a heal spell)
    public Character GetWeakestEnemy()
    {
        Character weakestEnemy = characters[ENEMY_TEAM][0];

        foreach(Character character in characters[ENEMY_TEAM])
        {
            if (character.health < weakestEnemy.health)
            {
                weakestEnemy = character;
            }
        }

        return weakestEnemy;
    }

    // get current party member (whose turn it is)
    public Character GetCurrentCharacter()
    {
        return characters[PLAYER_TEAM][characterTurnIndex];
    }

    // controls which party member goes
    private void NextAct()
    {
        uiController.UpdateCharacterUI();

        if (!characters[PLAYER_TEAM].All(c => c.isDead) && characters[ENEMY_TEAM].Count > 0)
        {
            if (characterTurnIndex < characters[PLAYER_TEAM].Count - 1)
            {
                characterTurnIndex++;

                // if a character is dead, skip their turn
                while (characters[PLAYER_TEAM][characterTurnIndex].isDead)
                {
                    Debug.Log("This character is dead!");
                    characterTurnIndex++;

                    if (characterTurnIndex > characters[PLAYER_TEAM].Count - 1)
                    {
                        characterTurnIndex = -1;
                        uiController.ToggleActionState(false);
                        uiController.ToggleSpellPanel(false);
                        StartCoroutine(EnemyAct());
                        dialogueController.SetText("The opposition prepares their attack!");
                    }
                }

                Debug.Log(characterTurnIndex);
                dialogueController.SetText($"It is {characters[PLAYER_TEAM][characterTurnIndex].characterName}'s turn!");

                uiController.ToggleActionState(true);
                uiController.ToggleSpellPanel(false);
                uiController.BuildSpellList(GetCurrentCharacter().spells);
            }
            else
            {
                characterTurnIndex = -1;
                uiController.ToggleActionState(false);
                uiController.ToggleSpellPanel(false);
                StartCoroutine(EnemyAct());
                dialogueController.SetText("The opposition prepares their attack!");
                // Debug.Log("Enemy turn");
            }
        }
        else
        {
            Debug.Log("Battle over!");
            // call function to end battle
            StartCoroutine(EndBattle());
        }
    }

    private IEnumerator EnemyAct()
    {
        foreach(Character character in characters[ENEMY_TEAM])
        {
            yield return new WaitForSeconds(1f);

            if (character.health > 0)
            {
                character.GetComponent<Enemy>().Act();
            }

            uiController.UpdateCharacterUI();

            yield return new WaitForSeconds(1f);
        }

        NextAct();
    }

    public void SelectTarget(Character target)
    {
        if (playerIsAttacking)
        {
            PlayerDoAttack(GetCurrentCharacter(), target);
        }
        else if (playerSelectedSpell != null)
        {
            if (GetCurrentCharacter().CastSpell(playerSelectedSpell, target))
            {
                // dialogueController.SetText($"{playerSelectedSpell.spellName} was cast on {target.characterName}!");
                StartCoroutine(TurnDelay());
            }
            else 
            {
                dialogueController.SetText("There is not enough mana to cast that spell.");
                // Debug.LogWarning("There is not enough mana to cast that spell.");
            }
        }
    }

    public void PlayerDoAttack(Character attacker, Character target)
    {
        dialogueController.SetText($"{attacker.characterName} attacks {target.characterName} for {attacker.attackPower} damage!");
        // Debug.Log(attacker.characterName + " attacks " + target.characterName);

        ScreenShakeController.instance.StartShake(0.2f, 0.05f);
        SoundManager.instance.PlaySound(attackSound);

        target.Hurt(attacker.attackPower);
        StartCoroutine(TurnDelay());
    }

    public void EnemyDoAttack(Character attacker, Character target)
    {
        dialogueController.SetText($"{attacker.characterName} attacks {target.characterName} for {attacker.attackPower} damage!");
        // Debug.Log(attacker.characterName + " attacks " + target.characterName);

        ScreenShakeController.instance.StartShake(0.2f, 0.05f);
        SoundManager.instance.PlaySound(attackSound);

        target.Hurt(attacker.attackPower);
    }

    private IEnumerator EndBattle() 
    {
        // if enemies are dead, you win
        if (characters[ENEMY_TEAM].Count == 0)
        {
            // set boolean value to 1, so that enemy is set inactive in game world
            PlayerPrefs.SetInt(currentEnemyKey, 1);
            dialogueController.SetText("You have successfully defeated the opposition!");
            SoundManager.instance.PlaySound(winSound);
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));
        } 
        // if all party members are dead, you lose
        else if (characters[PLAYER_TEAM].All(c => c.isDead))
        {
            // set boolean value to 0, so that enemy is set active in game world
            PlayerPrefs.SetInt(currentEnemyKey, 0);
            dialogueController.SetText("You have been defeated.");
            SoundManager.instance.PlaySound(loseSound);
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("GameOver");
        }

        yield return null;
    }
}
