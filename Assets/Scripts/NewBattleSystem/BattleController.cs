using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    /* 
    players    enemies
    0          1
    0 1 2 3    0 1 2 3
    */

    const private int PLAYER_TEAM = 0;
    const private int ENEMY_TEAM = 1;

    public static BattleController Instance { get; set; }

    public Dictionary<int, List<Character>> characters = new Dictionary<int, List<Character>>();

    public int characterTurnIndex;
    public Spell playerSelectedSpell;
    public bool playerIsAttacking;

    [SerializeField] private BattleSpawnPoint[] spawnPoints;
    [SerializeField] private BattleUIController uiController;

    private int actTurn;

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
    }

    public Character GetRandomPlayer()
    {
        return characters[PLAYER_TEAM][Random.Range(0, characters[PLAYER_TEAM].Count - 1)];
    }

    public Character GetWeakestEnemy()
    {
        Character weakestEnemy;

        foreach(Character character in characters[ENEMY_TEAM])
        {
            if (character.health < weakestEnemy.health)
            {
                weakestEnemy = character;
            }
        }

        return weakestEnemy;
    }

    // private void NextTurn()
    // {
    //     // if turn 0, set act turn to 1; if 1, set act turn to 0
    //     actTurn ^= 1;
    //     if(actTurn == 1) { EnemyAct(); }
    // }

    private void NextAct()
    {
        uiController.UpdateCharacterUI();

        if (!characters[PLAYER_TEAM].All(c => c.isDead) && characters[ENEMY_TEAM].Count > 0)
        {
            if (characterTurnIndex < characters[PLAYER_TEAM].Count - 1)
            {
                characterTurnIndex++;

                if(characters[PLAYER_TEAM][characterTurnIndex].isDead)
                {
                    return;
                }
            }
            else
            {
                StartCoroutine(EnemyAct());
                uiController.ToggleActionState(false);
                characterTurnIndex = 0;
                Debug.Log("Enemy turn");
            }

            uiController.ToggleActionState(true);
            uiController.BuildSpellList(GetCurrentCharacter().spells);
        }
        else
        {
            Debug.Log("Battle over!");
        }
    }

    private IEnumerator EnemyAct()
    {
        foreach(Character character in characters[ENEMY_TEAM])
        {
            yield return new WaitForSeconds(0.75f);

            if (character.health > 0)
            {
                character.GetComponent<Enemy>().Act();
            }

            uiController.UpdateCharacterUI();

            yield return new WaitForSeconds(1f);
        }
    }

    public void SelectTarget(Character target)
    {
        if (playerIsAttacking)
        {
            var curr = GetCurrentCharacter();
            Debug.Log($"{curr.characterName} attacks {target.characterName}");
            target.Hurt(curr.attackPower);
            NextAct();
        }
        else if (playerSelectedSpell != null)
        {
            if (GetCurrentCharacter().CastSpell(playerSelectedSpell, target))
            {
                NextAct();
            }
            else 
            {
                Debug.LogWarning("Not enough mana to cast that spell!");
            }
        }
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
    }

    public Character GetCurrentCharacter()
    {
        return characters[PLAYER_TEAM][characterTurnIndex];
    }
}
