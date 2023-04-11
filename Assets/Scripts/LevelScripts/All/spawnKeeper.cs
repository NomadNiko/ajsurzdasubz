using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnKeeper : MonoBehaviour
{
    public float minSpawnDelay = 1f; // the minimum time between obstacle spawns
    public float maxSpawnDelay = 2f; // the maximum time between obstacle spawns
    public GameObject[] obstaclePrefabs; // array of obstacle prefabs to spawn
    public gameManager gameManager;
    private float decreaseDelayInterval = 10f; // The interval at which to decrease the delay values
    private float yStartPos = 0.05f;
    private float zStartPos = 36f;
    private int trackRangeUp = 1;
    private int trackRangeLow = -1;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<gameManager>();
        StartCoroutine(countDown());
        StartCoroutine(SpawnObstacle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator countDown()
    {
        float minDelayDecrement = 0.15f; // The amount to decrement minSpawnDelay by
        float maxDelayDecrement = 0.15f; // The amount to decrement maxSpawnDelay by
        yield return new WaitForSeconds(decreaseDelayInterval);
        minSpawnDelay -= minDelayDecrement;
        maxSpawnDelay -= maxDelayDecrement;
        StartCoroutine(countDown());
    }

    IEnumerator SpawnObstacle()
    {

        while (true)
        {
            // Randomly select the track and obstacle to spawn
            int selectTrack = Random.Range(trackRangeLow, (trackRangeUp + 1)); // -1 = left track, 0 = middle track, 1 = right track
            int obstacleSelect = Random.Range(0, obstaclePrefabs.Length); // Randomly select an obstacle prefab to spawn

            // Randomly select the spawn position
            Vector3 spawnPos = new Vector3((0 + selectTrack), yStartPos, zStartPos); // Set the spawn position for the obstacle

            // Wait for the spawn delay time
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));

            // If the game is not over, spawn the obstacle
            if (!gameManager.isGameOver)
            {
                Instantiate(obstaclePrefabs[obstacleSelect], spawnPos, transform.rotation); // Spawn the obstacle at the spawn position
            }

        }
    }
}
