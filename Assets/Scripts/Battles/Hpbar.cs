using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hpbar : MonoBehaviour
{
    public Slider healthBar;
    public Monsters Monster;

    private void Start()
    {
        healthBar = GetComponent<Slider>();
    }

    public void SetHP(float HPnormal)
    {
        healthBar.value = HPnormal;
    }
}
