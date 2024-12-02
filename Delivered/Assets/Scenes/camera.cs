using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // 玩家对象
    public Vector3 offset;   // 摄像机相对于玩家的位置偏移
    public float smoothSpeed = 0.125f; // 平滑移动速度

    private void LateUpdate()
    {
        // 计算目标位置
        Vector3 desiredPosition = player.position + offset;

        // 使用 Lerp 方法进行平滑跟随
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // 可选：让摄像机始终朝向玩家
        transform.LookAt(player);
    }
}