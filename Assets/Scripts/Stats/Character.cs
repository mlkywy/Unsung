using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character 
{
    CharacterBase _base;
    int level;

    public int HP { get; set; }

    public List<Ability> Abilities { get; set; }

    public Character(CharacterBase cBase, int cLevel)
    {
        _base = cBase;
        level = cLevel;
        HP = _base.MaxHp;

        // generates moves of character based on level
        Abilities = new List<Ability>();
        foreach (var ability in _base.LearnableAbilities)
        {
            if (ability.Level <= level)
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
        get { return Mathf.FloorToInt((_base.MaxHp * level) / 100f) + 10; }
    }

    public int Attack
    {
        get { return Mathf.FloorToInt((_base.Attack * level) / 100f) + 5; }
    }

    public int Defense
    {
        get { return Mathf.FloorToInt((_base.Defense * level) / 100f) + 5; }
    }

    public int SpAttack
    {
        get { return Mathf.FloorToInt((_base.SpAttack * level) / 100f) + 5; }
    }

    public int SpDefense
    {
        get { return Mathf.FloorToInt((_base.SpDefense * level) / 100f) + 5; }
    }

    public int Speed
    {
        get { return Mathf.FloorToInt((_base.Speed * level) / 100f) + 5; }
    }
}
