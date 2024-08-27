using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public Transform planet; // Reference to the planet's transform
    public float moveSpeed = 10f; // Movement speed of the player
    public float gravity = 10f; // Gravity force towards the planet's center
    public float jumpForce = 5f; // Jump force

    private CharacterController controller;
    private Vector3 moveDirection;
    private float verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Get the direction towards the planet's center
        Vector3 gravityDirection = (transform.position - planet.position).normalized;

        // Apply gravity towards the planet's center
        verticalVelocity -= gravity * Time.deltaTime;

        // If grounded, reset the vertical velocity and allow jumping
        if (controller.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime; // Keep the player grounded

            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpForce; // Apply jump force
            }
        }

        // Calculate movement input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the direction the player should move based on the input
        Vector3 localRight = transform.right;
        Vector3 localForward = Vector3.Cross(gravityDirection, localRight);

        // Create the movement direction relative to the player's current orientation
        moveDirection = (localRight * horizontal + localForward * vertical).normalized;
        moveDirection *= moveSpeed;

        // Apply gravity to the movement direction
        moveDirection += gravityDirection * verticalVelocity;
        Debug.Log(moveDirection);

        // Move the player
        controller.Move(moveDirection * Time.deltaTime);

        // Rotate the player to align with the surface of the planet
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, -gravityDirection) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10 * Time.deltaTime);
    }
}