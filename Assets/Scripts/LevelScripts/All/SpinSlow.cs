using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinSlow : MonoBehaviour
{
    public float spinSpeed = 6f;
    public gameManager gameMan;             // Reference to game manage

    // Start is called before the first frame update
    void Start()
    {
        gameMan = FindObjectOfType<gameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward* spinSpeed);
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
