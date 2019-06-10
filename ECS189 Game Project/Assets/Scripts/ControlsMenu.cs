using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsMenu : MonoBehaviour
{
    public void BackToMenu()
    {
        GameManager.Instance.ChangeScene("StartMenu");
    }
}
