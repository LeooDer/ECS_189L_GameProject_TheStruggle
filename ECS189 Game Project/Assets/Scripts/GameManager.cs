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
        SceneManager.LoadScene("StartMenu");

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
    public void CurrentState(string scene)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        Scene newScene = SceneManager.GetSceneByName(scene);
        if(!SceneManager.Equals(currentScene,newScene))
        {
            SceneManager.LoadScene(scene);
            SceneManager.UnloadScene(currentScene);
            SceneManager.SetActiveScene(newScene);
        }
    }

}
