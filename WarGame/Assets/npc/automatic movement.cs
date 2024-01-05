using UnityEngine;
using UnityEngine.AI;

public class NPCFollow : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public float followDistance = 1.0f; // NPC将停在主角前方1米的位置

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            // 计算目标位置
            Vector3 targetPosition = player.position + player.forward * followDistance;
            // 设置NPC的目的地
            agent.destination = targetPosition;
        }
    }
}