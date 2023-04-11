using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetTile : MonoBehaviour
{
    private Vector3 startPos;               // Variables to store initial position and repeat width of the tile
    private float repeatWidth;

    void Start()
    {
        // Store initial position of the tile
        startPos = transform.position;

        // Get the size of the tile along the z-axis and divide by 2 to get half the size
        // This is used to calculate the repeat width of the tile
        repeatWidth = GetComponent<BoxCollider>().size.z / 2;
    }

    void Update()
    {
        // If the tile has moved past its initial position minus the repeat width along the z-axis
        // then reset its position back to the initial position
        if (transform.position.z < startPos.z - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
