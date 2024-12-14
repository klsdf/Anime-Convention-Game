//作者：闫辰祥
//创建时间: 2024年12月7日

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

/// <summary>
/// 音频类型枚举
/// </summary>
public enum SoundType
{
    /// <summary>背景音乐</summary>
    Music,
    /// <summary>音效</summary>
    SE,
    /// <summary>按钮音效</summary>
    ButtonSE,
    /// <summary>脚步声</summary>
    Footstep,
}

/// <summary>
/// 音频配置字典项，用于在Inspector中配置音频类型和对应的音频片段
/// </summary>
[System.Serializable]
public class AudioDictionary
{
    /// <summary>音频类型</summary>
    public SoundType key;
    /// <summary>对应的音频片段</summary>
    public AudioClip value;
}

/// <summary>
/// 音频播放控制器，负责管理和播放所有游戏音频
/// </summary>
/// <remarks>
/// 此控制器使用单例模式，确保场景中只有一个音频控制器实例
/// 可以同时管理多个音频源，自动处理音频的播放和停止
/// </remarks>
public class AudioPlayerController : Singleton<AudioPlayerController>
{
    [Header("音频字典")]
    [SerializeField]
    public List<AudioDictionary> soundDic = new List<AudioDictionary>();

    private List<AudioSource> audioPlayers = new List<AudioSource>();
    private int currentPlayerIndex = 0;

    void Start()
    {
        AddNewAudioSource();
        // gameObject.SetActive(false);
    }

    void Update()
    {

    }

    private void OnDestroy()
    {
        foreach(var player in audioPlayers)
        {
            // GameManager.Instance.RemoveAudioPlayer(player, playerType);
            Destroy(player);
        }
    }

    private void AddNewAudioSource()
    {
        AudioSource newPlayer = gameObject.AddComponent<AudioSource>();
        audioPlayers.Add(newPlayer);
        // GameManager.Instance.AddAudioPlayer(newPlayer, playerType);
    }

    /// <summary>
    /// 停止所有正在播放的音乐
    /// </summary>
    public void StopAllMusic()
    {
        foreach(var player in audioPlayers)
        {
            if (player.clip != null && player.isPlaying)
            {
                player.Stop();
            }
        }
    }

    /// <summary>
    /// 播放指定类型的音频
    /// </summary>
    /// <param name="type">要播放的音频类型</param>
    /// <remarks>
    /// 如果是背景音乐(Music)类型，会先停止其他正在播放的音乐
    /// 如果没有空闲的音频源，会自动创建新的音频源
    /// </remarks>
    public void PlayAudio(SoundType type)
    {
        // 如果是音乐类型，确保停止其他音乐
        if (type == SoundType.Music)
        {
            StopAllMusic();
        }

        // 查找可用的播放器
        bool needNewPlayer = true;
        for(int i = 0; i < audioPlayers.Count; i++)
        {
            if(!audioPlayers[i].isPlaying)
            {
                currentPlayerIndex = i;
                needNewPlayer = false;
                break;
            }
        }

        if(needNewPlayer)
        {
            AddNewAudioSource();
            currentPlayerIndex = audioPlayers.Count - 1;
        }

        var audioData = soundDic.Find(x => x.key == type);
        if (audioData != null && audioData.value != null)
        {
            audioPlayers[currentPlayerIndex].clip = audioData.value;
            audioPlayers[currentPlayerIndex].Play();
        }
        else
        {
            Debug.LogWarning($"No audio clip found for type: {type}");
        }
    }

    /// <summary>
    /// 更改所有音频源的音量
    /// </summary>
    /// <param name="volume">音量值(0-1)</param>
    public void ChangeMainVolume(float volume)
    {
        foreach (var item in audioPlayers)
        {
            item.volume = volume;
        }
    }

    /// <summary>
    /// 获取指定类型的音频剪辑
    /// </summary>
    /// <param name="type">音频类型</param>
    /// <returns>音频剪辑，如果未找到返回null</returns>
    public AudioClip GetAudioClip(SoundType type)
    {
        var audioData = soundDic.Find(x => x.key == type);
        return audioData?.value;
    }
}
