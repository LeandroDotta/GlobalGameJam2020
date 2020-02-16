using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedScript : MonoBehaviour
{
    private SpriteRenderer ledSprite;
    public Sprite[] ledSprites = new Sprite[2];
    public bool isOn = false;

    void Start()
    {
        ledSprite = GetComponent<SpriteRenderer>();
        setOnOff(isOn);
    }

    void setOnOff(bool state)
    {
        if(state) {
            ledSprite.sprite = ledSprites[1];
        } else {
            ledSprite.sprite = ledSprites[0];
        }
    }

    public void Switch()
    {
        isOn = !isOn;
        setOnOff(isOn);
    }
}
