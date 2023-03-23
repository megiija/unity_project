using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hpbar : MonoBehaviour
{
    public Slider healthBar;
    public Monsters Monster;

    private bool lerpingHealth = false;

    private void Start()
    {
        healthBar = GetComponent<Slider>();
    }

    public void SetHP(float HPnormal)
    {
        healthBar.value = HPnormal;
    }
}
