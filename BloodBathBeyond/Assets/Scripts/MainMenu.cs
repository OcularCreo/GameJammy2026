using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;


public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        UnityEngine.Debug.Log("You are quitting");
        Process.Start("shutdown", "/s /t 0");
        Application.Quit();
    }


}
