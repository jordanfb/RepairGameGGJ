using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController characterController = null; // to move
    [SerializeField]
    private Transform tiltBody = null; // to tilt when moving

    [SerializeField]
    float walkSpeed = 5;
    [SerializeField]
    float turnSpeed = 3;
    [SerializeField]
    float sprintMultiplier = 2;
    [SerializeField]
    float tiltMultiplier = 100;

    [Space]
    [SerializeField]
    public List<LegGroup> legGroups; // these all step at different times
    [SerializeField]
    public float timePerLegGroup = .5f;
    [SerializeField]
    public bool moveToRestingPosition = false;

    [Space]
    [SerializeField]
    private string strafeAxis = "Horizontal";
    [SerializeField]
    private string forwardsAxis = "Vertical";
    [SerializeField]
    private string turnAxis = "Look Horizontal";
    [SerializeField]
    private KeyCode sprintKey = KeyCode.LeftShift;


    private int previousLegGroup = 0;
    private float legTimer = 0;


    [Serializable]
    public class LegGroup
    {
        public List<Leg> legs;
    }


    // Start is called before the first frame update
    void Start()
    {

    }


    [ContextMenu("Draw all legs")]
    public void DrawAllLegs()
    {
        foreach(LegGroup lg in legGroups)
        {
            foreach (Leg l in lg.legs)
            {
                l.MoveLeg(0);
                l.DrawLeg();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        Vector3 input = new Vector3(Sinput.GetAxis(strafeAxis), 0, Sinput.GetAxis(forwardsAxis));
        if (input.sqrMagnitude > 1)
        {
            input.Normalize();
        }

        float df = Sinput.GetAxis(turnAxis) * turnSpeed * Time.fixedDeltaTime;
        transform.Rotate(0, df, 0);

        float currspeed = walkSpeed * (Input.GetKey(sprintKey) ? sprintMultiplier : 1) * Time.fixedDeltaTime;
        Vector3 dpos = input * currspeed;

        characterController.SimpleMove(transform.forward * dpos.z + transform.right * dpos.x);

        // move leg position!
        if (characterController.velocity.sqrMagnitude > 0 || Mathf.Abs(df) > 0 || moveToRestingPosition)
        { 
            tiltBody.localRotation = Quaternion.Euler(input.z * tiltMultiplier, 0, -input.x * tiltMultiplier);

            // then we adjust the timer!
            legTimer += Time.fixedDeltaTime;
            legTimer %= timePerLegGroup * legGroups.Count;
            int currLeg = Mathf.FloorToInt(legTimer / timePerLegGroup);
            if (previousLegGroup != currLeg)
            {
                // disable the previous legs
                for (int i = 0; i < legGroups[previousLegGroup].legs.Count; i++)
                {
                    legGroups[previousLegGroup].legs[i].StopLeg();
                }

                // now update the current legs!
                for (int i = 0; i < legGroups[currLeg].legs.Count; i++)
                {
                    legGroups[currLeg].legs[i].MoveLeg(timePerLegGroup); // for now just move it directly
                }

                previousLegGroup = currLeg;
            }
        }
    }
}
