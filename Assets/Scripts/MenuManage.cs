using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManage : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void QuitApp()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
