using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    public AudioClip clip;
    public bool onlyChangeOnce = true;

    private bool hasChanged = false;

    public bool ShouldChange()
    {
        return !(onlyChangeOnce && hasChanged);
    }

    public void ChangedMusic()
    {
        hasChanged = true;
    }
}
