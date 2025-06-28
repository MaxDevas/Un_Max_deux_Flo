using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 120f;

    float xRotation = 0f;
    float yRotation = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; 
        // Rotation up and down (autour de la x-axis)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);// Clamp

        // Rotation left right (autour de la y-axis)
        yRotation += mouseX;

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
