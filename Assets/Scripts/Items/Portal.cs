/*
 * Author:      #AUTHORNAME#
 * CreateTime:  #CREATETIME#
 * 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : InteractObjBase
{
    [Header("传送设置")]
    public SceneType targetScene; // 目标场景
    public Vector2 portalOffset = new Vector2(1f, 0f); // 对应传送门的生成偏移
    public float teleportDelay = 0.1f; // 传送延迟时间
    public bool canTeleport = true;    // 是否可以传送

    [Header("可选效果")]
    public bool fadeEffect = false;     // 是否使用渐变效果
    public ParticleSystem teleportEffect; // 可选的传送特效

    private GameObject player;
    private Portal linkedPortal; // 对应场景的传送门

    void Start()
    {
        // 如果没有关联的传送门，创建一个
        if (linkedPortal == null)
        {
            CreateLinkedPortal();
        }
    }

    private void CreateLinkedPortal()
    {
        // 获取目标场景的对象和出生点
        GameObject targetSceneObj = SceneController.Instance.GetSceneObject(targetScene);
        Transform spawnPoint = SceneController.Instance.GetSpawnPoint(targetScene);
        
        if (targetSceneObj == null || spawnPoint == null)
        {
            Debug.LogWarning($"Cannot create linked portal: missing scene object or spawn point for {targetScene}");
            return;
        }

        // 在目标场景的出生点创建传送门
        GameObject portalPrefab = gameObject;
        Vector3 position = spawnPoint.position + (Vector3)portalOffset;
        GameObject newPortal = Instantiate(portalPrefab, position, Quaternion.identity, targetSceneObj.transform);
        
        // 设置新传送门的属性
        Portal newPortalComponent = newPortal.GetComponent<Portal>();
        newPortalComponent.targetScene = SceneController.Instance.GetCurrentScene(); // 设置回传的场景
        newPortalComponent.linkedPortal = this; // 设置关联
        this.linkedPortal = newPortalComponent; // 双向关联

        // 确保新传送门不会再创建传送门
        newPortalComponent.enabled = false;
        newPortalComponent.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canTeleport) return;

        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            other.GetComponent<DemoCharacterController>().SetInteractObject(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<DemoCharacterController>().ClearInteractObject();
        }
    }

    private void EnableTeleport()
    {
        canTeleport = true;
    }

    public override void Interact()
    {
        TeleportPlayer(player);
    }

    private void TeleportPlayer(GameObject player)
    {
        if (!canTeleport || player == null) return;

        if (teleportEffect != null)
        {
            teleportEffect.Play();
        }

        // 获取目标场景的出生点
        Transform spawnPoint = SceneController.Instance.GetSpawnPoint(targetScene);
        if (spawnPoint != null)
        {
            // 切换场景并传送玩家
            SceneController.Instance.LoadScene(targetScene);
            player.transform.position = spawnPoint.position;

            // 暂时禁用传送门以防止连续传送
            canTeleport = false;
            Invoke("EnableTeleport", teleportDelay);
        }
        else
        {
            Debug.LogWarning($"No spawn point found for scene: {targetScene}");
        }
    }
}
