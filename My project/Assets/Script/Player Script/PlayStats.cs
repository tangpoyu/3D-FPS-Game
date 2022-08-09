using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayStats : MonoBehaviour
{
    [SerializeField]
    private Image healthStats, staminaStats;

    internal void updateHealth(float health)
    {
        health /= 100;
        healthStats.fillAmount = health;
    }

    internal void updateStamina(float stamina)
    {
        stamina /= 100;
        staminaStats.fillAmount = stamina;
    }
}
