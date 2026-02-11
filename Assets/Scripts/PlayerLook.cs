using UnityEngine;
using UnityEngine.InputSystem; // New Input System

public class PlayerLook : MonoBehaviour
{
    public static PlayerLook instance;          // Singleton reference for access from other scripts

    private float shakeDuration = 0f;           // Remaining time for screen shake
    private float shakeMagnitude = 0.1f;        // Strength of camera shake
    private float shakeFadeSpeed = 1.5f;        // Speed shake fades out
    private Vector3 initialCamPos;              // Original camera local position


    public float mouseSensitivity = 50f;        // final value used at the end of the episode  
    public Transform cam;

    private float xRotation = 0f;
    private Vector2 lookInput;

    // Initialize singleton instance
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        initialCamPos = cam.localPosition;      // Store starting camera position
    }

    void Update()
    {
        HandleMouseLook();
        HandleShake();          // Apply camera shake if active
    }

    // Trigger screen shake with duration and strength
    public void AddShake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }

    // Apply shake effect each frame
    private void HandleShake()
    {
        if (Time.timeScale == 0f)
        {
            cam.localPosition = initialCamPos;
            return;
        }

        if (shakeDuration > 0)
        {
            // Offset camera randomly within sphere
            cam.localPosition = initialCamPos + Random.insideUnitSphere * shakeMagnitude;
            // Reduce remaining shake time
            shakeDuration -= Time.deltaTime * shakeFadeSpeed;
        }
        else
        {
            // Reset camera to original position
            cam.localPosition = initialCamPos;
        }
    }

    // Called by Player Input (Send Messages) when Look value changes
    void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    void HandleMouseLook()
    {
        // Convert input to per-second rotation using sensitivity and deltaTime
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        // Vertical look (camera) — clamp to prevent over-rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontal look (player body)
        transform.Rotate(Vector3.up * mouseX);
    }
}
