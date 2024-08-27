using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Sensitivity of the mouse movement
    public Transform playerBody;          // Reference to the player body for rotating the entire body

    private float xRotation = 0f;         // Stores the current x-axis rotation

    void Start()
    {
        // Lock the cursor to the center of the screen and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get mouse movement input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Adjust the x-axis rotation (looking up and down)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp to avoid flipping

        // Apply rotation to the camera (head)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate the player body based on the mouse's X-axis movement (looking left and right)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
