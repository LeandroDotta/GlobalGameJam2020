using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float climbSpeed;
    [SerializeField] private float jumpForce;


    [SerializeField] Collider2D groundCheck;

    private float axisHorizontal;
    private float axisVertical;
    private bool inputJump;

    private bool grounded = true;
    private bool climbing;
    private bool canClimb;

    private Rigidbody2D rb2d;
    private Collider2D stair;

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
        axisVertical = Input.GetAxisRaw("Vertical");

        if (!inputJump && (grounded || climbing))
            inputJump = Input.GetButtonDown("Jump");

        if (canClimb)
        {
            if (!climbing && axisVertical > 0)
            {
                StartClimb();
            }
        }

        Move();
        Flip();
    }

    private void StartClimb()
    {
        rb2d.isKinematic = true;
        rb2d.velocity = Vector2.zero;
        Vector3 position = transform.position;
        position.x = stair.transform.position.x;
        transform.position = position;

        climbing = true;
    }

    private void StopClimb()
    {
        rb2d.isKinematic = false;
        climbing = false;
    }

    private void Move()
    {
        if (climbing)
        {
            Vector2 movement = new Vector2(0, axisVertical) * climbSpeed * Time.deltaTime;
            transform.Translate(movement);

            if (grounded || !canClimb)
            {
                StopClimb();
            }
        }
        else
        {
            Vector2 movement = new Vector2(axisHorizontal, 0) * speed * Time.deltaTime;
            transform.Translate(movement);
        }

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
        if (inputJump)
        {
            inputJump = false;
            StopClimb();
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            grounded = true;
        }

        if (other.CompareTag("Stair"))
        {
            canClimb = true;
            stair = other;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            grounded = false;
        }

        if (other.CompareTag("Stair"))
        {
            canClimb = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        grounded = other.CompareTag("Ground");
        canClimb = other.CompareTag("Stair");
    }
}