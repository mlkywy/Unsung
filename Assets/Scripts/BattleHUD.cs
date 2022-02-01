using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    public Image spriteUI;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;

    public TextMeshProUGUI hpValue;

    public Slider hpSlider;

    public void SetHUD(Unit unit)
    {
        spriteUI.GetComponent<Image>().sprite = unit.unitSprite;

        nameText.text = unit.unitName;
        levelText.text = "LVL\n" + unit.unitLevel;

        hpValue.text = unit.currentHP.ToString();

        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
        hpValue.text = hp.ToString();
    }
}
