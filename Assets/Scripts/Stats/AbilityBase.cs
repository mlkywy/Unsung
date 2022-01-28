using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability/New Ability")]
public class AbilityBase : ScriptableObject
{
    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] CharacterType type;
    [SerializeField] int power;
    [SerializeField] int accuracy;
    [SerializeField] int pp;
}
