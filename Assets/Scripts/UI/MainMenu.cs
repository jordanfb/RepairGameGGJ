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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(gameplaySceneIndex);
        SceneManager.LoadScene(levelSceneIndex, LoadSceneMode.Additive);
        SceneManager.LoadScene(uiSceneIndex, LoadSceneMode.Additive);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // stop playing in the editor
#else
        Application.Quit(); // quit the application in the built version
#endif
    }

    public void ControlRemapping()
    {
        // launch the SInput control remapping scene
        SceneManager.LoadScene(controlMapperSceneIndex);
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneIndex);
    }
}
