using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;

    [SerializeField] private Text textStair;

    // Start is called before the first frame update
    private void OnEnable()
    {
        inventory.OnStairChange += UpdateUI;
    }

    private void OnDisable()
    {
        inventory.OnStairChange -= UpdateUI;
    }

    private void UpdateUI(int stairCount)
    {
        textStair.text = stairCount.ToString();
    }
}
