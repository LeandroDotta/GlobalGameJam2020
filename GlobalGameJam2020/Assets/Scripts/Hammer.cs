using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public Vector2 direction;
    public float speed;
    public float returnSpeed;

    public Transform playerTransform;

    private State state;
    private Rigidbody2D rb2d;
    private Collider2D coll;

    private int layerDefault;
    private int layerIgnorePlayer;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();

        layerDefault = LayerMask.NameToLayer("Default");
        layerIgnorePlayer = LayerMask.NameToLayer("IgnorePlayer");
    }

    private void OnEnable()
    {
        SetState(State.Going);
    }

    private void FixedUpdate()
    {
        if (state == State.Going)
        {
            Move();
        }
    }

    private void Update()
    {
        if (state == State.Returning)
        {
            Follow();
        }
    }

    private void Move()
    {
        rb2d.velocity = direction * speed;
    }

    private void Follow()
    {
        Vector2 dir = (playerTransform.position - transform.position).normalized;
        Debug.Log("Hammer returning to direction: " + dir);
        transform.Translate(dir * returnSpeed * Time.deltaTime);
    }

    private void SetState(State newState)
    {
        state = newState;

        if (state == State.Going)
        {
            gameObject.layer = layerIgnorePlayer;
            rb2d.isKinematic = false;
            coll.isTrigger = false;
        } 
        else
        {
            gameObject.layer = layerDefault;
            rb2d.isKinematic = true;
            rb2d.velocity = Vector2.zero;
            coll.isTrigger = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Hammer Collided");
        SetState(State.Returning);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hammer Collided Trigger");
        if (other.CompareTag("Player"))
            gameObject.SetActive(false);


    }

    public enum State
    {
        Going,
        Returning
    }

}
