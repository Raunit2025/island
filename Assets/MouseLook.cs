using UnityEngine;

public class MouseLook : MonoBehaviour 
{
    public float mouseSensitivity = 300f;
    public Transform Player;

    float xRotation = 0f;
    float yRotation = 0f;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;        
 
        xRotation -= mouseY; 
        
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

        Player.Rotate(Vector3.up * mouseX);
    }
}