using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    public Button resumeBtn;
    public Button settingBtn;

    public Button getAssertBtn;
    public Button quitBtn;

    private void Awake() {
        resumeBtn.onClick.AddListener(ResumeGame);
        settingBtn.onClick.AddListener(OpenSettingPanel);
        getAssertBtn.onClick.AddListener(GetAssert);
        quitBtn.onClick.AddListener(QuitGame);
    }
    private void ResumeGame() {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    private void OpenSettingPanel() {
        GameObject settingUI = Instantiate(Resources.Load<GameObject>("UI/SettingMenu"));
        settingUI.transform.SetParent(GameObject.Find("Canvas").transform);
    }
    private void GetAssert() {
        SceneManager.LoadScene("GetAssert");
    }
    private void QuitGame() {
        SceneManager.LoadScene("StartScene");
    }
}