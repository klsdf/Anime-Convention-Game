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
    public Transform destinationPoint; // 传送目标点
    public float teleportDelay = 0.1f; // 传送延迟时间
    public bool canTeleport = true;    // 是否可以传送

    public DemoCameraFollow cameraFollow;
    public SpriteRenderer boundSprite;

    [Header("可选效果")]
    public bool fadeEffect = false;     // 是否使用渐变效果
    public ParticleSystem teleportEffect; // 可选的传送特效



    private GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canTeleport || destinationPoint == null) return;

        // 检查是否是玩家
        if (other.CompareTag("Player"))
        {
            // TeleportPlayer(other.gameObject);
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

    void Start()
    {
        destinationPoint = GameObject.Find("DestinationPoint").transform;
        cameraFollow = GameObject.Find("Main Camera").GetComponent<DemoCameraFollow>();
        boundSprite = GameObject.Find("室内").GetComponent<SpriteRenderer>();
    }

    public override void Interact()
    {
        TeleportPlayer(player);
    }
     private void TeleportPlayer(GameObject player)
    {
        if (teleportEffect != null)
        {
            teleportEffect.Play();
        }

        // 立即传送
        player.transform.position = destinationPoint.position;

        // 暂时禁用传送门以防止连续传送
        canTeleport = false;
        Invoke("EnableTeleport", teleportDelay);
        cameraFollow.SetBoundSprite(boundSprite);
    }
}
