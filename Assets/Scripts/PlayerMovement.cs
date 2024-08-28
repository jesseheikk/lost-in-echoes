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
        rb.freezeRotation = true;   // Prevent player from falling from planets gravity
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
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the direction the player should move in, relative to its orientation
        moveDirection = transform.forward * vertical + transform.right * horizontal;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    void MovePlayer()
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    void ApplyGravity()
    {
        Vector3 gravityDirection = (transform.position - planet.position).normalized;

        // Apply the gravity force towards the planet's center
        rb.AddForce(-gravityDirection * gravity * rb.mass);

        // Rotate the player to align with the planet's surface
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, gravityDirection) * transform.rotation;
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, 10 * Time.fixedDeltaTime));

        isGrounded = Physics.Raycast(transform.position, -transform.up, groundCheckDistance, groundLayer);
    }

    void Jump()
    {
        Vector3 jumpDirection = transform.up;
        rb.AddForce(jumpDirection * jumpForce, ForceMode.VelocityChange);
    }
}
