using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 设置界面控制器
/// 管理游戏音量等设置的UI界面
/// </summary>
public class SettingUI : MonoBehaviour
{
    /// <summary>
    /// 设置界面是否打开
    /// </summary>
    public bool isOpen = false;

    /// <summary>
    /// 主音量滑动条
    /// </summary>
    public Slider mainVolume;

    /// <summary>
    /// 背景音乐音量滑动条
    /// </summary>
    public Slider bgmVolume;

    /// <summary>
    /// 音效音量滑动条
    /// </summary>
    public Slider soundEffectVolume;

    /// <summary>
    /// 主音量减少和增加按钮
    /// </summary>
    public Button reduceMainBtn, addMainBtn;

    /// <summary>
    /// 背景音乐音量减少和增加按钮
    /// </summary>
    public Button reduceBgmBtn, addBgmBtn;

    /// <summary>
    /// 音效音量减少和增加按钮
    /// </summary>
    public Button reduceSundEffectBtn, addSundEffectBtn;

    /// <summary>
    /// 初始化UI控件的事件监听
    /// </summary>
    private void Start()
    {
        //总音量控制相关
        mainVolume.onValueChanged.AddListener(MainVolumeChange);
        reduceMainBtn.onClick.AddListener(() => { ChangeVolume(mainVolume, -0.1f); });
        addMainBtn.onClick.AddListener(() => { ChangeVolume(mainVolume, 0.1f); });
        //音乐音量控制相关
        bgmVolume.onValueChanged.AddListener(BGMVolumeChange);
        reduceBgmBtn.onClick.AddListener(() => { ChangeVolume(bgmVolume, -0.1f); });
        addBgmBtn.onClick.AddListener(() => { ChangeVolume(bgmVolume, 0.1f); });
        //音效音量控制相关
        soundEffectVolume.onValueChanged.AddListener(SoundEffectVolumeChange);
        reduceSundEffectBtn.onClick.AddListener(() => { ChangeVolume(soundEffectVolume, -0.1f); });
        addSundEffectBtn.onClick.AddListener(() => { ChangeVolume(soundEffectVolume, 0.1f); });

        // GameManager.Instance.SettingUI(this);
    }

    /// <summary>
    /// 改变指定滑动条的音量值
    /// </summary>
    /// <param name="volumeSlider">目标滑动条</param>
    /// <param name="value">改变的值</param>
    private void ChangeVolume(Slider volumeSlider, float value)
    {
        volumeSlider.value += value;
    }

    /// <summary>
    /// 改变主音量
    /// </summary>
    /// <param name="volumeStrong">音量强度</param>
    private void MainVolumeChange(float volumeStrong)
    {
        GameManager.Instance.ChangeMainVolume(volumeStrong);
    }

    /// <summary>
    /// 改变背景音乐音量
    /// </summary>
    /// <param name="volumeStrong">音量强度</param>
    private void BGMVolumeChange(float volumeStrong)
    {
        GameManager.Instance.ChangeBgmVolume(volumeStrong);
    }

    /// <summary>
    /// 改变音效音量
    /// </summary>
    /// <param name="volumeStrong">音量强度</param>
    private void SoundEffectVolumeChange(float volumeStrong)
    {
        GameManager.Instance.ChangeSoundEffectVolume(volumeStrong);
    }

    /// <summary>
    /// 打开设置界面并初始化数值
    /// </summary>
    public void OpenSettingUI()
    {
        this.gameObject.SetActive(true);
        GameManager.Instance.isDragingObject = false;
        mainVolume.value = GameManager.Instance.mainVolume;
        bgmVolume.value = GameManager.Instance.bgmVolume;
        soundEffectVolume.value = GameManager.Instance.soundEffectVolume;
    }

    /// <summary>
    /// 关闭设置界面
    /// </summary>
    public void CloseSettingUI()
    {
        this.gameObject.SetActive(false);
        GameManager.Instance.isDragingObject = true;
    }
    
    // Update is called once per frame
    // void Update()
    // {
    //     if (isOpen)
    //     {
    //         if (Input.GetKeyUp(KeyCode.Escape))
    //         {
    //             isOpen = false;
    //             this.gameObject.SetActive(false);
    //             GameManager.Instance.isDragingObject = true;
    //         }
    //     }
    // }
}
