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
    public Transform playerSpawnPoint;
    public SpriteRenderer boundSprite;
}

public class SceneController : Singleton<SceneController>
{
    public List<SceneObject> sceneObjects = new List<SceneObject>();
    private SceneObject currentSceneObject ;
    private DemoCameraFollow mainCamera;

    private void Start()
    {
        mainCamera = Camera.main.GetComponent<DemoCameraFollow>();
        currentSceneObject = sceneObjects.Find(x => x.sceneType == SceneType.City);
    }

    /// <summary>
    /// 加载指定场景
    /// </summary>
    public void LoadScene(SceneType sceneType)
    {
        foreach (var sceneObject in sceneObjects)
        {
            if (sceneObject.sceneType == sceneType)
            {
                sceneObject.sceneObject.SetActive(true);
                currentSceneObject = sceneObject;
                
                if (mainCamera != null && sceneObject.boundSprite != null)
                {
                    mainCamera.SetBoundSprite(sceneObject.boundSprite);
                }
            }
            else
            {
                sceneObject.sceneObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 获取指定场景的出生点
    /// </summary>
    /// <param name="sceneType">场景类型</param>
    /// <returns>出生点Transform，如果未找到返回null</returns>
    public Transform GetSpawnPoint(SceneType sceneType)
    {
        var sceneObject = sceneObjects.Find(x => x.sceneType == sceneType);
        return sceneObject?.playerSpawnPoint;
    }

    /// <summary>
    /// 获取当前场景
    /// </summary>
    public SceneType GetCurrentScene()
    {
        return currentSceneObject?.sceneType ?? SceneType.City;
    }

    /// <summary>
    /// 获取指定场景的场景对象
    /// </summary>
    /// <param name="sceneType">场景类型</param>
    /// <returns>场景GameObject，如果未找到返回null</returns>
    public GameObject GetSceneObject(SceneType sceneType)
    {
        var sceneObject = sceneObjects.Find(x => x.sceneType == sceneType);
        return sceneObject?.sceneObject;
    }

    /// <summary>
    /// 获取当前场景对象
    /// </summary>
    public GameObject GetCurrentSceneObject()
    {
        return currentSceneObject?.sceneObject;
    }
}