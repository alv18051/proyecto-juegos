using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] public float mouseSensitivity = 3.5f;
    [SerializeField] public float mouseSmoothTime = 0.03f;
    [SerializeField] bool lockCursorOnStart = true;

    float cameraPitch = 0.0f;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;
    Vector2 targetMouseDelta = Vector2.zero;

    bool isPaused = false;
    

    void Start()
    {
        pauseDetect();
        if (lockCursorOnStart)
        {
            if (!isPaused)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    void LateUpdate()
    {
        pauseDetect();
        if (!isPaused)
        {
            UpdateMouseInput();
        }/*
        else
        {
            currentMouseDelta = Vector2.zero;
            playerCamera.transform.localEulerAngles = Vector3.right * cameraPitch;
            transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
        }*/
    }

    void UpdateMouseInput()
    {
        targetMouseDelta = new Vector2 (Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        playerCamera.transform.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void pauseDetect()
    {
        if (Time.timeScale == 1.0f)
        {
            isPaused = false;
        }
        else
        {
            isPaused = true;
        }
    }
}
