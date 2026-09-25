using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 8f;

    public KeyCode jumpKey;
    public KeyCode leftKey;
    public KeyCode rightKey;

    [Tooltip("Raahaa tähän lattia-objekti, jolla on Collider2D")]
    public Collider2D groundCollider;

    private Rigidbody2D rb;
    private bool jumpRequested;
    private float groundY;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Lasketaan lattian PINNAN Y-koordinaatti automaattisesti
        // colliderin bounds-tiedosta (yläreuna = bounds.max.y)
        groundY = groundCollider.bounds.max.y;
    }

    void Update()
    {
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            jumpRequested = true;
        }
    }

    void FixedUpdate()
    {
        float moveX = 0f;
        if (Input.GetKey(leftKey)) moveX -= 1f;
        if (Input.GetKey(rightKey)) moveX += 1f;

        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);

        if (jumpRequested)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpRequested = false;
        }

        // Tarkistetaan onko hahmo lattian tasolla (pieni toleranssi 0.05f)
        isGrounded = transform.position.y <= groundY + 0.05f;

        // Estetään hahmoa uppoamasta lattian läpi
        if (isGrounded && rb.linearVelocity.y <= 0f)
        {
            transform.position = new Vector3(transform.position.x, groundY, transform.position.z);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        }
    }
}