using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject monsterPrefab; // 拖入Monster预制体
    public int spawnCount = 5;       // 生成数量
    public Vector3 spawnAreaCenter = Vector3.zero; // 生成区域中心
    public Vector3 spawnAreaSize = new Vector3(10, 0, 10); // 生成区域大小

    void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 randomPos = spawnAreaCenter + new Vector3(
                Random.Range(-spawnAreaSize.x/2, spawnAreaSize.x/2),
                0,
                Random.Range(-spawnAreaSize.z/2, spawnAreaSize.z/2)
            );
            Instantiate(monsterPrefab, randomPos, Quaternion.identity);
        }
    }
}