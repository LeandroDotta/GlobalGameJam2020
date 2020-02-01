using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    [SerializeField] Collider2D groundCheck;

    private float axisHorizontal;

    private bool inputJump;
    private bool grounded = true;

    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Jump();
    }

    private void Update() 
    {
        axisHorizontal = Input.GetAxisRaw("Horizontal");
        inputJump = Input.GetButtonDown("Jump");

        Move();
        Flip();
    }
    
    private void Move()
    {
        Vector2 movement = new Vector2(axisHorizontal, 0) * speed * Time.deltaTime;
        transform.Translate(movement);
    }

    private void Flip()
    {
        if (axisHorizontal != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = axisHorizontal;
            transform.localScale = scale;
        }
    }

    private void Jump()
    {
        if (inputJump && grounded)
        {
            inputJump = false;
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
