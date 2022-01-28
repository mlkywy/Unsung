using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability 
{
    public AbilityBase Base { get; set; }
    public int PP { get; set; }

    public Ability(AbilityBase cBase)
    {
        Base = cBase;
        PP = cBase.PP;
    }
}
