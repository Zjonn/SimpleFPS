using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputManager = UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{

    public float speed = 1;
    public string XAxisName = "AxisX", ZAxisName = "AxisZ";

    private Rigidbody rb;
    private float XAxis, ZAxis;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        XAxis = InputManager.GetAxis(XAxisName);
        ZAxis = InputManager.GetAxis(ZAxisName);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 normalToForward = new Vector3(transform.forward.z, 0, -transform.forward.x);
        Vector3 shift = transform.forward * XAxis + normalToForward * ZAxis;
        shift *= speed * Time.smoothDeltaTime;

        rb.MovePosition(rb.position + shift);
    }

    private void OnValidate()
    {
        if (speed <= 0) speed = 1;
    }
}
