using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character 
{
    public CharacterBase Base { get; set; }
    public int Level { get; set; }
    public int HP { get; set; }
    public List<Ability> Abilities { get; set; }

    public Character(CharacterBase cBase, int cLevel)
    {
        Base = cBase;
        Level = cLevel;
        HP = MaxHp;

        // generates moves of character based on level
        Abilities = new List<Ability>();
        foreach (var ability in Base.LearnableAbilities)
        {
            if (ability.Level <= Level)
            {
                Abilities.Add(new Ability(ability.Base));
            }

            if (Abilities.Count >= 4)
            {
                break;
            }
        }
    }

    public int MaxHp
    {
        get { return Mathf.FloorToInt((Base.MaxHp * Level) / 100f) + 10; }
    }

    public int Attack
    {
        get { return Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5; }
    }

    public int Defense
    {
        get { return Mathf.FloorToInt((Base.Defense * Level) / 100f) + 5; }
    }

    public int SpAttack
    {
        get { return Mathf.FloorToInt((Base.SpAttack * Level) / 100f) + 5; }
    }

    public int SpDefense
    {
        get { return Mathf.FloorToInt((Base.SpDefense * Level) / 100f) + 5; }
    }

    public int Speed
    {
        get { return Mathf.FloorToInt((Base.Speed * Level) / 100f) + 5; }
    }
}
