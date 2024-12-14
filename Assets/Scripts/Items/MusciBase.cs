using UnityEngine;

/// <summary>
/// 音乐播放器基类，用于自动播放背景音乐
/// </summary>
/// <remarks>
/// 使用方法：
/// 1. 将此脚本附加到场景中的物体上
/// 2. 在Inspector中设置需要播放的音乐类型
/// </remarks>
public class MusicBase : MonoBehaviour
{
    /// <summary>
    /// 当前要播放的音乐类型
    /// </summary>
    [Header("音乐类型")]
    [SerializeField] private SoundType musicType = SoundType.Music;

    /// <summary>
    /// 是否在启用时自动播放
    /// </summary>
    [Header("是否自动播放")]
    [SerializeField] private bool playOnEnable = true;

    private void OnEnable()
    {
        if (playOnEnable)
        {
            PlayMusic();
        }
    }

    /// <summary>
    /// 播放背景音乐
    /// 会自动停止其他正在播放的背景音乐
    /// </summary>
    private void PlayMusic()
    {
        // 停止其他音乐，播放当前音乐
        AudioPlayerController.Instance.StopAllMusic();
        AudioPlayerController.Instance.PlayAudio(musicType);
    }
}
