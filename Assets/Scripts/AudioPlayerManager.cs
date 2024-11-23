using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AudioPlayerType
{
    MusicPlayer,
    SoundsPlayer,
}

public class AudioPlayerManager : MonoBehaviour
{
    //����������
    public AudioPlayerType playerType;
    private AudioSource thisPlayer;
    // Start is called before the first frame update
    void Start()
    {
        thisPlayer = GetComponent<AudioSource>();
        GameManager.Instance.AddAudioPlayer(thisPlayer, playerType);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManager.Instance?.RemoveAudioPlayer(thisPlayer, playerType);
    }
}
