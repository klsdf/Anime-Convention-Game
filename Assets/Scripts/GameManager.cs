using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//作者：闫辰祥
//创建时间: 2024年8月12日

public class GameManager : Singleton<GameManager>
{
    public bool isDragingObject;

    public float bgmVolume = 1;//音乐音量
    public float soundEffectVolume = 1;//音效音量
    public float mainVolume = 1;//总音量

    private List<AudioSource> bgmAudioPlayer = new List<AudioSource>();//音乐播放器
    private List<AudioSource> soundEffectAudioPlayer = new List<AudioSource>();//音效播放器

    public SettingUI setting = null;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!setting.isOpen)
                setting.OpenSettingUI();
        }
    }
    public void SettingUI(SettingUI settingUI)
    {
        setting = settingUI;
        setting.gameObject.SetActive(false);
    }
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
