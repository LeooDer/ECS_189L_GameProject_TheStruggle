using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Player.Command;

public class GameManager
{
    // Start is called before the first frame update
    private static GameManager instance = new GameManager();

    static GameManager()
    {
        GameManager.Instance.ChangeScene("StartMenu");

    }
    private GameManager()
    {

    }
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    public void ChangeScene(string scene)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        Scene newScene = SceneManager.GetSceneByName(scene);
        if(!SceneManager.Equals(currentScene,newScene))
        {
            SceneManager.LoadScene(scene);
        }
    }

    public bool CheckCurrentScene(string scene)
    {
        if(SceneManager.Equals(SceneManager.GetActiveScene(),SceneManager.GetSceneByName(scene)))
            return true;
        return false;
    }

    public void Pause()
    {
        if(Time.timeScale == 0)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }
}
