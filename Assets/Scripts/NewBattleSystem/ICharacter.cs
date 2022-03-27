using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
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
}