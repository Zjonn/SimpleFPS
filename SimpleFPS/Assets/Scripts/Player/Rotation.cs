using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputManager = UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager;

public class Rotation : MonoBehaviour
{
    public GameObject cameraHolder;
    public float rotationSpeed = 5;
    public float minVerticalAngle, maxVerticalAngle;
    public string horizontalAxisName = "Horizontal", verticalAxisName = "Vertical";


    // Update is called once per frame
    void Update()
    {
        HorizontalRotation();
        VerticalRotation();
    }

    void HorizontalRotation()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed * InputManager.GetAxis(horizontalAxisName));
    }

    void VerticalRotation()
    {
        float rotationFactor = InputManager.GetAxis(verticalAxisName);
        rotationFactor *= Time.deltaTime * rotationSpeed;

        float angle = (cameraHolder.transform.rotation.eulerAngles.x - rotationFactor) % 360;

        if (IsAngleInRange(angle))
        {
            cameraHolder.transform.Rotate(Vector3.left, rotationFactor, Space.Self);
        }
    }

    bool IsAngleInRange(float angle)
    {
        return (minVerticalAngle > maxVerticalAngle && angle >= minVerticalAngle || angle <= maxVerticalAngle) ||
            (maxVerticalAngle >= minVerticalAngle && angle >= minVerticalAngle && angle <= maxVerticalAngle);
    }

    private void OnValidate()
    {
        if (rotationSpeed <= 0) rotationSpeed = 1;
        minVerticalAngle = (minVerticalAngle < 0) ? 360 + minVerticalAngle % 360 : minVerticalAngle % 360;
        maxVerticalAngle = (maxVerticalAngle > 360) ? 360 + maxVerticalAngle % 360 : maxVerticalAngle % 360;
    }
}
