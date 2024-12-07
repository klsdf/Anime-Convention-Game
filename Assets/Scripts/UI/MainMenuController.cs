using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;
//作者：闫辰祥
//创建时间: DATE


/// <summary>
/// 控制游戏一开始的主菜单
/// </summary>
public class MainMenuController : MonoBehaviour
{
    public Button startGameBtn;
    // public Button beingAssertBtn;
    public Button settingBtn;
    public Button quitBtn;
    public GameObject settingUI;

    private void Awake() {
        startGameBtn.onClick.AddListener(StartGame);
        settingBtn.onClick.AddListener(OpenSettingPanel);
        quitBtn.onClick.AddListener(QuitGame);
    }
    private void PlaySE()
    {
        AudioPlayerController.Instance.PlayAudio(SoundType.ButtonSE);
    }
    public void StartGame()
    {
        PlaySE();
        SceneManager.LoadScene("TopDown");
    }
    // public void BeingAssert()
    // {
    //     PlaySE();
    //     SceneManager.LoadScene("GetAssert");
    // }
    public void OpenSettingPanel()
    {
        PlaySE();
        settingUI.SetActive(true);
 
    }

    public void QuitGame()
    {
        PlaySE();
        Application.Quit();
    }
}
