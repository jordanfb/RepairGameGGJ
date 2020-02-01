using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public UnityTemplateProjects.SimpleCameraController freecam; // this is included in the default unity setup now? May as well allow us to cheat and use it
    public bool usingFreecam = false;
    public KeyCode useFreeCam = KeyCode.F12;

    public SimpleMovement characterController; // so we can disable movement when using freecam

    public float movementDelay = .1f;
    public Vector3 followOffset = new Vector3(0, 5, 5);


    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(useFreeCam))
        {
            // then toggle it and swap how we control the camera!
            usingFreecam = !usingFreecam;
            characterController.enabled = !usingFreecam;
            freecam.enabled = usingFreecam;
        }

        if (!usingFreecam)
        {
            // then follow the player and try to align behind them probably?
            Vector3 moveTo = characterController.transform.position + characterController.transform.TransformVector(followOffset);
            transform.position = Vector3.Lerp(moveTo, transform.position, movementDelay);
            transform.LookAt(characterController.transform);
            transform.rotation = Quaternion.Lerp(Quaternion.LookRotation(characterController.transform.position - transform.position), transform.rotation, movementDelay);
            //transform.rotation.SetLookRotation(characterController.transform.position - transform.position); // look at the character for now
        }
    }
}
