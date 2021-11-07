using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject instructionPanel;
    [SerializeField] GameObject showLevels;
    [SerializeField] GameObject CompleteLevelFirst;
   public void StartGame()
    {
        SceneManager.LoadScene("level 1");
    }
    public void loadGame(int i)
    {
        if (PlayerPrefs.GetInt("Level") >= i-1)
            SceneManager.LoadScene(i);
        else
            CompleteLevelFirst.SetActive(true);

    }
   
    public void QuitGame()
    {
        Application.Quit();
    }
    bool showSetting = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowSetting();
        }
    }
    bool showInstruction = false;
    public void ShowSetting()
    {
        showSetting = !showSetting;
        Time.timeScale = showSetting? 0:1 ;
        settingPanel.SetActive(showSetting);
    }
    bool showInstructio = false;
    public void ShowInstructio()
    {
        showInstructio = !showInstructio;
        Time.timeScale = showInstructio ? 0 : 1;
        showLevels.SetActive(showInstructio);
    }
    public void ShowInstruction()
    {
        showInstruction = !showInstruction;
        Time.timeScale = showInstruction ? 0 : 1;
        instructionPanel.SetActive(showInstruction);
    }
}
