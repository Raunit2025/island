using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZombieSpawner : MonoBehaviour
{
    [Header("Zombie Settings")]
    public GameObject zombiePrefab;
    public int zombiesPerWave = 5;
    public float spawnRadius = 10f;
    public float timeBetweenWaves = 5f;

    [Header("References")]
    public Transform player;

    private List<GameObject> aliveZombies = new List<GameObject>();
    private int currentWave = 0;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            currentWave++;
            Debug.Log("Spawning Wave: " + currentWave);

            for (int i = 0; i < zombiesPerWave; i++)
            {
                Vector3 spawnPos = transform.position + (Random.insideUnitSphere * spawnRadius);
                spawnPos.y = 60f; // Keep on ground

                GameObject newZombie = Instantiate(zombiePrefab, spawnPos, Quaternion.identity);
                aliveZombies.Add(newZombie);
            }

            // Wait until all zombies are dead
            yield return new WaitUntil(() => aliveZombies.TrueForAll(z => z == null));

            Debug.Log("Wave " + currentWave + " cleared!");

            yield return new WaitForSeconds(timeBetweenWaves);

            // Increase difficulty if you want
            zombiesPerWave += 2;
        }
    }

    // Optional: cleanup dead zombies
    void Update()
    {
        aliveZombies.RemoveAll(z => z == null);
    }
}
