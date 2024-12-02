using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement; // ���������ռ���ʹ�ó�������
using System.Collections.Generic; // ���������ռ���ʹ��List

public class CarController : MonoBehaviour
{
    public float acceleration = 10f; // ���ٶ�
    public float maxSpeed = 100f;     // ����ٶ�
    public float turnSpeed = 100f;    // ת���ٶ�
    public float brakeForce = 50f;    // ɲ������
    public float deceleration = 5f;    // �ٶ�˥��ϵ��
    public Slider speedSlider;         // �ٶȻ���
    public List<NavMeshAgent> aiAgents; // �洢���AI Agent
    public Transform finishLine;       // �յ�����
    public string nextLevelSceneName;  // ��һ�صĳ�������
    public string failLevelSceneName;   // ʧ�ܹصĳ�������

    private float currentSpeed = 0f;   // ��ǰ�ٶ�

    void Update()
    {
        HandleInput();
        UpdateSpeedSlider();
        CheckAIBehavior();
        CheckGameStatus();
    }

    private void HandleInput()
    {
        // �����߼�
        if (Input.GetKey(KeyCode.UpArrow))
        {
            currentSpeed += acceleration * Time.deltaTime; // ʹ�ü��ٶ����ӵ�ǰ�ٶ�
        }

        // ɲ���߼�
        if (Input.GetKey(KeyCode.Space))
        {
            currentSpeed -= brakeForce * Time.deltaTime; // ʹ��ɲ�����ȼ��ٵ�ǰ�ٶ�
        }

        // ת���߼�
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        }

        // ��������ٶ�
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);

        // Ӧ���ٶ�˥��
        if (!Input.GetKey(KeyCode.UpArrow) && currentSpeed > 0)
        {
            currentSpeed -= deceleration * Time.deltaTime; // Ӧ��˥��
            currentSpeed = Mathf.Max(currentSpeed, 0); // ȷ���ٶȲ�Ϊ��
        }

        // �ƶ�����
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    private void UpdateSpeedSlider()
    {
        speedSlider.value = currentSpeed / maxSpeed; // �����ٶȻ���
    }

    private void CheckAIBehavior()
    {
        if (currentSpeed > 15f) // �ٶ���ֵ
        {
            foreach (var aiAgent in aiAgents)
            {
                aiAgent.SetDestination(transform.position); // ��������λ����ΪĿ��
            }
        }
    }

    private void CheckGameStatus()
    {
        // �������Ƿ񵽴��յ�����
        if (Vector3.Distance(transform.position, finishLine.position) < 5f)
        {
            // �л�����һ��
            SceneManager.LoadScene(nextLevelSceneName);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("NPC")) // ȷ��AI��"NPC"��ǩ
        {
            // �л���ʧ�ܹؿ�
            SceneManager.LoadScene(failLevelSceneName);
        }
    }
}