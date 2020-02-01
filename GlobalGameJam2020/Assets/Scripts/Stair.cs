using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class Stair : MonoBehaviour
{
    [SerializeField] Collider2D platformCollider;

    private float horizontalRotation = 0;
    private float verticalRotation = 90;

    private Orientation orientation;

    private int layerDefault;
    private int layerIgnorePlayer;
    
    private void Start()
    {
        layerDefault = LayerMask.NameToLayer("Default");
        layerIgnorePlayer = LayerMask.NameToLayer("IgnorePlayer");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (orientation == Orientation.Horizontal)
                SetOrientation(Orientation.Vertical);
            else
                SetOrientation(Orientation.Horizontal);
        }
    }

    public void SetOrientation(Orientation orientation)
    {
        this.orientation = orientation;

        if (orientation == Orientation.Horizontal)
        {
            transform.localRotation = Quaternion.Euler(0, 0, horizontalRotation);
            platformCollider.gameObject.layer = layerDefault;
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, verticalRotation);
            platformCollider.gameObject.layer = layerIgnorePlayer;
        }

    }

    public enum Orientation
    {
        Vertical,
        Horizontal
    }
}
