using UnityEngine;

public class BarracksSpawner : MonoBehaviour
{
    public Transform player; // 玩家的Transform组件，需要在Inspector中设置
    public GameObject barracksPrefab; // 兵营的预制体，需要在Inspector中设置
    public float spawnDistance = 5f; // 兵营生成在玩家前方的距离

    void Update()
    {
        // 检查是否按下了数字键1
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnBarracks();
        }
    }

    public void SpawnBarracks()
    {
        if(player == null || barracksPrefab == null)
        {
            Debug.LogError("Player or BarracksPrefab reference not set in the inspector!");
            return;
        }

        // 计算兵营应该生成的位置
        Vector3 spawnPosition = player.position + player.forward * spawnDistance;

        // 在计算出的位置生成兵营
        Instantiate(barracksPrefab, spawnPosition, Quaternion.identity);
    }
}
