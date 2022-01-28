using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI hpValue;
    [SerializeField] private HPBar hpBar;
    [SerializeField] private bool isPlayerUnit;

    public void SetData(Character character)
    {
        nameText.text = character.Base.Name;

        if (isPlayerUnit) 
        {
            hpValue.text = " " + character.HP;
            levelText.text = "LVL\n" + character.Level;
            hpBar.SetHP((float) character.HP / character.MaxHp);
        }
    }
}
