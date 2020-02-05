using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coil : MonoBehaviour
{
    private Vector2 distance;
    private Vector2 direction;
    public float force = 150;
    private float radius;
    private float proportion;

    public void Attract(Transform body) {
        radius = transform.GetChild(0).GetComponent<CircleCollider2D>().radius;

        distance = (body.position - transform.position);
        direction = distance.normalized;
        proportion = (radius - distance.magnitude) / radius;

        body.parent.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * -force);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Coil OnCollisionEnter Detected");
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Coil OnTriggerStay Detected");
        Attract(other.transform);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Coil OnTriggerExit Detected");
    }
}
