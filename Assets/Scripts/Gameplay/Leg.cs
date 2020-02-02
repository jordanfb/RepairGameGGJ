using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour
{
    [SerializeField]
    LineRenderer lr = null;

    [SerializeField]
    Transform hip = null;
    [SerializeField]
    Transform goalFootPos = null;
    [SerializeField]
    Vector3 footPos = Vector3.zero;
    
    [Space]
    [SerializeField]
    AudioSource footstepSound;
    [SerializeField]
    AudioClip[] footsteps = null;

    [SerializeField]
    private float maxLegStretch = 1.5f;


    private float legLength = 0;
    private Vector3 lastHipPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        lastHipPos = hip.position;
        legLength = Vector3.Distance(hip.position, footPos);
        legLength = Mathf.Max(.01f, legLength); // don't divide by zero
        MoveLeg(0);
        DrawLeg();
    }

    // Update is called once per frame
    void Update()
    {
        DrawLeg();
    }

    public void MoveLeg(float moveTime)
    {
        // for testing just have it move directly.
        footPos = goalFootPos.position; // move it to the goal position.
        if (footstepSound)
        {
            AudioClip c = footsteps[Random.Range(0, footsteps.Length)];
            footstepSound.clip = c;
            footstepSound.PlayDelayed(Random.Range(0, .01f));
        }
    }

    public void StopLeg()
    {
        // stop moving
    }

    public void DrawLeg()
    {
        lr.positionCount = 2;
        lr.SetPosition(0, hip.position);
        lr.SetPosition(1, footPos);
    }

    public void DrawLegAndMove()
    {
        Vector3 deltaHip = hip.position - lastHipPos;
        Vector3 deltaFoot = footPos - hip.position;
        float currLength = Vector3.Distance(hip.position, footPos);
        float towardsFoot = Vector3.Dot(deltaFoot, deltaHip);
        if (currLength / legLength > maxLegStretch && towardsFoot < 0)
        {
            // then move the leg!
            // for now assume the hip is facing forwards?
            Vector3 inversePos = hip.InverseTransformPoint(footPos);
            inversePos.x *= -.5f; // flip the positions
            inversePos.z *= -.5f;
            footPos = hip.TransformPoint(inversePos);
        }
        lr.positionCount = 2;
        lr.SetPosition(0, hip.position);
        lr.SetPosition(1, footPos);

        // update hip pos so we can use it next time
        lastHipPos = hip.position;
    }
}
