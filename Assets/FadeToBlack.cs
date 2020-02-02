using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    [SerializeField] Yarn.Unity.DialogueRunner dialogueRunner;
    [SerializeField] Image fadeToBlackImage;
    [SerializeField] float fadeInTime = 1f;
    [SerializeField] float fadeOutTime = 1f;
    [SerializeField] MainMenu mainMenuReference;


    void Start()
    {
        dialogueRunner.AddCommandHandler("fadein", FadeIn);
        dialogueRunner.AddCommandHandler("exittomainmenu", FadeOutExit);
        dialogueRunner.AddCommandHandler("fadeout", FadeOut);
    }

    public void FadeIn(string[] parameters)
    {
        StartCoroutine(FadeCoroutine(fadeInTime, 1, 0, false));
    }

    public IEnumerator FadeCoroutine(float time, float startTransparency, float endTransparency, bool exitToMainMenuAfter)
    {
        Color c = fadeToBlackImage.color;
        if (time > 0)
        {
            float progress = 0;
            while (progress < 1)
            {
                progress += Time.deltaTime / time;
                c.a = Mathf.Lerp(startTransparency, endTransparency, MusicManager.SmootherStep(progress));
                fadeToBlackImage.color = c;
                yield return null; // wait a frame
            }
        }
        c.a = endTransparency;
        fadeToBlackImage.color = c;
        
        if (exitToMainMenuAfter)
        {
            // then exit to the main menu!
            mainMenuReference.ExitToMainMenu();
        }
    }

    public void FadeOut(string[] parameters)
    {
        StartCoroutine(FadeCoroutine(fadeOutTime, 0, 1, false));
    }

    public void FadeOutExit(string[] parameters)
    {
        StartCoroutine(FadeCoroutine(fadeOutTime, 0, 1, true));
    }
}
