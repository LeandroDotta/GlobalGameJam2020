using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : MonoBehaviour
{
    [SerializeField] private Transform baseTransform;
    [SerializeField] private Transform bodyTransform;
    [SerializeField] private Sprite spriteOff;
    [SerializeField] private Sprite spriteOn;


    private bool isFixed;
    private Collider2D triggerAreaCurrentColl;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = spriteOff;
    }

    private void PlugIt()
    {
        bodyTransform.position = baseTransform.position;
        //spriteRenderer.sprite = spriteOn;
        //SoundController.Instance.PlaySFX(10);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Plug OnCollisionEnter Detected");
        if (triggerAreaCurrentColl == null)
            return;

        if (col.collider.name == triggerAreaCurrentColl.name)
        {
            PlugIt();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Plug OnTriggerStay Detected");
        if (triggerAreaCurrentColl == null || triggerAreaCurrentColl.name != other.name)
        {
            triggerAreaCurrentColl = other;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Plug OnTriggerExit Detected");
        triggerAreaCurrentColl = null;
    }
}
