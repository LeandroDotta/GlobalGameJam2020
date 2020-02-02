using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class Stair : MonoBehaviour
{
    [SerializeField] Collider2D platformCollider;
    [SerializeField] Collider2D baseCollider;
    [SerializeField] Collider2D stairTrigger;

    private float horizontalRotation = 0;
    private float verticalRotation = 90;

    private Orientation orientation;

    private int layerDefault;
    private int layerIgnorePlayer;
    
    private void Start()
    {
        layerDefault = LayerMask.NameToLayer("Default");
        layerIgnorePlayer = LayerMask.NameToLayer("IgnorePlayer");

        SetOrientation(Orientation.Horizontal);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    if (orientation == Orientation.Horizontal)
        //        SetOrientation(Orientation.Vertical);
        //    else
        //        SetOrientation(Orientation.Horizontal);
        //}
    }

    public void SetOrientation(Orientation orientation)
    {
        this.orientation = orientation;

        if (orientation == Orientation.Horizontal)
        {
            transform.localRotation = Quaternion.Euler(0, 0, horizontalRotation);
            platformCollider.gameObject.layer = layerDefault;
            platformCollider.enabled = true;
            stairTrigger.enabled = false;
            baseCollider.gameObject.SetActive(false);

        }
        else
        {
            if (transform.localScale.x > 0)
                transform.localRotation = Quaternion.Euler(0, 0, verticalRotation);
            else
                transform.localRotation = Quaternion.Euler(0, 0, -verticalRotation);

            platformCollider.gameObject.layer = layerIgnorePlayer;
            platformCollider.enabled = false;
            stairTrigger.enabled = true;
            baseCollider.gameObject.SetActive(true);

        }
    }

    public void ToggleOrientation()
    {
        if (orientation == Orientation.Horizontal)
            SetOrientation(Orientation.Vertical);
        else
            SetOrientation(Orientation.Horizontal);
    }

    public enum Orientation
    {
        Vertical,
        Horizontal
    }
}
