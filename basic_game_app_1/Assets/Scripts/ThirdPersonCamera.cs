using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public float RotationSpeed = 1;
    public Transform Target, Player, Camera;
    float mouseX, mouseY;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        CamControl();
        ZoomHandler();
    }

    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * RotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * RotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(Target);

        // Rotate camera around player
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        }
        else
        {
            Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
            Player.rotation = Quaternion.Euler(0, mouseX, 0);
        }
    }

    const float zoomMin = -2.5f;
    const float zoomMax = -8.5f;
    void ZoomHandler()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            Vector3 pos = Camera.position;
            pos.z = pos.z + 0.5f;
            if (pos.z < zoomMin)
            {
                Camera.position = pos;
            }
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            Vector3 pos = Camera.position;
            pos.z = pos.z - 0.5f;
            if (pos.z > zoomMax)
            {
                Camera.position = pos;
            }
        }
    }
}
