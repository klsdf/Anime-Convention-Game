using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//作者：闫辰祥
//创建时间: 2024年8月12日

public class GameManager : Singleton<GameManager>
{
    public bool isDragingObject;

    // public SettingUI setting = null;

    void Start()
    {
        // setting = Resources.Load<SettingUI>("UI/SettingMenu");
    }

    public bool isOpenSettingUI = false;
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isOpenSettingUI == true)
            {
                isOpenSettingUI = false;    
                // setting.CloseSettingUI();
            }
            else
            {
                isOpenSettingUI = true;
                // setting.OpenSettingUI();
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
    // public void AddAudioPlayer(AudioSource audioSource, AudioPlayerType playerType)
    // {
    //     switch (playerType)
    //     {
    //         case AudioPlayerType.MusicPlayer:
    //             if (!bgmAudioPlayer.Contains(audioSource))
    //                 bgmAudioPlayer.Add(audioSource);
    //             break;
    //         case AudioPlayerType.SoundsPlayer:
    //             if (!soundEffectAudioPlayer.Contains(audioSource))
    //                 soundEffectAudioPlayer.Add(audioSource);
    //             break;
    //         default:
    //             break;
    //     }
    //     audioSource.volume = bgmVolume;
    // }
    /// <summary>
    /// 移除音乐播放器
    /// </summary>
    /// <param name="audioSource"></param>
    // public void RemoveAudioPlayer(AudioSource audioSource, AudioPlayerType playerType)
    // {
    //     switch (playerType)
    //     {
    //         case AudioPlayerType.MusicPlayer:
    //             if (bgmAudioPlayer.Contains(audioSource))
    //                 bgmAudioPlayer.Remove(audioSource);
    //             break;
    //         case AudioPlayerType.SoundsPlayer:
    //             if (soundEffectAudioPlayer.Contains(audioSource))
    //                 soundEffectAudioPlayer.Remove(audioSource);
    //             break;
    //         default:
    //             break;
    //     }
    // }

}
