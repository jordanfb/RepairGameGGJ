using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int mainMenuSceneIndex = 0;
    public int controlMapperSceneIndex = 1;
    public int gameplaySceneIndex = 2;
    public int uiSceneIndex = 3;
    public int levelSceneIndex = 4;
    public int creditSceneIndex = 5;
    public bool initializeMouseSensitivity = true;


    // Start is called before the first frame update
    void Start()
    {
        if (initializeMouseSensitivity)
        {
            InitializeMouseSensitivity();
        }
    }

    private void InitializeMouseSensitivity()
    {
        // this is a really janky way to set the mouse sensitivity to .2f by default to balance out the controller input
        if (!PlayerPrefs.HasKey("OneTimeRun.MouseSensitivity"))
        {
            Sinput.LoadControlScheme("MainControlScheme", true); // need to load it before we can save it

            Sinput.mouseSensitivity = .2f; // initialize it once
            SinputSystems.SinputFileIO.SaveControls(Sinput.controls, Sinput.controlSchemeName);
            PlayerPrefs.SetString("OneTimeRun.MouseSensitivity", "done");
        }
    }

    [ContextMenu("Delete one time run keys")]
    private void ClearOneTimePrefs()
    {
        if (PlayerPrefs.HasKey("OneTimeRun.MouseSensitivity"))
        {
            PlayerPrefs.DeleteKey("OneTimeRun.MouseSensitivity");
        }
    }

    [ContextMenu("Play Game")]
    public void PlayGame()
    {
        SceneManager.LoadScene(gameplaySceneIndex);
        SceneManager.LoadScene(levelSceneIndex, LoadSceneMode.Additive);
        SceneManager.LoadScene(uiSceneIndex, LoadSceneMode.Additive);
    }

    [ContextMenu("Quit Game")]
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // stop playing in the editor
#else
        Application.Quit(); // quit the application in the built version
#endif
    }

    [ContextMenu("Control RemappingGame")]
    public void ControlRemapping()
    {
        // launch the SInput control remapping scene
        SceneManager.LoadScene(controlMapperSceneIndex);
    }

    [ContextMenu("Main Menu Scene")]
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneIndex);
    }

    [ContextMenu("Credit scene")]
    public void CreditScene()
    {
        SceneManager.LoadScene(creditSceneIndex);
    }

    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
