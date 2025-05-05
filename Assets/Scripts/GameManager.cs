using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    int currentLevel = 1;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    public void NewGame()
    {
        SceneManager.LoadScene(sceneName: "howto");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NextLevel(){
        currentLevel++;
        if(currentLevel > 3){
            Debug.Log("WIN GOES HERE AAAAAAA");    
        }

        SceneManager.LoadScene(sceneName:"level"+currentLevel);
    }
}