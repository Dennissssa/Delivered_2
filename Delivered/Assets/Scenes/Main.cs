using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement; // 引入命名空间以使用场景管理
using System.Collections.Generic; // 引入命名空间以使用List

public class CarController : MonoBehaviour
{
    public float acceleration = 10f; // 加速度
    public float maxSpeed = 100f;     // 最大速度
    public float turnSpeed = 100f;    // 转向速度
    public float brakeForce = 50f;    // 刹车力度
    public float deceleration = 5f;    // 速度衰减系数
    public Slider speedSlider;         // 速度滑块
    public List<NavMeshAgent> aiAgents; // 存储多个AI Agent
    public Transform finishLine;       // 终点区域
    public string nextLevelSceneName;  // 下一关的场景名称
    public string failLevelSceneName;   // 失败关的场景名称

    private float currentSpeed = 0f;   // 当前速度

    void Update()
    {
        HandleInput();
        UpdateSpeedSlider();
        CheckAIBehavior();
        CheckGameStatus();
    }

    private void HandleInput()
    {
        // 加速逻辑
        if (Input.GetKey(KeyCode.UpArrow))
        {
            currentSpeed += acceleration * Time.deltaTime; // 使用加速度增加当前速度
        }

        // 刹车逻辑
        if (Input.GetKey(KeyCode.Space))
        {
            currentSpeed -= brakeForce * Time.deltaTime; // 使用刹车力度减少当前速度
        }

        // 转向逻辑
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        }

        // 限制最大速度
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);

        // 应用速度衰减
        if (!Input.GetKey(KeyCode.UpArrow) && currentSpeed > 0)
        {
            currentSpeed -= deceleration * Time.deltaTime; // 应用衰减
            currentSpeed = Mathf.Max(currentSpeed, 0); // 确保速度不为负
        }

        // 移动汽车
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    private void UpdateSpeedSlider()
    {
        speedSlider.value = currentSpeed / maxSpeed; // 更新速度滑块
    }

    private void CheckAIBehavior()
    {
        if (currentSpeed > 15f) // 速度阈值
        {
            foreach (var aiAgent in aiAgents)
            {
                aiAgent.SetDestination(transform.position); // 用汽车的位置作为目标
            }
        }
    }

    private void CheckGameStatus()
    {
        // 检查玩家是否到达终点区域
        if (Vector3.Distance(transform.position, finishLine.position) < 5f)
        {
            // 切换到下一关
            SceneManager.LoadScene(nextLevelSceneName);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("NPC")) // 确保AI有"NPC"标签
        {
            // 切换到失败关卡
            SceneManager.LoadScene(failLevelSceneName);
        }
    }
}