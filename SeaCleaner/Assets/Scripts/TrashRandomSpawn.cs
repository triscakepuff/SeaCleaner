using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashRandomSpawn : MonoBehaviour
{
    [Header("Spawner Settings")]
    public int numberOfSpawns = 10;
    public GameObject[] prefabsToSpawn;

    [Header("Bounds for Spawning")]
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -10f;
    public float maxY = 10f;

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        for (int i = 0; i < numberOfSpawns; i++)
        {
            // Choose a random prefab from the list
            GameObject prefab = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)];

            // Choose a random position within the bounds
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            Vector2 randomPosition = new Vector2(randomX, randomY);

            // Instantiate the prefab at the random position
            Instantiate(prefab, randomPosition, Quaternion.identity);
        }
    }
}
