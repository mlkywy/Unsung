using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character/New Character")]
public class CharacterBase : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private Sprite sprite;
    [SerializeField] private CharacterType type;

    [TextArea] 
    [SerializeField] private string description;

    // base stats
    [SerializeField] private int maxHp;
    [SerializeField] private int attack;
    [SerializeField] private int defense;
    [SerializeField] private int spAttack;
    [SerializeField] private int spDefense;
    [SerializeField] private int speed;

    [SerializeField] List<LearnableAbility> learnableAbilities;

    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description; }
    }

    public Sprite Sprite
    {
        get { return sprite; }
    }

    public int MaxHp
    {
        get { return maxHp; }
    }

    public int Attack
    {
        get { return attack; }
    }

    public int Defense
    {
        get { return defense; }
    }

    public int SpAttack
    {
        get { return spAttack; }
    }

    public int SpDefense
    {
        get { return spDefense; }
    }

    public int Speed
    {
        get { return speed; }
    }

    public List<LearnableAbility> LearnableAbilities
    {
        get { return learnableAbilities; }
    }
}

[System.Serializable]
public class LearnableAbility
{
    [SerializeField] AbilityBase abilityBase;
    [SerializeField] int level;

    public AbilityBase Base
    {
        get { return abilityBase; }
    }

    public int Level
    {
        get { return level; }
    }
}

public enum CharacterType
{
    None,
    Special,
    Boss
}

