using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Character/New Ability")]
public class AbilityBase : ScriptableObject
{
    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] CharacterType type;
    [SerializeField] int power;
    [SerializeField] int accuracy;
    [SerializeField] int pp;

    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description; }
    }

    public CharacterType Type
    {
        get { return type; }
    }

     public int Power
    {
        get { return power; }
    }

    public int Accuracy
    {
        get { return accuracy; }
    }

    public int PP
    {
        get { return pp; }
    }
}
