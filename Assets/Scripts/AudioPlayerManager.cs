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
    //ÒôÁ¿²¥·ÅÆ÷
    public AudioPlayerType playerType;
    private AudioSource thisPlayer;
    // Start is called before the first frame update
    void Start()
    {
        thisPlayer = GetComponent<AudioSource>();
        GameManager.Instance.AddAudioPlayer(thisPlayer, playerType);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        GameManager.Instance.RemoveAudioPlayer(thisPlayer, playerType);
    }
}
