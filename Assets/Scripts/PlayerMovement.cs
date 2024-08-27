using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform planet;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float gravity = 10f;
    [SerializeField] float jumpForce = 5f; 
    [SerializeField] LayerMask groundLayer;
    
    float groundCheckDistance = 0.2f;
    bool isGrounded;
    Rigidbody rb;
    Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        CheckInput();
    }

    void FixedUpdate()
    {
        ApplyGravity();
        MovePlayer();
    }

    void CheckInput()
    {
        // Get input for movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the direction the player should move in, relative to their orientation
        moveDirection = transform.forward * vertical + transform.right * horizontal;

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded) // Check jump input in Update
        {
            Jump();
        }
    }

    void MovePlayer()
    {
        // Move the player
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    void ApplyGravity()
    {
        // Calculate gravity direction (pointing towards the planet's center)
        Vector3 gravityDirection = (transform.position - planet.position).normalized;

        // Apply the gravity force towards the planet's center
        rb.AddForce(-gravityDirection * gravity * rb.mass);

        // Rotate the player to align with the planet's surface
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, gravityDirection) * transform.rotation;
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, 10 * Time.fixedDeltaTime));

        // Check if the player is grounded
        isGrounded = Physics.Raycast(transform.position, -transform.up, groundCheckDistance, groundLayer);
    }

    void Jump()
    {
        // Apply jump force in the direction opposite to gravity
        Vector3 jumpDirection = transform.up;
        rb.AddForce(jumpDirection * jumpForce, ForceMode.VelocityChange);
    }
}
