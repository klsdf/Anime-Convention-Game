using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音频播放器类型枚举
/// </summary>
public enum AudioPlayerType
{
    /// <summary>
    /// 音乐播放器
    /// </summary>
    MusicPlayer,

    /// <summary>
    /// 音效播放器
    /// </summary>
    SoundsPlayer,
}

/// <summary>
/// 音频播放管理器
/// 负责管理和控制游戏中所有音频的播放
/// </summary>
public class AudioPlayerManager : MonoBehaviour
{
    /// <summary>
    /// 音频播放器类型
    /// </summary>
    public AudioPlayerType playerType;

    /// <summary>
    /// 音频源组件
    /// </summary>
    private AudioSource thisPlayer;

    /// <summary>
    /// 初始化音频播放器
    /// </summary>
    private void Start()
    {
        thisPlayer = GetComponent<AudioSource>();
        GameManager.Instance.AddAudioPlayer(thisPlayer, playerType);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 在对象销毁时移除音频播放器
    /// </summary>
    private void OnDestroy()
    {
        GameManager.Instance?.RemoveAudioPlayer(thisPlayer, playerType);
    }
}