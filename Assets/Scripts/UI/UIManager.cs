using UnityEngine;
//作者：闫辰祥
//创建时间: DATE

public class UIManager : MonoBehaviour
{
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TopDown");
    }
    public void BeingAssert()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GetAssert");
    }
}
