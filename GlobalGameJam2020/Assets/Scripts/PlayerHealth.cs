using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public int TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth < 0)
            currentHealth = 0;

        return currentHealth;
    }
}
