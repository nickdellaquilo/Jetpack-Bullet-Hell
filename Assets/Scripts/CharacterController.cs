using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float verticalSpeed = 1.0f;
    public float horizontalSpeed = 1.0f;
    public int health = 10;
    private bool invincible = false;
    
    public Camera mainCamera;

    private Rigidbody2D rb;
    private float horizontalInput = 0.0f;
    private float verticalInput = 0.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if health is depleted
        if (health <= 0)
        {
            // Destroy player entity
            // Display high score UI
            // Option to play again
        }

        // Get player input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }
    
    void FixedUpdate()
    {
        // Update movement based on player inputs
        Vector2 movementDirection = new Vector2(horizontalInput * horizontalSpeed, verticalInput * verticalSpeed).normalized;
        if (movementDirection != Vector2.zero)
        {
            rb.AddForce(movementDirection, ForceMode2D.Impulse);
        }
    }
}
