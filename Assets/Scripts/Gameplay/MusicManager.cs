using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public List<AudioClip> musicClips = new List<AudioClip>();
    public float transitionTime = 1; // seconds
    public bool playTrack0OnStart = true;

    [SerializeField]
    AudioSource source1 = null;
    [SerializeField]
    AudioSource source2 = null;


    AudioSource activeSource = null; // the main source, they swap back and forth
    AudioSource secondarySource = null;


    bool transitioning = false;

    public static float SmootherStep(float t)
    {
        return t * t * t * (t * (6f * t - 15f) + 10f);
    }


    private void Start()
    {
        activeSource = source1;
        secondarySource = source2;
        activeSource.volume = 1;
        secondarySource.volume = 0;

        if (playTrack0OnStart)
        {
            PlayMusicClip(0);
        }
    }

    public void PlayMusicClip(int i = 0)
    {
        if (i >= musicClips.Count)
        {
            Debug.LogError("Tried playing music out of range");
            return;
        }

        StartCoroutine(Transition(musicClips[i]));
    }

    public void PlayMusicClip(AudioClip c)
    {
        StartCoroutine(Transition(c));
    }

    private IEnumerator Transition(AudioClip newClip)
    {
        if (transitionTime <= 0)
        {
            // swap instantly
            activeSource.volume = 1;
            activeSource.clip = newClip;
            activeSource.Play();
            secondarySource.Stop();
        }
        else
        {
            float progress = 0;
            secondarySource.volume = 0;
            secondarySource.clip = newClip;
            secondarySource.Play();
            while (progress < 1)
            {
                progress += Time.deltaTime / transitionTime;
                float t = SmootherStep(progress);
                secondarySource.volume = t;
                activeSource.volume = 1 - t;
                yield return null; // wait for a frame
            }

            // swap the sources!
            AudioSource tempSwap = activeSource;
            activeSource = secondarySource;
            secondarySource = tempSwap;

            // make sure the volumes are all correct
            activeSource.volume = 1;
            secondarySource.volume = 0;
            secondarySource.Stop();
        }
    }
}
