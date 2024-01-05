using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingIsland : MonoBehaviour
{
    public float amplitude = 0.5f; // 上下移动的幅度
    public float frequency = 1f; // 上下移动的频率

    private Vector3 startPos;

    void Start()
    {
        // 记录起始位置
        startPos = transform.position;
    }

    void Update()
    {
        // 计算新的Y位置
        float newY = startPos.y + amplitude * Mathf.Sin(Time.time * frequency);
        // 设置新的位置
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}