using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    public string characterName { get; set; }
    public Sprite characterSprite { get; set; }
    public int health { get; set; }
    public int maxHealth { get; set; }
    public int attackPower { get; set; }
    public int defensePower { get; set; }
    public int manaPoints { get; set; }
    public int maxManaPoints { get; set; }
    public List<Spell> spells { get; set; }
    public bool isDead { get; set; }
}