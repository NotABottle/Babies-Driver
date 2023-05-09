using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int currentHealth;
    private int maxHealth = 3;

    private void OnEnable() => EventManager.StartListening("DealDamage",TakeDamage);
    private void OnDisable() => EventManager.StopListening("DealDamage",TakeDamage);

    private void Awake() => currentHealth = maxHealth;

    private void TakeDamage(Dictionary<string, object> obj)
    {
        currentHealth --;

        if(currentHealth == 0) EventManager.TriggerEvent("LoseCondition",null);
    }
}
