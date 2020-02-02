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
    [SerializeField] private float groundCheckSize = 0.1f;
    [SerializeField] private LayerMask groundCheckLayer;

    [Header("Spawn Points")]
    [SerializeField] private Transform hammerSpawnPoint;
    [SerializeField] private Transform stairSpawnPoint;

    [Header("Components")]
    [SerializeField] private GameObject hammerPrefab;
    [SerializeField] private Collider2D coll;

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
    private PlayerInventory inventory;
    private Stair focusedStair;
    private Hammer hammer;
    private AudioSource audioSource;

    private int layerWalkable;



    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        health = GetComponent<PlayerHealth>();
        inventory = GetComponent<PlayerInventory>();
        audioSource = GetComponent<AudioSource>();

        layerWalkable = LayerMask.NameToLayer("Walkable");

        GameObject hammerObj = Instantiate(hammerPrefab);
        hammer = hammerObj.GetComponent<Hammer>();
        hammer.playerTransform = this.transform;
        hammer.gameObject.SetActive(false);

    }

    private void FixedUpdate()
    {
        Jump();
    }

    private void Update()
    {
        axisHorizontal = Input.GetAxisRaw("Horizontal");
        axisVertical = Input.GetAxisRaw("Vertical");

        Bounds collBounds = coll.bounds;
        grounded = Physics2D.OverlapArea(new Vector2(collBounds.min.x, collBounds.min.y), new Vector2(collBounds.max.x, collBounds.min.y - groundCheckSize), groundCheckLayer);

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
            if (Input.GetButtonDown("Rotate"))
            {
                Debug.Log("ROTATE PRESSED");
                focusedStair.ToggleOrientation();
            } 
            else if (Input.GetButtonDown("Grab"))
            {
                inventory.StoreStair(focusedStair.gameObject);
            }
        }
        else
        {
            if (Input.GetButtonDown("Grab"))
            {
                int direction = transform.localScale.x > 0 ? 1 : -1;
                inventory.SpawnStair(stairSpawnPoint.position, direction);
            }
        }

        if (Input.GetButtonDown("Shoot"))
        {
            Shoot();
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
        if ((axisVertical !=0 | axisHorizontal !=0))
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            if(audioSource.isPlaying)
                audioSource.Stop();
        }
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

            SoundController.Instance.PlaySFX(0, 1, 2);
        }
    }

    private void Shoot()
    {
        if (hammer.gameObject.activeSelf)
            return;

        hammer.transform.position = hammerSpawnPoint.position;
        hammer.direction = new Vector2((transform.localScale.x > 0 ? 1 : -1), 0);
        hammer.gameObject.SetActive(true);

        SoundController.Instance.PlaySFX(3, 4, 5);
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

        //if (layerWalkable == other.gameObject.layer /*other.CompareTag("Ground")*/)
        //{
        //    grounded = true;
        //}

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
        //if (layerWalkable == other.gameObject.layer/*other.CompareTag("Ground")*/)
        //{
        //    grounded = false;
        //}

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
        //if (layerWalkable == other.gameObject.layer /*other.CompareTag("Ground")*/)
            //grounded = true;

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        Bounds bounds = coll.bounds;
        Vector3 center = new Vector3(bounds.center.x, bounds.min.y - groundCheckSize / 2);
        Vector3 size = new Vector3(bounds.size.x, groundCheckSize);
        Gizmos.DrawWireCube(center, size);
    }
}