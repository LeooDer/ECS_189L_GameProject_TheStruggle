using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        if (!AudioManager.instance.IsPlaying("MenuSong"))
            AudioManager.instance.Play("MenuSong");
    }

    public void PlayGame()
    {
        GameManager.Instance.ChangeScene("Test Level");
    }

    public void Controls()
    {
        GameManager.Instance.ChangeScene("Controls");
    }
}
