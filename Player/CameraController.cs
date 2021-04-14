using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    //variables and references handling the camera
    public float mouseSensitivity = 150f;
    [SerializeField] private Transform playerBody;
    private float xRotation = 0f;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
     Cursor.lockState = CursorLockMode.Locked;
    }
    void LateUpdate()
    {
        //sets the mouse input, multiplied by sensitivity (and time, but this is required to move), into variables
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //ensures camera doesnt "flip", and clamps the camera to have a max and min Y value of 90 degrees
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //rotates the camera using variables and rotates the player body "sideways"
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);    
    }
}
