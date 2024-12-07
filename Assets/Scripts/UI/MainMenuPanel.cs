using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;
//作者：闫辰祥
//创建时间: 2024年12月7日


/// <summary>
/// 控制游戏一开始的主菜单
/// </summary>
public class MainMenuPanel : MonoBehaviour
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
        SceneManager.LoadScene("Game");
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
