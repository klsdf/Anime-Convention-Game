//作者：闫辰祥
//创建时间: 2024年12月7日

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
// public enum AudioPlayerType
// {
//     MusicPlayer,
//     SoundsPlayer,
// }

public enum SoundType
{
    Music,
    SE,
    ButtonSE,
}

[System.Serializable]
public class AudioDictionary
{
    public SoundType key;
    public AudioClip value;
}



public class AudioPlayerController : Singleton<AudioPlayerController>
{
    
    [Header("音频字典")]
    [SerializeField]
    public List<AudioDictionary> soundDic = new List<AudioDictionary>();

    // public float mainVolume = 1;//总音量

    //音频播放器类型
    // public AudioPlayerType playerType;
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

    public void PlayAudio(SoundType type)
    {
        // 如果所有播放器都在播放中，添加新的播放器
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
        AudioClip clip = soundDic.Find(x => x.key == type).value;

        audioPlayers[currentPlayerIndex].clip = clip;
        audioPlayers[currentPlayerIndex].Play();
    }



    //     /// <summary>
    // /// 更改音乐音量
    // /// </summary>
    // /// <param name="volume"></param>
    // public void ChangeBgmVolume(float volume)
    // {
    //     foreach (var item in audioPlayers)
    //     {
    //         item.volume = volume * mainVolume;
    //     }
    // }

    /// <summary>
    /// 更改总音量
    /// </summary>
    /// <param name="volume"></param>
    public void ChangeMainVolume(float volume)
    {
        // mainVolume = volume;
        foreach (var item in audioPlayers)
        {
            item.volume = volume;
        }
    }
}
