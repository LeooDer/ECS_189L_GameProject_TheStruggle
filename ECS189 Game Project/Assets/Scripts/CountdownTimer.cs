using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public int timeLeft = 60;
    public Text countdownText;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("Countdown");
    }

    // Update is called once per frame
    void Update()
    {
        countdownText.text = ("TIME LEFT: " + timeLeft);

        if (timeLeft <= 0)
        {
            StopCoroutine("Countdown");
            countdownText.text = "TIMES UP!";
            LoadLoseLevel();
        }
    }

    IEnumerator Countdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }

    void LoadLoseLevel()
    {
        // Load the level named "Lose".
        GameManager.Instance.ChangeScene("Lose");
    }
}
