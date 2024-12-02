using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeLimit = 60f; // 设置计时器的时间（秒）
    private float timer;
    public TMP_Text timerText; // 用于显示剩余时间的 TextMesh Pro 组件

    void Start()
    {
        timer = timeLimit; // 初始化计时器
        UpdateTimerText(); // 初始化显示的时间
    }

    void Update()
    {
        // 递减计时器
        timer -= Time.deltaTime;

        // 检查计时器是否结束
        if (timer <= 0)
        {
            LoadNextLevel();
        }

        // 更新显示的时间
        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        // 更新 TMP 文本的内容
        timerText.text = $"{Mathf.Ceil(timer)} Left"; // 使用 Mathf.Ceil 以整秒显示
    }

    void LoadNextLevel()
    {
        // 切换到下一个关卡
        SceneManager.LoadScene("Fail");
    }
}