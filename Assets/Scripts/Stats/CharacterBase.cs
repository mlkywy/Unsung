using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character/New Character")]
public class CharacterBase : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] Sprite sprite;
    [SerializeField] CharacterType type;

    [TextArea] 
    [SerializeField] string description;

    // base stats
    [SerializeField] int maxHp;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int spAttack;
    [SerializeField] int spDefense;
    [SerializeField] int speed;

    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description; }
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
}

public enum CharacterType
{
    None,
    Special,
    Boss
}
