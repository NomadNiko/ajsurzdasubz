using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveXScript : MonoBehaviour
{

    private float zBound = -5;              // Position of the back boundary
    public bool stopMovement = false;       // Flag to stop movement
    public gameManager gameMan;             // Reference to game manage
    public speedKeeper speedKeeper;

    private void Start()
    {
        // Find game manager object in scene
        speedKeeper = FindObjectOfType<speedKeeper>();
        gameMan = FindObjectOfType<gameManager>();
    }

    void Update()
    {
        // Don't move if game is over
        if (gameMan.isGameOver)
        {
            return;
        }

        // Increase speed for train cart obstacle
        if (gameObject.name == "TrainCart(Clone)")
        {
            transform.Translate(Vector3.up * Time.deltaTime * (speedKeeper.speed + 4));
        }
        else        // Move object backward
        {
            transform.Translate(Vector3.up * Time.deltaTime * speedKeeper.speed);
        }




        // Destroy obstacle if it goes past the back boundary
        if (transform.position.z < zBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        // Set game over flag if the player collides with this object
        if (other.gameObject.CompareTag("Player"))
        {
            gameMan.isGameOver = true;
        }
    }
}
