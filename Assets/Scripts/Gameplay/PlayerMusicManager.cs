using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMusicManager : MonoBehaviour
{
    public MusicManager musicManager;
    private MusicChanger lastInteractedMusicChanger = null;

    private void OnTriggerEnter(Collider other)
    {
        MusicChanger musicChanger = other.gameObject.GetComponent<MusicChanger>();
        //print("Collided with " + other.gameObject.name);
        if (musicChanger != null && musicChanger != lastInteractedMusicChanger)
        {
            if (musicChanger.ShouldChange())
            {
                // then try talking with it!
                musicManager.PlayMusicClip(musicChanger.clip);
                musicChanger.ChangedMusic();
                lastInteractedMusicChanger = musicChanger;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        MusicChanger musicChanger = other.gameObject.GetComponentInChildren<MusicChanger>();
        //print("Left collision with " + other.gameObject.name);
        if (musicChanger == lastInteractedMusicChanger)
        {
            lastInteractedMusicChanger = null; // reset the node so we can talk to it again
        }
    }
}
