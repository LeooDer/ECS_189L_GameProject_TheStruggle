/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    //[SerializeField] private 
    private float timeLeft = 0f;
    private readonly float startingTime = 90f;

    // Start is called before the first frame update
    void Start()
    {
        // Game starts with 90 seconds.
        timeLeft = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= 1 * Time.deltaTime;
        if (timeLeft < 0)
        {
            //GameOver(); // Need to make a GameOver() function
        }
    }
}*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public int timeLeft = 90;
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
