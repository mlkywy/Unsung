using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public void Act()
    {
        int randomAct = Random.Range(0, 2);

        switch(randomAct)
        {
            case 0:
                Defend();
                break;
            case 1:
                // spell
                Spell spellToCast = GetRandomSpell();

                if (spellToCast.spellType == Spell.SpellType.Heal)
                {
                    // get friendly weak target
                }

                if (!CastSpell(spellToCast, null))
                {
                    // attack
                }

                break;
            case 2: 
                // attack
                break;
        }
    }

    Spell GetRandomSpell()
    {
        return spells[Random.Range(0, spells.Count - 1)];
    }



    public override void Die()
    {
        base.Die();
        // do more
    }
}
