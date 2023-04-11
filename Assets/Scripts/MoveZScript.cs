using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveZScript : MonoBehaviour
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

        transform.Translate(Vector3.back * Time.deltaTime * speedKeeper.speed);




        // Destroy obstacle if it goes past the back boundary
        if (transform.position.z < zBound)
        {
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        // Set game over flag if the player collides with this object
        if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("Obstacle"))
        {
            gameMan.isGameOver = true;
        } else if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("PowerUp"))
        {
            Destroy(gameObject);
        }
    }
}
