using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InPlay : MonoBehaviour
{
    [SerializeField] MonsterBase _base;
    [SerializeField] int level;
    [SerializeField] bool isPlayer;

    public Monsters Monster { get; set; }

    public void SetUp()
    {
        Monster = new Monsters(_base, level);
        if (isPlayer)
            GetComponent<Image>().sprite = Monster.Base.RightSprite;
        else
            GetComponent<Image>().sprite = Monster.Base.LeftSprite;
    }
}