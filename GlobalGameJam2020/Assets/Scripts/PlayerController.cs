using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private const string TAG = "PlayerController";
    [SerializeField] private float speed;
    [SerializeField] private float climbSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float invulnerableTime;


    [SerializeField] Collider2D groundCheck;

    private float axisHorizontal;
    private float axisVertical;
    private bool inputJump;

    private bool grounded = true;
    private bool climbing;
    private bool canClimb;
    private bool canRotateStair;
    private bool invulnerable;

    private Rigidbody2D rb2d;
    private PlayerHealth health;
    private Stair focusedStair;

    private int layerWalkable;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        health = GetComponent<PlayerHealth>();
        layerWalkable = LayerMask.NameToLayer("Walkable");
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

        if (canRotateStair)
        {
            Debug.Log("CAN ROTATE!!");
            if (Input.GetButtonDown("Rotate"))
            {
                Debug.Log("ROTATE PRESSED");
                focusedStair.ToggleOrientation();
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
        position.x = focusedStair.transform.position.x;
        position.y = position.y + 0.5f;
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

            if (!canClimb)
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

    private void TakeHit(GameObject hazardObj)
    {
        Hazard hazard = hazardObj.GetComponent<Hazard>();
        Debug.Log("Hazard: " + hazard);
        if (hazard != null)
        {
            if (hazard.oneHitKill)
            {
                Die();
            }
            else
            {
                if (!invulnerable)
                {
                    Debug.Log("Applying Damage = " + hazard.damage);
                    int life = health.TakeDamage(hazard.damage);

                    Debug.Log("Current Damage: " + life);

                    if (life == 0)
                        Die();
                    else
                        StartCoroutine("InvulnerableCoroutine");
                }
            }
        }
    }

    private void Die()
    {
        // Restart Scene (for testing)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // Go back to checkpoint
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

    private void OnCollisionStay2D(Collision2D col)
    {
        Collider2D collider = col.collider;

        if (collider.CompareTag("Hazard"))
        {
            TakeHit(collider.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Stair stair = other.GetComponentInParent<Stair>();

        if (stair != null)
        {
            focusedStair = other.GetComponentInParent<Stair>();

            if (other.CompareTag("Stair"))
                canClimb = true;

            if (other.CompareTag("StairBase"))
                canRotateStair = true;
        }

        if (layerWalkable == other.gameObject.layer /*other.CompareTag("Ground")*/)
        {
            grounded = true;
        }

        if (other.CompareTag("Pickup"))
        {
            Pickup pickup = other.GetComponent<Pickup>();
            if (pickup.restore)
                health.Restore(pickup.health);

            Destroy(pickup.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (layerWalkable == other.gameObject.layer/*other.CompareTag("Ground")*/)
        {
            grounded = false;
        }

        if (other.CompareTag("Stair"))
        {
            canClimb = false;
        }

        if (other.CompareTag("StairBase"))
        {
            canRotateStair = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (layerWalkable == other.gameObject.layer /*other.CompareTag("Ground")*/)
            grounded = true;

        if (other.CompareTag("Stair"))
            canClimb = true;

        if (other.CompareTag("StairBase"))
            canRotateStair = true;

        if (other.CompareTag("Hazard"))
        {
            Debug.Log("Hazard Trigger");
            TakeHit(other.gameObject);
        }
    }

    private IEnumerator InvulnerableCoroutine()
    {
        invulnerable = true;
        Debug.Log("Player is Invulnerable");
        yield return new WaitForSeconds(invulnerableTime);
        invulnerable = false;
        Debug.Log("Player is vulnerable again");
    }
}