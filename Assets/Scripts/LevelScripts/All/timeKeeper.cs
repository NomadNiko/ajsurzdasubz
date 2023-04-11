using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class timeKeeper : MonoBehaviour
{
    public TextMeshPro timerText;
    public float secondsCount;
    private int minutesCount;
    public int seconds;
    private int milliseconds;
    public gameManager gameManager;
    public bool isGameOver;


    void Start()
    {
        gameManager = FindObjectOfType<gameManager>();
    }


    // Update is called once per frame
    void Update()
    {
        UpdateTimerUI();

    }

    public void UpdateTimerUI()
    {
        if (gameManager.isGameOver)
        {
            timerText.text = "Score: " + secondsCount.ToString("F2");
            return;
        }

        secondsCount += Time.deltaTime;
            minutesCount = (int)secondsCount / 60;
            seconds = (int)secondsCount % 60;
            milliseconds = (int)(secondsCount * 1000) % 100;
            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutesCount, seconds, milliseconds);


    }
}