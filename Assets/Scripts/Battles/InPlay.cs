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

    Image image;
    Vector3 originalPos;

    private void Awake()
    {
        image = GetComponent<Image>();
        originalPos = image.transform.localPosition;
    }

    public void SetUp()
    {
        Monster = new Monsters(_base, level);
        if (isPlayer)
            GetComponent<Image>().sprite = Monster.Base.RightSprite;
        else
            GetComponent<Image>().sprite = Monster.Base.LeftSprite;
    }

    public void PlayEnterAnimation()
    {
        if (isPlayer)
        {
            //image.transform.localPosition = new Vector3;
        }
    }

    public void PlayAttackAnimation()
    {

    }


}