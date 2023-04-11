using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controlStartMenu : MonoBehaviour
{
    // The selected player
    public string playerSelected;
    public TextMeshPro timerText;
    private Vector2 startPos;
    public int pixelDistToDetect = 20;
    private bool fingerDown;

    void Start()
    {
        timerText.text = "HighScore: " + PlayerPrefs.GetFloat("playerHighScore").ToString("F2");
    }

    void Update()
    {
        if (fingerDown == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            // If so, we're going to set the startPos to the first touch's position, 
            startPos = Input.touches[0].position;
            // ... and set fingerDown to true to start checking the direction of the swipe.
            fingerDown = true;
        }
        if (fingerDown)
        {
            if (Input.touches[0].position.x <= startPos.x - pixelDistToDetect)
            {
                fingerDown = false;
                PlayerPrefs.SetInt(playerSelected, 2);
                PlayerPrefs.Save();
                // Load the first level
                SceneManager.LoadScene("Level1");
            }
            //Did we swipe right?
            else if (Input.touches[0].position.x >= startPos.x + pixelDistToDetect)
            {
                fingerDown = false;
                PlayerPrefs.SetInt(playerSelected, 1);
                PlayerPrefs.Save();
                // Load the first level
                SceneManager.LoadScene("Level1");
            }
        }
        // If the Z key is pressed
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // Set the selected player to 2 and save it
            PlayerPrefs.SetInt(playerSelected, 2);
            PlayerPrefs.Save();
            // Load the first level
            SceneManager.LoadScene("Level1");
        }
        // If the A key is pressed
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Set the selected player to 1 and save it
            PlayerPrefs.SetInt(playerSelected, 1);
            PlayerPrefs.Save();
            // Load the first level
            SceneManager.LoadScene("Level1");
        }
    }
}
