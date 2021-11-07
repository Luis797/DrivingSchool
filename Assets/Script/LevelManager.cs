using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] GameObject settingPanel;
  
    public void ReplayLevel()

    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
   
    public void NextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
   
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    bool showSetting = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowSetting();
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ShowSetting()
    {
        showSetting = !showSetting;
        Time.timeScale = showSetting ? 0 : 1;
        settingPanel.SetActive(showSetting);
    }
}
