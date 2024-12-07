using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;
//作者：闫辰祥
//创建时间: DATE

public class UIManager : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }
    private void PlaySE()
    {
        AudioPlayerController.Instance.PlayAudio(SoundType.ButtonSE);
    }
    public void StartGame()
    {
        PlaySE();
        SceneManager.LoadScene("TopDown");
    }
    public void BeingAssert()
    {
        PlaySE();
        SceneManager.LoadScene("GetAssert");
    }
}
