using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedItemTracker : MonoBehaviour
{

    // The dialogue runner that we want to attach the 'visited' function to
    [SerializeField] Yarn.Unity.DialogueRunner dialogueRunner;

    private HashSet<string> _collectedItems = new HashSet<string>();

    void Start()
    {
        // Register a function on startup called "collected" that lets Yarn
        // scripts query to see if an item has been collected.
        dialogueRunner.RegisterFunction("collected", 1, delegate (Yarn.Value[] parameters)
        {
            var itemName = parameters[0];
            return _collectedItems.Contains(itemName.AsString);
        });

    }

    // called when you collect an item
    public void CollectItem(string itemName)
    {
        _collectedItems.Add(itemName);
    }
}
