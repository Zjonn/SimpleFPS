using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputManager = UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager;

public class Movement : MonoBehaviour
{

    public float speed = 1;
    public string XAxisName = "AxisX", ZAxisName = "AxisZ";


    // Update is called once per frame
    void Update()
    {
        Vector3 translation = new Vector3(InputManager.GetAxis(XAxisName), 0, InputManager.GetAxis(ZAxisName));
        translation *= speed * Time.deltaTime;

        transform.Translate(translation);
    }

    private void OnValidate()
    {
        if (speed <= 0) speed = 1;
    }
}
