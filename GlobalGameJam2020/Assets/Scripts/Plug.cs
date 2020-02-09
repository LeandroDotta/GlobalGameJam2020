using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : MonoBehaviour
{
    [SerializeField] private Transform bodyTransform;
    [SerializeField] private Transform pluggedPositionTransform;
    [SerializeField] private Sprite spritePlugged;

    private bool isFixed;
    private Collider2D triggerAreaCurrentColl;
    private Animator anim;
    private SpriteRenderer spriteUnplugged;

    private Goal goal;

    private void Start()
    {
        anim = bodyTransform.GetChild(0).GetComponent<Animator>();
        spriteUnplugged = bodyTransform.GetChild(0).GetComponent<SpriteRenderer>();
        goal = GetComponent<Goal>();
    }

    private void PlugIt()
    {
        bodyTransform.position = pluggedPositionTransform.position;
        spriteUnplugged.sprite = spritePlugged;
        anim.Rebind();
        anim.enabled = false;

        SoundController.Instance.PlaySFX(13);

        if (goal != null)
            goal.Complete();
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
