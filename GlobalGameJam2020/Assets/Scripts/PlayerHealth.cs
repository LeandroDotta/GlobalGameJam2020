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
        return SetHealth(currentHealth - amount);
    }

    public int Restore(int amount)
    {
        return SetHealth(currentHealth + amount);
    }

    private int SetHealth(int health)
    {
        currentHealth = health;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        if (currentHealth < 0)
            currentHealth = 0;

        return health;
    }
}
