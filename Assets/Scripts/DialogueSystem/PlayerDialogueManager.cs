using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogueManager : MonoBehaviour
{
    public bool canMoveWhileTalking = true;
    public float resetNPCTime = 5f;
    public SimpleMovement playerMovement; // disable movement when it's time to talk to things!
    public string interactionButton = "Interact";

    private bool hasAddedListener = false; // so that we only add ourselves as a listener once

    private OurNPC lastInteractedNPC = null;

    private void OnTriggerEnter(Collider other)
    {
        OurNPC npc = other.gameObject.GetComponentInChildren<OurNPC>();
        //print("Collided with " + other.gameObject.name);
        if (npc != null && npc != lastInteractedNPC)
        {
            if (!hasAddedListener)
            {
                // find the dialogue component and register that we want to know when dialogue ends and allow the player to move
                FindObjectOfType<Yarn.Unity.DialogueUI>().onDialogueEnd.AddListener(() => { DialogueEnded(); });
                hasAddedListener = true;
            }
            if (npc.CanPlay())
            {
                // then try talking with it!
                Yarn.Unity.DialogueRunner runner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
                if (!runner.isDialogueRunning)
                {
                    // don't start the dialogue if it's already talking, we'll have to account for that in our script by checking if we've said things already
                    runner.StartDialogue(npc.talkToNode);
                }
                playerMovement.isAllowedToMove = !runner.isDialogueRunning || canMoveWhileTalking; // only stop the player from moving if dialogue actually starts
                lastInteractedNPC = npc;
                npc.Played();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        OurNPC npc = other.gameObject.GetComponentInChildren<OurNPC>();
        //print("Left collision with " + other.gameObject.name);
        if (lastInteractedNPC != null && lastInteractedNPC.resetImmediately && npc == lastInteractedNPC)
        {
            StartCoroutine(ResetLastInteractedNPCAfterTime(npc, resetNPCTime));
        }
    }

    private IEnumerator ResetLastInteractedNPCAfterTime(OurNPC lastNPC, float t)
    {
        yield return new WaitForSeconds(t);
        if (lastNPC == lastInteractedNPC)
        {
            // only reset it if we haven't talked to another node since
            lastInteractedNPC = null; // reset the node so we can talk to it again
        }
    }

    public void DialogueEnded()
    {
        playerMovement.isAllowedToMove = true;
    }
}
