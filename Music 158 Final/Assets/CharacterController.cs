using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public float flyingSpeed = 10.0f;
    public float mouseSensitivity = 100.0f;

    private float verticalRotation = 90.0f;
    private float horizontalRotation = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Move forward/backward and strafe left/right
        float horizontalMovement = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        float verticalMovement = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        transform.Translate(new Vector3(horizontalMovement, 0, verticalMovement));


        // Fly up/down
        float flyingMovement = 0.0f;
        if (Input.GetKey(KeyCode.Space))
        {
            flyingMovement = flyingSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            flyingMovement = -flyingSpeed * Time.deltaTime;
        }
        transform.Translate(new Vector3(0, flyingMovement, 0));

        // Get mouse movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        // Rotate camera around y-axis based on mouse movement
        horizontalRotation += mouseX;

        // Rotate camera around x-axis based on mouse movement
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
    }
}
