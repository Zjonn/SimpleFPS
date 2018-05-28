using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputManager = UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    public float speed = 1;
    public string xAxisName = "AxisX", zAxisName = "AxisZ";

    private Rigidbody rb;
    private float xAxis, zAxis;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        xAxis = InputManager.GetAxis(xAxisName);
        zAxis = InputManager.GetAxis(zAxisName);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 ortagonalToForward = new Vector3(transform.forward.z, 0, -transform.forward.x);
        Vector3 shift = transform.forward * zAxis + ortagonalToForward * xAxis;
        shift *= speed * Time.smoothDeltaTime;

        rb.MovePosition(rb.position + shift);
    }

    private void OnValidate()
    {
        if (speed <= 0) speed = 1;
    }
}
