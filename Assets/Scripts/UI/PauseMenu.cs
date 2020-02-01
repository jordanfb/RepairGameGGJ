using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public MainMenu mainMenuComponentReference;
    public GameObject pauseMenuObject;
    public GameObject pauseMenuSelectedObject;

    private bool wasLocked = false;
    private GameObject previouslySelectedObject = null;

    private bool isPaused = false;
    private float fixedDeltaTime = .2f;

    private void Start()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
        print(Time.fixedDeltaTime);
    }

    private void Update()
    {
        if (Sinput.GetButtonDown("Menu"))
        {
            // then toggle pause!
            isPaused = !isPaused;
            if (isPaused )
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = fixedDeltaTime;
        mainMenuComponentReference.ExitToMainMenu();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = fixedDeltaTime;
        isPaused = false;
        pauseMenuObject.gameObject.SetActive(false);
        if (wasLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        EventSystem.current.SetSelectedGameObject(previouslySelectedObject);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0;
        isPaused = true;
        wasLocked = !Cursor.visible;
        previouslySelectedObject = EventSystem.current.currentSelectedGameObject;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuObject.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(pauseMenuSelectedObject);
    }
}
