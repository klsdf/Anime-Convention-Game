using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    // public bool isOpen = false;
//     public Slider mainVolume;//总音量
//     public Slider bgmVolume;//音乐音量
//     public Slider soundEffectVolume;//音效音量
//     public Button reduceMainBtn, addMainBtn;//总音量增减按钮
//     public Button reduceBgmBtn, addBgmBtn;//音乐音量增减按钮
//     public Button reduceSundEffectBtn, addSundEffectBtn;//音效音量增减按钮
//     // Start is called before the first frame update
//     void Start()
//     {
//         //总音量控制相关
//         mainVolume.onValueChanged.AddListener(MainVelumeChange);
//         reduceMainBtn.onClick.AddListener(() => { ChangeMainVelume(mainVolume, -0.1f); });
//         addMainBtn.onClick.AddListener(() => { ChangeMainVelume(mainVolume, 0.1f); });
//         //音乐音量控制相关
//         bgmVolume.onValueChanged.AddListener(BGMVelumeChange);
//         reduceBgmBtn.onClick.AddListener(() => { ChangeMainVelume(bgmVolume, -0.1f); });
//         addBgmBtn.onClick.AddListener(() => { ChangeMainVelume(bgmVolume, 0.1f); });
//         //音效音量控制相关
//         soundEffectVolume.onValueChanged.AddListener(SoundEffectVelumeChange);
//         reduceSundEffectBtn.onClick.AddListener(() => { ChangeMainVelume(soundEffectVolume, -0.1f); });
//         addSundEffectBtn.onClick.AddListener(() => { ChangeMainVelume(soundEffectVolume, 0.1f); });

//         // GameManager.Instance.SettingUI(this);
//     }

//     private void ChangeMainVelume(Slider velumeScrollbar, float value)
//     {
//         velumeScrollbar.value += value;
//     }
//     //总音量控制
//     private void MainVelumeChange(float volumeStrong)
//     {
//         AudioPlayerController.Instance.ChangeMainVolume(volumeStrong);
//     }
//     //音乐音量控制
//     private void BGMVelumeChange(float volumeStrong)
//     {
//         AudioPlayerController.Instance.ChangeBgmVolume(volumeStrong);
//     }
//     //音效音量控制
//     private void SoundEffectVelumeChange(float volumeStrong)
//     {
//         AudioPlayerController.Instance.ChangeSoundEffectVolume(volumeStrong);
//     }
//     //打开设置界面并初始化
//     public void OpenSettingUI()
//     {
//         this.gameObject.SetActive(true);
//         GameManager.Instance.isDragingObject = true;
//         mainVolume.value = AudioPlayerController.Instance.mainVolume;
//         bgmVolume.value = AudioPlayerController.Instance.bgmVolume;
//         soundEffectVolume.value = AudioPlayerController.Instance.soundEffectVolume;
//     }

//     public void CloseSettingUI()
//     {
//         this.gameObject.SetActive(false);
//         GameManager.Instance.isDragingObject = true;
//     }
    
//     // Update is called once per frame
//     // void Update()
//     // {
//     //     if (isOpen)
//     //     {
//     //         if (Input.GetKeyUp(KeyCode.Escape))
//     //         {
//     //             isOpen = false;
//     //             this.gameObject.SetActive(false);
//     //             GameManager.Instance.isDragingObject = true;
//     //         }
//     //     }
    // }
}
