using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputManager = UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager;

public class Rotation : MonoBehaviour
{

    public GameObject toRotate;
    public float rotationSpeed = 5;
    public string HorizontalAxisName = "Horizontal", VerticalAxisName = "Vertical";


    // Update is called once per frame
    void Update()
    {
        Vector2 rotMultiplier = new Vector3(InputManager.GetAxis(HorizontalAxisName), InputManager.GetAxis(VerticalAxisName), 0);
        toRotate.transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed * rotMultiplier.x, Space.World);
        toRotate.transform.Rotate(Vector3.left, Time.deltaTime * rotationSpeed * rotMultiplier.y, Space.Self);
    }

    private void OnValidate()
    {
        if (rotationSpeed <= 0) rotationSpeed = 1;
    }
}
