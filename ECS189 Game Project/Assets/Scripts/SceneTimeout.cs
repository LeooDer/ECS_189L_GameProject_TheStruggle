using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player.Command;

public class SceneTimeout : MonoBehaviour
{
    private double InitialTime;
    private double EndTime;
    public void Awake()
    {
        InitialTime = 0.0;
        EndTime = 2.0;
    }
    public void Update()
    {
        if(InitialTime < EndTime)
        {
            InitialTime += Time.deltaTime;
        }
        else
        {
            GameManager.Instance.ChangeScene("StartMenu");
        }
    }
}
