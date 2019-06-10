using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        GameManager.Instance.ChangeScene("Test Level");
    }

    public void Controls()
    {
        GameManager.Instance.ChangeScene("Controls");
    }
}
