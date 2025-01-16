using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//作者：闫辰祥
//创建时间: 2024年8月12日

/// <summary>
/// 游戏管理器
/// 管理游戏全局状态和音频系统
/// </summary>
public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// 是否正在拖拽物体
    /// </summary>
    public bool isDragingObject;

    /// <summary>
    /// 背景音乐音量
    /// </summary>
    public float bgmVolume = 1;

    /// <summary>
    /// 音效音量
    /// </summary>
    public float soundEffectVolume = 1;

    /// <summary>
    /// 主音量
    /// </summary>
    public float mainVolume = 1;

    /// <summary>
    /// 背景音乐播放器列表
    /// </summary>
    private List<AudioSource> bgmAudioPlayer = new List<AudioSource>();

    /// <summary>
    /// 音效播放器列表
    /// </summary>
    private List<AudioSource> soundEffectAudioPlayer = new List<AudioSource>();

    /// <summary>
    /// 设置界面引用
    /// </summary>
    public SettingUI setting = null;

    /// <summary>
    /// 设置界面是否打开
    /// </summary>
    public bool isOpenSettingUI = false;

    void Start()
    {
        setting = Resources.Load<SettingUI>("UI/SettingMenu");
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isOpenSettingUI == true)
            {
                isOpenSettingUI = false;    
                setting.CloseSettingUI();
            }
            else
            {
                isOpenSettingUI = true;
                setting.OpenSettingUI();
            }
        }
    }
    // public void SettingUI(SettingUI settingUI)
    // {
    //     setting = settingUI;
    //     setting.gameObject.SetActive(false);
    // }
    /// <summary>
    /// 添加音乐播放器
    /// </summary>
    /// <param name="audioSource"></param>
    public void AddAudioPlayer(AudioSource audioSource, AudioPlayerType playerType)
    {
        switch (playerType)
        {
            case AudioPlayerType.MusicPlayer:
                if (!bgmAudioPlayer.Contains(audioSource))
                    bgmAudioPlayer.Add(audioSource);
                break;
            case AudioPlayerType.SoundsPlayer:
                if (!soundEffectAudioPlayer.Contains(audioSource))
                    soundEffectAudioPlayer.Add(audioSource);
                break;
            default:
                break;
        }
        audioSource.volume = bgmVolume;
    }
    /// <summary>
    /// 移除音乐播放器
    /// </summary>
    /// <param name="audioSource"></param>
    public void RemoveAudioPlayer(AudioSource audioSource, AudioPlayerType playerType)
    {
        switch (playerType)
        {
            case AudioPlayerType.MusicPlayer:
                if (bgmAudioPlayer.Contains(audioSource))
                    bgmAudioPlayer.Remove(audioSource);
                break;
            case AudioPlayerType.SoundsPlayer:
                if (soundEffectAudioPlayer.Contains(audioSource))
                    soundEffectAudioPlayer.Remove(audioSource);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 更改音乐音量
    /// </summary>
    /// <param name="volume"></param>
    public void ChangeBgmVolume(float volume)
    {
        bgmVolume = volume;
        foreach (var item in bgmAudioPlayer)
        {
            item.volume = bgmVolume * mainVolume;
        }
    }
    /// <summary>
    /// 更改音效音量
    /// </summary>
    /// <param name="volume"></param>
    public void ChangeSoundEffectVolume(float volume)
    {
        soundEffectVolume = volume;
        foreach (var item in soundEffectAudioPlayer)
        {
            item.volume = soundEffectVolume * mainVolume;
        }
    }
    /// <summary>
    /// 更改总音量
    /// </summary>
    /// <param name="volume"></param>
    public void ChangeMainVolume(float volume)
    {
        mainVolume = volume;
        foreach (var item in bgmAudioPlayer)
        {
            item.volume = bgmVolume * mainVolume;
        }
        foreach (var item in soundEffectAudioPlayer)
        {
            item.volume = soundEffectVolume * mainVolume;
        }
    }
}
