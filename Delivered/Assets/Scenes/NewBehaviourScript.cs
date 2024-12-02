using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour
{
    public string levelToLoad; // 要加载的关卡名称


    void Start()
    {

    }


    void Update()
    {

    }

 
    public void LoadLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}