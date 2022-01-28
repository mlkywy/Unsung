using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] private CharacterBase _base;
    [SerializeField] private int level;
    // [SerializeField] private bool isPlayerUnit;

    public Character Character { get; set; }

    public void Setup()
    {
        Character = new Character(_base, level);

        GetComponent<Image>().sprite = Character.Base.Sprite;
        GetComponent<Image>().rectTransform.sizeDelta = new Vector2(Character.Base.Sprite.texture.width, Character.Base.Sprite.texture.height);
    }

    
}
