using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;  // The enemy prefab you want to spawn.
    [SerializeField] Collider floorCollider;  // The collider representing the floor area.
    [SerializeField] Transform enemyParent;  // Transform for Enemy to spawn.

    [SerializeField] float spawnInterval = 2f; // Time between enemy spawns.
    public float movementSpeed = 2f; // The speed at which the enemy moves.

    private void Start()
    {
        // Start spawning enemies at regular intervals.
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Calculate a random spawn point on the edge of the floor area.
            Vector3 spawnPoint = CalculateRandomSpawnPoint();

            // Instantiate the enemy prefab at the calculated spawn point.
            Instantiate(enemyPrefab, spawnPoint, Quaternion.identity, enemyParent);

            // Wait for the specified spawn interval before spawning another enemy.
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector3 CalculateRandomSpawnPoint()
    {
        // Calculate random values for X and Z within the collider bounds.
        float randomX = Random.Range(floorCollider.bounds.min.x, floorCollider.bounds.max.x);
        float randomZ = Random.Range(floorCollider.bounds.min.z, floorCollider.bounds.max.z);

        // Choose a random edge of the collider (top, bottom, left, or right).
        int edge = Random.Range(0, 4);

        // Offset value to move the spawn point inside the edge (adjust as needed).
        float offset = 1.0f;

        // Calculate the spawn point based on the chosen edge.
        switch (edge)
        {
            case 0: // Top edge
                return new Vector3(randomX, 0f, floorCollider.bounds.max.z - offset);
            case 1: // Bottom edge
                return new Vector3(randomX, 0f, floorCollider.bounds.min.z + offset);
            case 2: // Left edge
                return new Vector3(floorCollider.bounds.min.x + offset, 0f, randomZ);
            case 3: // Right edge
                return new Vector3(floorCollider.bounds.max.x - offset, 0f, randomZ);
            default:
                return Vector3.zero;
        }
    }
}
