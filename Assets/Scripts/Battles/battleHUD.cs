using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class battleHUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Hpbar hpBar;

    public void setData(Monsters monster)
    {
        nameText.text = monster.Base.Name;
        levelText.text = "Lvl. " + monster.Level;
        hpBar.SetHP((float) monster.HP / monster.MaxHP);
    }

    public void updateHP(Monsters monster)
    {
        hpBar.SetHP((float)monster.HP / monster.MaxHP);
    }
}
