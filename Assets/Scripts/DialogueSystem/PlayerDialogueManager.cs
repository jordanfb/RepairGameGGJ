using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogueManager : MonoBehaviour
{
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
                runner.StartDialogue(npc.talkToNode);
                playerMovement.isAllowedToMove = !runner.isDialogueRunning; // only stop the player from moving if dialogue starts
                lastInteractedNPC = npc;
                npc.Played();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        OurNPC npc = other.gameObject.GetComponentInChildren<OurNPC>();
        //print("Left collision with " + other.gameObject.name);
        if (npc == lastInteractedNPC)
        {
            lastInteractedNPC = null; // reset the node so we can talk to it again
        }
    }

    public void DialogueEnded()
    {
        playerMovement.isAllowedToMove = true;
    }
}
