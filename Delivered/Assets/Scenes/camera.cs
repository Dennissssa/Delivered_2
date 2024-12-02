using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // ��Ҷ���
    public Vector3 offset;   // ������������ҵ�λ��ƫ��
    public float smoothSpeed = 0.125f; // ƽ���ƶ��ٶ�

    private void LateUpdate()
    {
        // ����Ŀ��λ��
        Vector3 desiredPosition = player.position + offset;

        // ʹ�� Lerp ��������ƽ������
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // ��ѡ���������ʼ�ճ������
        transform.LookAt(player);
    }
}