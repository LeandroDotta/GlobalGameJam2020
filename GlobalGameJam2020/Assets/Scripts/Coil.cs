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
    private Hazard hazard;
    private SpriteRenderer magneticField;
    public bool isOn = true;

    private AudioSource electromagneticAudio;

    void Start()
    {
        electromagneticAudio = GetComponent<AudioSource>();
        magneticField = transform.GetChild(1).GetComponent<SpriteRenderer>();
        hazard = GetComponent<Hazard>();
        setOnOff(isOn);
    }

    private void setOnOff(bool state)
    {
        hazard.on = state;
        magneticField.enabled = state;
    }

    public void Attract(Transform body) {
        if(isOn) {
            radius = transform.GetChild(0).GetComponent<CircleCollider2D>().radius;

            distance = (body.position - transform.position);
            direction = distance.normalized;
            proportion = (radius - distance.magnitude) / radius;

            body.parent.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * -force);

            electromagneticAudio.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Coil OnCollisionEnter Detected");
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Coil OnTriggerStay Detected");
        if(other.tag == "Player") {
            Attract(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Coil OnTriggerExit Detected");
        electromagneticAudio.Stop();
    }

    public void Switch()
    {
        isOn = !isOn;
        setOnOff(isOn);
    }
}
