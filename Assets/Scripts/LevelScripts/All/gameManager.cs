using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public GameObject player1; // the game object for player 1
    public GameObject player2; // the game object for player 2
    public GameObject player3;
    public TextMeshPro timerText;
    public timeKeeper timeKeeper;
    public spawnKeeper spawnKeeper;
    public float playerHighScore;
    public string playerSelected; // the selected player
    public bool playerSpawned = false; // whether or not the player has spawned
    public bool isGameOver = false; // whether or not the game is over
    private int restartWait = 3; // the time to wait before restarting the game
    private Vector3 playerSpawn = new Vector3(0, 0.15f, -0.45f); // the spawn position for the player

    void Start()
    {
        timeKeeper = FindObjectOfType<timeKeeper>();
        spawnKeeper = FindObjectOfType<spawnKeeper>();
    }

    void Update()
    {
        // If the game is over, restart the game
        if (isGameOver)
        {
            StartCoroutine(RestartGame());
        }
        if (!playerSpawned)
        {
            StartCoroutine(SpawnPlayer());
        }
    }


    IEnumerator SpawnPlayer()
    {
        playerSpawned = true; // Set the player to be spawned
        if (PlayerPrefs.GetInt(playerSelected) == 1) // If player 1 is selected
        {
            Instantiate(player1, playerSpawn, transform.rotation); // Spawn player 1 at the player spawn position
        }
        if (PlayerPrefs.GetInt(playerSelected) == 2) // If player 2 is selected
        {
            Instantiate(player2, playerSpawn, transform.rotation); // Spawn player 2 at the player spawn position
        }
        yield return new WaitForSeconds(1); // Wait for 1 second
    }


    IEnumerator RestartGame()
    {
        if (timeKeeper.secondsCount > PlayerPrefs.GetFloat("playerHighScore"))
        {
            PlayerPrefs.SetFloat("playerHighScore", timeKeeper.secondsCount);
            PlayerPrefs.Save();
        }

        // Wait for the restart wait time
        yield return new WaitForSeconds(restartWait);
        SceneManager.LoadScene("LoadingMenu");
        PlayerPrefs.DeleteKey(playerSelected);
    }
}
