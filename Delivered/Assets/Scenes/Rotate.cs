using UnityEngine;
using UnityEngine.AI;

public class NavMeshMoveAndLook : MonoBehaviour
{
    public float rotationSpeed = 10f; // 旋转速度

    private NavMeshAgent navMeshAgent;

    void Start()
    {
        // 获取 NavMeshAgent 组件
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // 如果 NavMeshAgent 正在移动
        if (navMeshAgent.velocity.magnitude > 0.1f)
        {
            // 获取当前行进方向
            Vector3 direction = navMeshAgent.velocity.normalized;

            // 计算目标旋转
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            // 平滑旋转
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}