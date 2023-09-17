using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public GameObject pickUpPrefab;
    public GameObject enemyPrefab;
    public Vector3 minSpawnPoint = new Vector3(-11.0f, 0.0f, -21.0f);
    public Vector3 maxSpawnPoint = new Vector3(11.0f, 0.0f, 21.0f);

    private static float pickupTimer = 5;
    private static float enemyTimer = 10;

    Vector3 GeneratePosition()
    {
        return new Vector3(
            Random.Range(minSpawnPoint.x, maxSpawnPoint.x),
            0.5f,
            Random.Range(minSpawnPoint.z, maxSpawnPoint.z)
        );
    }

    void Update()
    {
        pickupTimer -= Time.deltaTime;
        enemyTimer -= Time.deltaTime;
        if (pickupTimer <= 0)
        {
            Vector3 randomPosition = GeneratePosition();
            Instantiate(pickUpPrefab, randomPosition, Quaternion.identity);
            pickupTimer = 5;
            Debug.Log("Spawn pickup");
        }
        if (enemyTimer <= 0)
        {
            Debug.Log("Entrou");
            Vector3 randomPosition = GeneratePosition();
            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
            enemyTimer = 10;
        }
    }
}
