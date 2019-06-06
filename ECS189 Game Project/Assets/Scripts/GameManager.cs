using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Player.Command;

public enum GameState { MAIN_MENU, START, PLAYING, END}

public class GameManager
{
    // Start is called before the first frame update
    private static GameManager instance = new GameManager();
    public GameState gameState = GameState.MAIN_MENU;
    static GameManager()
    {
        SceneManager.LoadScene("SampleScene");
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
    public void CurrentState(GameState current)
    {
        if(gameState != current)
        {
            gameState = current;
            switch (current)
            {
                case GameState.MAIN_MENU:
                    break;
                case GameState.START:
                    break;
                case GameState.PLAYING:
                    break;
                case GameState.END:
                    break;
                default:
                    break;
            }
        }
    }

}
