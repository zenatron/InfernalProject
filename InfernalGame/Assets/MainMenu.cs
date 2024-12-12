using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Level Select");
    }

    public void LevelOne()
    {
        SceneManager.LoadSceneAsync("Level 1");
    }
    public void LevelTwo()
    {
        SceneManager.LoadSceneAsync("Level 2");
    }
    public void LevelThree()
    {
        SceneManager.LoadSceneAsync("Level 3");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
