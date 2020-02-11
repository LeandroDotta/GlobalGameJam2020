using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;

    private int currentHealth;

    public UnityAction<int> OnHealthChange;
    public UnityAction<int, int> OnHealthIncrease;
    public UnityAction<int, int> OnHealthDecrease;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public int TakeDamage(int amount)
    {
        SetHealth(currentHealth - amount);

        OnHealthDecrease?.Invoke(currentHealth, amount);

        return currentHealth;
    }

    public int Restore(int amount)
    {
        SetHealth(currentHealth + amount);

        OnHealthIncrease?.Invoke(currentHealth, amount);

        return currentHealth;
    }

    private void SetHealth(int health)
    {
        if (health > maxHealth)
            health = maxHealth;

        if (health < 0)
            health = 0;

        if (health != currentHealth)
        {
            OnHealthChange?.Invoke(health);
            currentHealth = health;
        }
    }
}
