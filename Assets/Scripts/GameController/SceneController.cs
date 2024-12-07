//作者：闫辰祥
//创建时间: 2024年12月7日

using System.Collections.Generic;
using UnityEngine;

public enum SceneType
{
    City,
    CyberCity
}

[System.Serializable]
public class SceneObject
{
    public SceneType sceneType;
    public GameObject sceneObject;
}


public class SceneController : MonoBehaviour
{
    public List<SceneObject> sceneObjects = new List<SceneObject>();

    public void LoadScene(SceneType sceneType)
    {
        foreach (var sceneObject in sceneObjects)
        {
            if (sceneObject.sceneType == sceneType)
            {
                sceneObject.sceneObject.SetActive(true);
            }
            else
            {
                sceneObject.sceneObject.SetActive(false);
            }
        }
    }
}