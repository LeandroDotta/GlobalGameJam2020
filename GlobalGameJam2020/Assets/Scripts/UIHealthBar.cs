using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float emptyTransparancy = 0.3f;

    [SerializeField] private PlayerHealth health;
    [SerializeField] private Image[] healthItems;

    private void OnEnable()
    {
        health.OnHealthChange += UpdateUI;
    }

    private void OnDisable()
    {
        health.OnHealthChange -= UpdateUI;  
    }

    private void UpdateUI(int health)
    {
        for (int i = 0; i < healthItems.Length; i++)
        {
            Color color = healthItems[i].color;
            if (i < health)
            {
                color.a = 1;
            }
            else
            {
                color.a = emptyTransparancy;
            }

            healthItems[i].color = color;
        }
    }
}