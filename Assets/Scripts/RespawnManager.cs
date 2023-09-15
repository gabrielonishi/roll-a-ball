using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public GameObject pickUpPrefab;
    public Vector3 minSpawnPoint = new Vector3(-11.0f, 0.0f, -21.0f);
    public Vector3 maxSpawnPoint = new Vector3(11.0f, 0.0f, 21.0f);

    private static float timer = 5;
    
    void Start()
    {
        Debug.Log("Entrou");
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(minSpawnPoint.x, maxSpawnPoint.x),
                0.5f,
                Random.Range(minSpawnPoint.z, maxSpawnPoint.z)
            );
            Instantiate(pickUpPrefab, randomPosition, Quaternion.identity);
            timer = 5;
        }
    }
}
