using System.Collections;
using System.Collections.Generic;
using CrashKonijn.Goap.Behaviours;
using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    [SerializeField] private GameObject clientPrefab;
    [SerializeField] private float spawnInterval;
    [SerializeField] private int totalSpawns;
    [SerializeField] private GoapSetBehaviour goapSetBehaviour;
    private float time;
    private int spawnCounter = 0;

    void Start()
    {
        time = spawnInterval;
        clientPrefab.GetComponent<AgentBehaviour>().goapSetBehaviour = goapSetBehaviour;
    }

    void Update()
    {
        time -= Time.deltaTime;

        while (time < 0)
        {
            time += spawnInterval;
            Spawn();
            if (spawnCounter >= totalSpawns)
            {
                enabled = false;
            }
        }
    }

    void Spawn()
    {
        Instantiate(clientPrefab, transform.position, transform.rotation);
        spawnCounter++;
    }
}