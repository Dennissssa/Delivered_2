using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class PlayerFollowMultipleAI : MonoBehaviour
{
    public List<GameObject> aiCharacters; // AI 角色的列表
    public float stoppingDistance = 2.0f; // 停止跟随的距离

    void Update()
    {
        // 遍历所有 AI 角色
        foreach (GameObject aiCharacter in aiCharacters)
        {
            NavMeshAgent aiNavMeshAgent = aiCharacter.GetComponent<NavMeshAgent>();
            if (aiNavMeshAgent != null)
            {
                float distance = Vector3.Distance(aiCharacter.transform.position, transform.position);

                // 如果距离大于停止距离，移动 AI
                if (distance > stoppingDistance)
                {
                    aiNavMeshAgent.SetDestination(transform.position);
                }
                else
                {
                    // 停止移动
                    aiNavMeshAgent.ResetPath();
                }
            }
        }
    }
}