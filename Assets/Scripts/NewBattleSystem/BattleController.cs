using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BattleController : MonoBehaviour
{
    /* 
    players    enemies
    0          1
    0 1 2 3    0 1 2 3
    */

    public static BattleController Instance { get; set; }

    public Dictionary<int, List<Character>> characters = new Dictionary<int, List<Character>>();

    public int characterTurnIndex = 0;
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

    
    public void StartBattle(List<Character> players, List<Character> enemies)
    {
        Debug.Log("Setup battle!");

        for (int i = 0; i < players.Count; i++)
        {
            characters[0].Add(spawnPoints[i + 4].Spawn(players[i]));
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            characters[1].Add(spawnPoints[i].Spawn(enemies[i]));
        }
    }

    public Character GetRandomPlayer()
    {
        var alive = characters[0].Where(c => !c.isDead).ToList();

        return alive[Random.Range(0, alive.Count - 1)];
       
        // return characters[0][Random.Range(0, characters[0].Count - 1)];
    }

    public Character GetWeakestEnemy()
    {
        Character weakestEnemy = characters[1][0];

        foreach(Character character in characters[1])
        {
            if (character.health < weakestEnemy.health)
            {
                weakestEnemy = character;
            }
        }

        return weakestEnemy;
    }

    // switches between players and enemy turn
    private void NextTurn()
    {
        // if turn 0, set act turn to 1; if 1, set act turn to 0
        // actTurn = actTurn == 0 ? 1 : 0;
        actTurn ^= 1;
    }

    // each turn of each character
    private void NextAct()
    {
        // if (characters[0].Count > 0 && characters[1].Count > 0)
        if (!characters[0].All(c => c.isDead) && characters[1].Count > 0)
        {
            // while characters are able to make their turn
            while (characterTurnIndex < 4)
            {
                // skip the turns of dead characters
                while (characters[0][characterTurnIndex].isDead)
                {
                    Debug.Log($"Skipped character! {characterTurnIndex}");
                    characterTurnIndex++;  
                }

                // players chooses act
                switch(actTurn)
                {
                    case 0:
                        uiController.ToggleActionState(true);
                        uiController.BuildSpellList(GetCurrentCharacter().spells);
                        break;
                    case 1:
                        StartCoroutine(PerformAct());
                        uiController.ToggleActionState(false);
                        break;
                }

                // next character's turn
                characterTurnIndex++; 
                Debug.Log(characterTurnIndex);
            }
            NextTurn();
            characterTurnIndex = 0;
            Debug.Log("turn: " + actTurn);
        }
        else
        {
            Debug.Log("Battle over!");
        }
    }

    private IEnumerator PerformAct()
    {
        yield return new WaitForSeconds(0.75f);

        if (GetCurrentCharacter().health > 0)
        {
            GetCurrentCharacter().GetComponent<Enemy>().Act();
        }

        uiController.UpdateCharacterUI();

        yield return new WaitForSeconds(1f);

        NextAct();
    }

    public void SelectCharacter(Character character)
    {
        if (playerIsAttacking)
        {
            DoAttack(GetCurrentCharacter(), character);
        }
        else if (playerSelectedSpell != null)
        {
            if (GetCurrentCharacter().CastSpell(playerSelectedSpell, character))
            {
                uiController.UpdateCharacterUI();
                NextAct();
            }
            else 
            {
                Debug.LogWarning("Not enough mana to cast that spell!");
            }
        }
    }

    public void DoAttack(Character attacker, Character target)
    {
        Debug.Log("Do attack.");
        target.Hurt(attacker.attackPower);
        // NextAct();
        if (actTurn == 0)
        {
            NextAct();
        }
    }


    public Character GetCurrentCharacter()
    {
        return characters[actTurn][characterTurnIndex];
    }
}
