using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public Sprite characterSprite;
    public int health;
    public int maxHealth;
    public int attackPower;
    public int defensePower;
    public int manaPoints;
    public int maxManaPoints;
    public List<Spell> spells;

    public void Hurt(int amount)
    {
        // formula for damage amount
        // int damageAmount = Random.Range(0,1) * (amount - defensePower); <-- doesn't work
        int damageAmount = amount;

        if (damageAmount > 0)
        {
            health = Mathf.Max(health - damageAmount, 0);
        }

        if (health == 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        // formula for heal amount
        // int healAmount = Random.Range(0,1) * (int)(amount + (maxHealth * 0.33)); <-- doesn't work
        int healAmount = amount;
        // health should never go above max health
        health = Mathf.Min(health + healAmount, maxHealth);
    }

    public void Defend()
    {
        defensePower += (int)(defensePower * 0.33);
        Debug.Log("Defense increased!");
    }

    public bool CastSpell(Spell spell, Character targetCharacter)
    {
        bool successful = manaPoints >= spell.manaCost;

        if (successful)
        {
            Spell spellToCast = Instantiate<Spell>(spell, transform.position, Quaternion.identity);
            manaPoints -= spell.manaCost;
            spellToCast.Cast(targetCharacter);
        }

        return successful;
    }

    // virtual means it can be overridden
    public virtual void Die()
    {
        Destroy(this.gameObject);
        Debug.LogFormat("{0} has died!", characterName);
    }
}
