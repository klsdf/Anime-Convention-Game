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
        UIController.Instance.ShowPanel(PanelType.Item );
    }
    private void OpenSettingPanel() {
        UIController.Instance.ShowPanel(PanelType.Setting);
    }
    private void GetAssert() {
        UIController.Instance.ShowPanel(PanelType.Assert);
    }
    private void QuitGame() {
        SceneManager.LoadScene("StartScene");
    }
}