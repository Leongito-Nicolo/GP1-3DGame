using UnityEngine;

public class LookAtCursor : MonoBehaviour
{
    public float mouseSensitivity;       // sensitivity of the mouse movement
    public Transform playerBody;                // tranform of the player to rotate along with camera

    private float xRotation = 0f;               // to store up and down rotation

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;       // lock the cursor at the center of the screen when the scene loaded
    }

    void Update()
    {
        // mouse horizontal and vertical input multiplied by sensitivity
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;   
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;        // adjust vertical rotation on the mouse movement
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // prevent camera to flip

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);      // apply rotation to the camera

        playerBody.Rotate(Vector3.up * mouseX);     // rotate the player with the camera
    }
}
